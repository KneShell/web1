﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Repositories;
using MyApp.Models;
using MyApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        // GET: /<controller>/

        public HomeController(ITeacherRepository teacherRepository,
            IStudentRepository studentRepository)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            var teachers = _teacherRepository.GetAllTeachers();

            var viewModel = new StudentTeacherViewModels()
            {
                Student = new Student(),
                Teachers = teachers
            };

            return View(viewModel);
        }
        public IActionResult Student()
        {   // Razor 파일 디스플레이

            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher() {Name ="세종대왕", Class="한글"},
                new Teacher() {Name ="이순신", Class="해상전략"},
                new Teacher() {Name ="제갈량", Class="지략"},
                new Teacher() {Name ="을지문덕", Class="지상전략"},
            };

            var viewModel = new StudentTeacherViewModels()
            {
                Student = new Student(),
                Teachers = teachers
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Student(StudentTeacherViewModels model)
        {   // View에서 넘어오는 값들을 받음
            if (ModelState.IsValid)
            {
                // 모델에서 받아온 값에 대한 유효성 검사
                _studentRepository.AddStudent(model.Student);
                _studentRepository.Save();
            }
            return View();
        }
    }
}
