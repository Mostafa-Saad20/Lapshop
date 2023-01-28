using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lapshop.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s\-*]+", ErrorMessage = "Invalid First Name")]
        public string? FirstName { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s\-*]+", ErrorMessage = "Invalid Last Name")]
        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,12}$", ErrorMessage =
            "Password must be from [6 - 12] characters, and must contain at least One small character," +
            " One Capital character and One Digit number. ")]
        public string? Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password!")]
        public string? ConfirmPassword { get; set; }

        public ICollection<WhishListItem>? WhishListItems { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }

    public class CustomerLogin
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,12}$", ErrorMessage =
            "Password must be from [6 - 12] characters, and must contain at least One small character," +
            " One Capital character and One Digit number. ")]
        public string? Password { get; set; }

    }

    public class CustomerMessage
    {
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }

    }
}
