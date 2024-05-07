
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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

        public IActionResult UpdatePOST(Schedule model)
        {
          

            ViewBag.action = "UpdatePOST";
            
            List<Employee> data1 = HttpContext.Session.GetComplexData<List<Employee>>("loggerUser");
       


            var data = _db.schedules.Where(x => x.ScheduleID == model.ScheduleID).Select(s => new Schedule
            {
                ScheduleID = s.ScheduleID,
                StartDate = s.StartDate,
                StartTime = s.StartTime,
                action = s.action,
                Date = s.Date,
        employee=s.employee.Where(x=>x.Include(si => si.Slot)).to,
                EmployeeID = s.EmployeeID,
                EndDate = s.EndDate,
                EndTime = s.EndTime,
                Name = s.Name


            }).AsNoTracking().FirstOrDefault();


            _db.schedules.Attach(data);
            data.employee.RemoveAll(x => x.EmployeeID != 0);
            data.employee=data1;

            ViewBag.action = data.action;
       
            _db.SaveChanges();

            ViewBag.employees = _db.employees.AsNoTracking().ToList();

            return View("AddEdit", data);
        }
        public IActionResult Update(int scheduleid)
        {

            ViewBag.action = "UpdatePOST";

            ViewBag.employees = _db.employees.ToList();
            var data = _db.schedules.Where(x => x.ScheduleID == scheduleid).Select(s=>new Schedule
            {
                ScheduleID=s.ScheduleID,
                StartDate=s.StartDate,
                StartTime=s.StartTime,
                action=s.action,
                Date=s.Date,
                employee=s.employee,
                EmployeeID=s.EmployeeID,
                EndDate=s.EndDate,
                EndTime=s.EndTime,
                Name=s.Name
            }).FirstOrDefault();
            data.action = "UpdatePOST";
            HttpContext.Session.SetComplexData("loggerUser", data.employee);
            return View("AddEdit",data);
        }

        [HttpPost]
        public IActionResult delEmp(Schedule model,int empid)
        {
            ViewBag.employees = _db.employees.ToList();
            List<Employee> data1 = HttpContext.Session.GetComplexData<List<Employee>>("loggerUser");
            data1.Remove( data1.Where(x => x.EmployeeID == empid).FirstOrDefault());
            model.employee = data1;
            ViewBag.action = model.action;
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
            HttpContext.Session.SetComplexData("loggerUser", null);
            var data = _db.schedules.ToList();
            return View(data);
        }
    }
}
