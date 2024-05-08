using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
	public class CalenderModel
	{
        public int currentYear { get; set; }
        public int currentMonth { get; set; }
        public int lastDay { get; set; }
        public string Time { get; set; }
        public List<DetailsModels> Details { get; set; }
    }
}
