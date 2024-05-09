using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class AutoScheduleController : Controller
    {

        private ApplicationDbContext _db;

        public AutoScheduleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Add()
        {
            @ViewBag.action = "AddPOST";
            ViewBag.employees = _db.employees.ToList();
            return View();
        }


        public List<int> GetAvilableEmployees(DateTime Start,DateTime End)
        {
            var schdules = _db.schedules.Include(x=>x.employee).ToList();
            List<int> list=new List<int>();

            foreach(var obj in schdules)
            {
                var emp = obj.employee;
                foreach(var obj2 in emp)
                {
                    list.Add(obj2.EmployeeID);
                }
            }
            
            return list;
        }

        public IActionResult AddPOST(AutoScheduleModel model)
        {
            int noOFshifts = 0;
            if (model.Shift1Active == true)
            {
                noOFshifts ++;
            }
            if (model.Shift2Active == true)
            {
                noOFshifts++;
            }
            if (model.Shift3Active == true)
            {
                noOFshifts++;
            }

            List<int> list=GetAvilableEmployees(model.StartDate,model.EndDate);

            List<Employee> EligEmp = _db.employees.ToList();
            int empCount = EligEmp.Count;
            int empCurrent = 0;
            List<Employee> Schduleemp = new List<Employee>();

     
                if (model.Shift1Active==true)
                {
                    for(int y = 1; y <= model.Shift1Emp;y++)
                    {
                        Schduleemp.Add(EligEmp[empCurrent]);
                        empCurrent++;

                    }

                    var Schdl = new Schedule
                    {
                        employee = Schduleemp,
                        Date = model.Date,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        StartTime = model.Shift1_StartTime,
                        EndTime = model.Shift1_EndTime,
                        Name=model.Name+"Shift #1"
                    
                    };

                    model.Shift1EmpList = Schduleemp;
            
                }
                if (model.Shift2Active == true)
                {
                    Schduleemp=new List<Employee>();
                    for (int y = 1; y <= model.Shift2Emp; y++)
                    {
                        Schduleemp.Add(EligEmp[empCurrent]);
                        empCurrent++;

                    }

                    var Schdl = new Schedule
                    {
                        employee = Schduleemp,
                        Date = model.Date,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        StartTime = model.Shift1_StartTime,
                        EndTime = model.Shift1_EndTime,
                        Name = model.Name + "Shift #1"

                    };

                    model.Shift2EmpList = Schduleemp;

                }

                if (EligEmp.Count >= empCurrent)
                {
                    empCurrent = 0;
                }
        


            return View("Add",model); 
        }
    }
}
