using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCEdu.Models;

namespace MVCEdu.ViewModels
{
    public class EnrollStudentsViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<int?> SelectedStudents { get; set; }
        public IEnumerable<SelectListItem?> StudentList { get; set; }
        public IEnumerable<int?> CourseId { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }

    }
}