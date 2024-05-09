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

            AutoScheduleModel SchedulePack = new AutoScheduleModel();
            SchedulePack = model;

            List<Employee> EligEmp = _db.employees.ToList();
            int empCount = EligEmp.Count-1;
            int empCurrent = 0;

            List<Employee> Schduleemp = new List<Employee>();

         

            int NoofDaysforSchedule =(int) (model.EndDate - model.StartDate).TotalDays+1;
            
            SchedulePack.ScheduleActiveDate = model.StartDate;

            for (int t = 1; t <= NoofDaysforSchedule; t++)
            {
           

                if (model.Shift1Active == true)
                {
                    for (int y = 1; y <= model.Shift1Emp; y++)
                    {
                        Schduleemp.Add(EligEmp[empCurrent]);
                        empCurrent++;

                        if (empCount < empCurrent)
                        {
                            empCurrent = 0;
                        }


                    }

                  

                    SchedulePack.Shift1EmpList = Schduleemp;

                }

                if (empCount < empCurrent)
                {
                    empCurrent = 0;
                }

                if (model.Shift2Active == true)
                {
                    Schduleemp = new List<Employee>();
                    for (int y = 1; y <= model.Shift2Emp; y++)
                    {
                        Schduleemp.Add(EligEmp[empCurrent]);
                        empCurrent++;
                        if (empCount < empCurrent)
                        {
                            empCurrent = 0;
                        }
                    }

              

                    SchedulePack.Shift2EmpList = Schduleemp;

                }

                if (empCount < empCurrent)
                {
                    empCurrent = 0;
                }
                if (model.Shift3Active == true)
                {
                    Schduleemp = new List<Employee>();
                    for (int y = 1; y <= model.Shift3Emp; y++)
                    {
                        Schduleemp.Add(EligEmp[empCurrent]);
                        empCurrent++;
                        if (empCount < empCurrent)
                        {
                            empCurrent = 0;
                        }

                    }

                   

                    SchedulePack.Shift3EmpList = Schduleemp;

                }

            
                int yy = 0;

                if (SchedulePack.Shift1EmpList != null)
                {
                    if (SchedulePack.Shift1EmpList.Count > 0)
                    {

                        var Schdl = new Schedule
                        {
                            employee = SchedulePack.Shift1EmpList,
                            Date = model.Date,
                            ScheduleActiveDate = SchedulePack.ScheduleActiveDate,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            StartTime = model.Shift1_StartTime,
                            EndTime = model.Shift1_EndTime,
                            Name = model.Name + " Shift #1",
                            scheduleNo = 1,
                            shiftNo = 1

                        };

                        _db.Update(Schdl);
                        _db.SaveChanges();
                    }
                }


                if (SchedulePack.Shift2EmpList != null)
                {
                    if (SchedulePack.Shift2EmpList.Count > 0)
                    {

                        var Schdl = new Schedule
                        {
                            employee = SchedulePack.Shift2EmpList,
                            Date = model.Date,
                            ScheduleActiveDate = SchedulePack.ScheduleActiveDate,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            StartTime = model.Shift2_StartTime,
                            EndTime = model.Shift2_EndTime,
                            Name = model.Name + " Shift #2",
                            scheduleNo = 1,
                            shiftNo = 2

                        };




                        _db.Update(Schdl);
                        _db.SaveChanges();
                    }
                }

                if (SchedulePack.Shift3EmpList != null)
                {

                    if (SchedulePack.Shift3EmpList.Count > 0)
                    {

                        var Schdl = new Schedule
                        {
                            employee = SchedulePack.Shift3EmpList,
                            Date = model.Date,
                            ScheduleActiveDate = SchedulePack.ScheduleActiveDate,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            StartTime = model.Shift3_StartTime,
                            EndTime = model.Shift3_EndTime,
                            Name = model.Name + " Shift #3",
                            scheduleNo = 1,
                            shiftNo = 3

                        };

                        _db.Update(Schdl);
                        _db.SaveChanges();
                    }


                }

                //Reset

                SchedulePack = new AutoScheduleModel();
                Schduleemp = new List<Employee>();
                SchedulePack.ScheduleActiveDate = model.StartDate.AddDays(t);

            }
       


            return View("Add",model); 
        }
    }
}
