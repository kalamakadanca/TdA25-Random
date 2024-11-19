using System.ComponentModel.DataAnnotations;

namespace TourDeApp.Models.Schemas
{
    public class Error
    {
        [Required]
        public int Code { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
