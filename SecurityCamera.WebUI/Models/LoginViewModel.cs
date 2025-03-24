using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SecurityCamera.WebUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Lütfen gerekli alanları doldurunuz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen gerekli alanları doldurunuz")]
        [DisplayName("Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
