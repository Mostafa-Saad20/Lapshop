namespace Lapshop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Image { get; set; }

        public int? LaptopId { get; set; }
		public Laptop? Laptop { get; set; }

		public int? AccessoryId { get; set; }
		public Accessory? Accessory { get; set; }

		public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

    }
}
