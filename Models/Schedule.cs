using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
       
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [NotMapped]
        public int  EmployeeID { get; set; }

        [ForeignKey("ScheduleID")]
        public List<Employee> employee { get; set; }

        [NotMapped]
        public string action { get; set; }

    }
}
