using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Student
    {
        [BindNever]
        public string name { get; set; }
        public int Age { get; set; }
        public string country { get; set; }
    }
}
