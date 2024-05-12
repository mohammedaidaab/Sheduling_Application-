using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using WebApplication1.Models;
using WebApplication1.ViewModel;





using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using WebApplication1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{

    

    public class CalenderController : Controller
    {
        private ApplicationDbContext _db;
        public CalenderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("[controller]/[action]/{customerID?}")]
        public ActionResult Details(string customerId)
        {
 
            return PartialView("Details", _db.schedules.Include(c=>c.employee).Where(x=>x.ScheduleID==int.Parse(customerId)).FirstOrDefault());
        }

    
        public IActionResult Move(int value,int currentMonth)
        {
            int Lastday = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month+value);
            List<DetailsModels> detailPack = new List<DetailsModels>();

      

            for (int x = 1; x <= Lastday; x++)
            {
                List<ScheduleModel> ScheulePAck = new List<ScheduleModel>();
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month+value, x);

                var schdl = _db.schedules.Where(x => x.StartDate <= dt.Date && x.EndDate >= dt.Date).GroupBy(c => c.Name)
    .Select(cs => new { Name = cs.Max(s => s.Name), ScheduleID = cs.Max(s => s.ScheduleID), scheduleNo = cs.Max(s => s.scheduleNo) })
    .ToList();

                foreach (var obj in schdl)
                {
                    ScheduleModel sm = new ScheduleModel
                    {
                        Name = obj.Name,
                        Scheduleid = obj.ScheduleID,
                        scheduleNo = obj.scheduleNo
                    };

                    ScheulePAck.Add(sm);

                }

                DetailsModels dp = new DetailsModels
                {
                    Day = x,
                    scheduleModels = ScheulePAck

                };

                detailPack.Add(dp);


            }




            CalenderModel cm = new CalenderModel
            {
                currentMonth = currentMonth+value,
                currentYear = 2024,
                Time = "12.30 - 01.30",
                lastDay = Lastday,
                Details = detailPack

            };
            

            return View("Index",cm);
        }
        public IActionResult Index()
        {

            int Lastday= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            List<DetailsModels> detailPack = new List<DetailsModels>();
           


			for(int x=1; x <= Lastday; x++)
            {
				List<ScheduleModel> ScheulePAck = new List<ScheduleModel>();
				DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, x);

                //            var schdl = _db.schedules.Where(x => x.StartDate <= dt.Date && x.EndDate >= dt.Date).GroupBy(c => c.Name)
                //.Select(cs => new { Name = cs.Max(s=>s.Name), ScheduleID=cs.Max(s=>s.ScheduleID), scheduleNo=cs.Max(s=>s.scheduleNo) })
                //.ToList();
                var schdl = _db.schedules.Where(x => x.ScheduleActiveDate == dt).GroupBy(x=>x.scheduleNo).Select  (x=>new { ScheduleID = x.Key, scheduleNo=x.Max(y=>y.scheduleNo),Name=x.Max(y=>y.Name) }).ToList();
                foreach (var obj in schdl)
                {
                    ScheduleModel sm = new ScheduleModel { 
                    Name = obj.Name,    
                    Scheduleid=obj.ScheduleID,
                    scheduleNo=obj.scheduleNo
                    };

                    ScheulePAck.Add(sm);
                    
                }

                DetailsModels dp = new DetailsModels
                {
                    Day=x,
                    scheduleModels=ScheulePAck
          
                };

                detailPack.Add(dp);
             
                
            }

			


			CalenderModel cm=new CalenderModel{
                currentMonth=5,
                currentYear=2024,
                Time="12.30 - 01.30",
                lastDay= Lastday,
                Details=detailPack  
                
			};


            return View(cm);
        }
    }
}
