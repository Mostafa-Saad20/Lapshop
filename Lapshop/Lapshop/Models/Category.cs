using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapshop.Models
{
    public class LapCategory
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [Required]
        public IFormFile? ImageFile { get; set; }
        
        public ICollection<Laptop>? Laptops { get; set; }
    }

    public class AccCategory
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        public string? Image { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [Required]
        public IFormFile? ImageFile { get; set; }

        public ICollection<Accessory>? Accessories { get; set; }
    }
}
