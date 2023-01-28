namespace Lapshop.Models
{
	public class Comparison
	{
		public int Id { get; set; }

		public string? UniqueProperty { get; set; }

		public int? LaptopId { get; set; }

		public int? AccessoryId { get; set; }

		public string? Name { get; set; }

		public string? Image { get; set; }

		public string? Description { get; set; }

		public decimal Price { get; set; }

		public decimal RAMSize { get; set; }

		public string? RAMType { get; set; }

		public string? ProcessorBrand { get; set; }

		public string? ProcessorType { get; set; }

		public decimal CPU { get; set; }

		public string? HDD { get; set; }

		public bool HasSSD { get; set; }

		public decimal? SSD { get; set; }

		public string? GPUBrand { get; set; }

		public decimal? GPU { get; set; }

		public decimal? DisplaySize { get; set; }

		public string? OS { get; set; }

		public decimal? Width { get; set; }

		public decimal? Length { get; set; }

		public decimal? Weight { get; set; }

		public string? Color { get; set; }

	}
}
