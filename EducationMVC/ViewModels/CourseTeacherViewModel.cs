using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MVCEdu.Models;

namespace MVCEdu.ViewModels
{
    public class CourseTeacherViewModel
    {
        public string SearchTitle { get; set; }
        public string SearchSemester { get; set; }
        public string SearchProgramme { get; set; }

        public IList<Course> Courses { get; set; }

        public IList<Teacher> Teachers { get; set; }

    }
}
