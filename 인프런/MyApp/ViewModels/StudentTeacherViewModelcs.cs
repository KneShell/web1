using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.ViewModels
{
    public class StudentTeacherViewModels
    {
        public Student Student { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
