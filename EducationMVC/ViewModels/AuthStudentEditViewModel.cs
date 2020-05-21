using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCEdu.ViewModels
{
    public class AuthStudentEditViewModel
    {
        public string ProjectUrl { get; set; }
        public string PURL { get; set; }
        public int enrollId { get; set; }
        
        public IFormFile project { get; set; }
    }
}
