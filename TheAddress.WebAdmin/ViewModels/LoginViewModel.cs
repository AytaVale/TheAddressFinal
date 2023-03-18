using System.ComponentModel.DataAnnotations;

namespace TheAddress.WebAdmin.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email adresiniz")]
        [Required(ErrorMessage = "Xananı doldurun")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifrəniz")]
        [Required(ErrorMessage = "Xananı doldurun")]

        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifrədə ən az 4 simvol olmalıdır")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
