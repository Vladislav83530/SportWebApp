using System.ComponentModel.DataAnnotations;

namespace SportWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect address")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string? UserSurname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not remember")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? PasswordConfirm { get; set; }
    }
}
