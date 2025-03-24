using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SecurityCamera.WebUI.Areas.Admin.Models
{
    public class MyPasswordChangeViewModel
    {
        [DisplayName("Şifre"), DataType(DataType.Password), StringLength(15), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), DataType(DataType.Password), StringLength(15), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string RePassword { get; set; }
    }
}
