namespace Lapshop.Models
{
    public class WhishListItem
    {
        public int Id { get; set; }

        public string? ItemName { get; set; }
		public decimal? ItemPrice { get; set; }
		public string? ItemImage { get; set; }

		public int? LaptopId { get; set; }
        public Laptop? Laptop { get; set; }

        public int? AccessoryId { get; set; }
        public Accessory? Accessory { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


    }
}
