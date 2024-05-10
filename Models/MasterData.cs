using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MasterData
    {
        [Key]
        public int id { get; set; }

        public int scheduleNo { get; set; }

    }
}
