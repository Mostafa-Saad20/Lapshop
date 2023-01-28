using System.ComponentModel.DataAnnotations;

namespace Lapshop.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, Enter your Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please, Enter Admin Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } 

        [Required(ErrorMessage = "Please, Enter your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please, Re-Enter your Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }

    public class AdminLogin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, Enter Admin Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please, Enter Admin Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }

    public class AdminEdit
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

    }

}
