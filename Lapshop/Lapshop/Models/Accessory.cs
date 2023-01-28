using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapshop.Models
{
    public class Accessory
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

        public DateTime SoldAt { get; set; } = DateTime.Now;

        // .. Navigation ..
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }

        public int? CategoryId { get; set; }
        public AccCategory? Category { get; set; }

        public int? BrandId { get; set; }
        public AccBrand? Brand { get; set; }

        public ICollection<AccessoryImages>? AccessoryImages { get; set; }
	}

    public class AccessoryImages
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }

        public int AccId { get; set; }
        public Accessory? Accessory { get; set; }

    }

	public class AccessoryReviews
	{
		public int Id { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerEmail { get; set; }
		public decimal? Rate { get; set; }
		public string? Comment { get; set; }

		public int? AccId { get; set; }
	}
}
