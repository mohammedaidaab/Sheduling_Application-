
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {

        private ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            var data = _db.employees.ToList();
            return View(data);
        }

        public IActionResult Add()
        {
            @ViewBag.action = "AddPOST";
      
            return View("AddEdit");  
    
        }

        public IActionResult Update(int empid)
        {
            @ViewBag.action = "UpdatePOST";
            var data = _db.employees.Where(x => x.EmployeeID == empid).FirstOrDefault();

            return View("AddEdit",data);

        }

        [HttpPost]

        public IActionResult AddPOST(Employee model)
        {
            _db.Add(model);
            _db.SaveChanges();
            @ViewBag.action = "Add";
            return RedirectToAction("Index");
        }


        [HttpPost]

        public IActionResult UpdatePOST(Employee model)
        {
            _db.Update(model);
            _db.SaveChanges();
      
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int empid)
        {

            var data = _db.employees.Where(x => x.EmployeeID == empid).FirstOrDefault();
            _db.Remove(data);
            _db.SaveChanges();
       
            return RedirectToAction("Index");
        }
    }
}
