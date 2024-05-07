using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext _db;
        public ScheduleController(ApplicationDbContext db)
        {
                _db=db; 
        }


        public IActionResult Add()
        {
            @ViewBag.action = "AddPOST";
            ViewBag.employees = _db.employees.ToList();
            return View("AddEdit");

        }
        public IActionResult Index()
        {
            var data = _db.schedules.ToList();
            return View(data);
        }
    }
}
