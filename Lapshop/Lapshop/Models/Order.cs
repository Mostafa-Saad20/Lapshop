using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace Lapshop.Models
{
	public class Order
	{
		public int Id { get; set; }

		[Required]
		[RegularExpression(@"[a-zA-Z\s\-*]+", ErrorMessage = "Invalid First Name")]
		public string? FirstName { get; set; }

		[Required]
		[RegularExpression(@"[a-zA-Z\s\-*]+", ErrorMessage = "Invalid Last Name")]
		public string? LastName { get; set; }

		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email address")]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"[0-9]{11}$", ErrorMessage = "Invalid Phone number")]
		public string? Phone { get; set; }

		[Required]
		public string? Address1 { get; set; }
		
		public string? Address2 { get; set; }

		public string? ZIPCode { get; set; }

		[Required]
		public string? PaymentMethod { get; set; }

		public DateTime Date { get; set; } = DateTime.Today.Date;

		public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;

		public int? CustomerId { get; set; }
		public Customer? Customer { get; set; }

	}
}
