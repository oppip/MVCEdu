using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCEdu.Data;
using MVCEdu.Models;
using MVCEdu.ViewModels;

namespace MVCEdu.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly MVCEduContext _context;

        public EnrollmentsController(MVCEduContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var MVCEduContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(await MVCEduContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        public async Task<IActionResult> Status(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }





        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }

        /////////////////////////////////////////
        ////////////////////////////////////////
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollStudents(int id, EnrollStudentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<int?> listStudents = model.SelectedStudents;
                    IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                    _context.Enrollment.RemoveRange(toBeRemoved);
                    IEnumerable<int?> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                    IEnumerable<int?> newStudents = listStudents.Where(s => !existStudents.Contains(s));

                    foreach (int studentId in newStudents)
                        _context.Enrollment.Add(new Enrollment { StudentId = studentId, CourseId = id, Year = model.Year, Semester = model.Semester, });

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(CoursesController.Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EnrollStudents(int? id)
        {
            var course = _context.Course.Where(s => s.Id == id).Include(s => s.Students).First();

            if (course == null)
            {
                return NotFound();
            }

            var enrollStudentsVM = new EnrollStudentsViewModel
            {

                Course = course,
                /*CoursesList = new SelectList(_context.Course.OrderBy(s => s.Id), "Id", "Title"),*/
                StudentList = new MultiSelectList(_context.Student.OrderBy(s => s.Id), "Id", "FirstName"),
                SelectedStudents = course.Students.Select(s => s.StudentId),
            };


            return View(enrollStudentsVM);
        }

        [HttpGet]
        public async Task<IActionResult> UnEnrollStudents(int? id)
        {
            var course = _context.Course.Where(s => s.Id == id).Include(s => s.Students).First();

            if (course == null)
            {
                return NotFound();
            }

            var unEnrollStudentVM = new UnEnrollStudentViewModel
            {
                Course = course,
                StudentList = new MultiSelectList(_context.Student.OrderBy(s => s.Id), "Id", "FirstName"),
                SelectedStudents = course.Students.Select(s => s.StudentId),
            };

            return View(unEnrollStudentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnEnrollStudents(int id)
        {

            var enrollments = await _context.Enrollment.FirstOrDefaultAsync(s => s.Id == id);
            await TryUpdateModelAsync<Enrollment>(
               enrollments,
               "",
               s => s.FinishDate);

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enrollments);
        }
    }
}

