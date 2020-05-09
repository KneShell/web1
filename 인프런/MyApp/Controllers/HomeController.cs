using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Student()
        {   // Razor 파일 디스플레이
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Student(Student model)
        {   // View에서 넘어오는 값들을 받음
            if (ModelState.IsValid)
            {
                // 모델에서 받아온 값에 대한 유효성 검사
            }
            return View();
        }
    }
}
