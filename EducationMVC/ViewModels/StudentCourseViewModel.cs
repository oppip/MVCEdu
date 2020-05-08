using MVCEdu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MVCEdu.ViewModels
{
    public class StudentCourseViewModel
    {
        public IList<Student> Students { get; set; }
        public string SearchStudentId { get; set; }
        public string SearchFirstName { get; set; }
        public string SearchLastName { get; set; }
    }
}
