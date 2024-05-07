
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext _db;
        public ScheduleController(ApplicationDbContext db)
        {
                _db=db; 
        }

        public IActionResult AddEmp(Schedule model)
        {
            List<Employee> data1 = HttpContext.Session.GetComplexData<List<Employee>>("loggerUser");
            model.employee = data1;

            if (model.employee == null)
            {
                var data = _db.employees.Where(x => x.EmployeeID == model.EmployeeID).ToList();
                model.employee = data;
            }
            else
            {
                var data = _db.employees.Where(x => x.EmployeeID == model.EmployeeID).FirstOrDefault();
                model.employee.Add(data);
            }

            HttpContext.Session.SetComplexData("loggerUser", model.employee);
            ViewBag.action = model.action;
            ViewBag.employees = _db.employees.ToList();
            return View("AddEdit",model);
        }

        [HttpPost]
        public IActionResult delEmp(Schedule model,int empid)
        {
            ViewBag.employees = _db.employees.ToList();
            List<Employee> data1 = HttpContext.Session.GetComplexData<List<Employee>>("loggerUser");
            data1.Remove( data1.Where(x => x.EmployeeID == empid).FirstOrDefault());
            model.employee = data1;
            HttpContext.Session.SetComplexData("loggerUser", model.employee);
            return View("AddEdit",model);
        }
        public IActionResult Add()
        {
            @ViewBag.action = "AddPOST";
            ViewBag.employees = _db.employees.ToList();

            return View("AddEdit");

        }

        public IActionResult AddPOST(Schedule model)
        {
            model.employee= HttpContext.Session.GetComplexData<List<Employee>>("loggerUser");

            _db.Update(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var data = _db.schedules.ToList();
            return View(data);
        }
    }
}
