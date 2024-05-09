using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class AutoScheduleModel
    {


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
        public DateTime Shift1_StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime Shift1_EndTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Shift2_StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Shift2_EndTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Shift3_StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Shift3_EndTime { get; set; }

        public int Shift1Emp { get; set; }
        public int Shift2Emp { get; set; }
        public int Shift3Emp { get; set; }

        public bool Shift1Active { get; set; }
        public bool Shift2Active { get; set; }
        public bool Shift3Active { get; set; }

        public List<Employee>? Shift1EmpList { get; set; }
        public List<Employee>? Shift2EmpList { get; set; }
        public List<Employee>? Shift3EmpList { get; set; }


    }
}
