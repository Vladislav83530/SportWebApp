using System.ComponentModel.DataAnnotations;

namespace SportWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string? UserSurname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль")]
        public string? PasswordConfirm { get; set; }
    }
}
