using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{

    

    public class CalenderController : Controller
    {
        private ApplicationDbContext _db;
        public CalenderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            int Lastday= DateTime.DaysInMonth(2024, 05);
            List<DetailsModels> detailPack = new List<DetailsModels>();
           


			for(int x=1; x <= Lastday; x++)
            {
				List<ScheduleModel> ScheulePAck = new List<ScheduleModel>();
				DateTime dt = new DateTime(2024, 5, x);

                var schdl = _db.schedules.Where(x => x.StartDate <= dt.Date && x.EndDate >= dt.Date).ToList();

                foreach(var obj in schdl)
                {
                    ScheduleModel sm = new ScheduleModel { 
                    Name = obj.Name,    
                    Scheduleid=obj.ScheduleID
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
