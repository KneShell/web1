using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        [Range(15, 70)]
        public int age { get; set; }
        [Required, MinLength(5)]
        public string country { get; set; }
    }
}
