using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapshop.Models
{
    public class Laptop
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Image { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? Images { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "RAM Size (in GB)")]
        public decimal RAMSize { get; set; }

        [Required]
        [Display(Name = "RAM Type")]
        public string? RAMType { get; set; }

        [Required]
        [Display(Name = "Processor Brand")]
        public string? ProcessorBrand { get; set; }

        [Required]
        [Display(Name = "Processor Type")]
        public string? ProcessorType { get; set; }

        [Required]
        [Display(Name = "CPU Max Speed (in GHz)")]
        public decimal CPU { get; set; }

        [Required]
        [Display(Name = "HDD Size")]
        public string? HDD { get; set; }

        [Display(Name = "SSD Available")]
        public bool HasSSD { get; set; }

        [Display(Name = "SSD Size (in GB)")]
        public decimal? SSD { get; set; }

        [Required]
        [Display(Name = "Graphics Card Brand")]
        public string? GPUBrand { get; set; }

        [Required]
        [Display(Name = "Graphics Card Size (in GB)")]
        public decimal? GPU { get; set; }

        [Required]
        [Display(Name = "Display Size (in Inch)")]
        public decimal? DisplaySize { get; set; }

        [Required]
        [Display(Name = "Operating System")]
        public string? OS { get; set; }

        [Display(Name = "Laptop Width (in cm)")]
        public decimal? Width { get; set; }

        [Display(Name = "Laptop Length (in cm)")]
        public decimal? Length { get; set; }

        [Display(Name = "Laptop Weight (in Kgm)")]
        public decimal? Weight { get; set; }

        [Required]
        [Display(Name = "Laptop Colour")]
        public string? Color { get; set; }

        public DateTime SoldAt { get; set; } = DateTime.Now;

        // .. Navigation ..
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }

        [Display(Name = "Select Category")]
        public int? CategoryId { get; set; }
        public LapCategory? Category { get; set; }

        [Display(Name = "Select Brand")]
        public int? BrandId { get; set; }
        public LapBrand? Brand { get; set; }

        public ICollection<LaptopImages>? LaptopImages { get; set; }

    }


    public class LaptopImages
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }

        public int LapId { get; set; }
        public Laptop? Laptop { get; set; }

    }

    public class LaptopReviews
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal? Rate { get; set; }
        public string? Comment { get; set; }
        public int? LapId { get; set; }
        
    }

}
