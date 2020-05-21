using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCEdu.Models;

namespace MVCEdu.ViewModels
{

    public class AuthTeacherViewModel
    {
        public string nameTeacher { get; set; }
        public Student Student { get; set; }
        public Enrollment Enrollment { get; set; }
        public Course Course { get; set; }
        public int? StudentId { get; set; }
        public IList<Student> StudentList { get; set; }
        public IList<Course> CourseList { get; set; }
    }
}
