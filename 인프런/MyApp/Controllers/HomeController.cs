using System;
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

            var students = _studentRepository.GetAllStudent();

            var viewModel = new StudentTeacherViewModels()
            {
                Student = new Student(),
                Students = students
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

            var students = _studentRepository.GetAllStudent();

            var viewModel = new StudentTeacherViewModels()
            {
                Student = new Student(),
                Students = students
            };

            ModelState.Clear();
            return View(viewModel);
        }

        public IActionResult Detail(int id)
        {
            var result = _studentRepository.GetStudent(id);
            return View(result);
        }
    }
}
