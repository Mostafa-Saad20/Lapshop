namespace Lapshop.Models
{
    public class LapBrand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Nationality { get; set; }

        public ICollection<Laptop>? Laptops { get; set; }
    }

    public class AccBrand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Nationality { get; set; }

        public ICollection<Accessory>? Accessories { get; set; }
    }
}
