using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100")]
        public int DispayOrder { get; set; }
        public DateTime CreatedDataTime { get; set; } = DateTime.Now;
    }
}
