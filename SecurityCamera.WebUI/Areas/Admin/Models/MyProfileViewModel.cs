using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SecurityCamera.WebUI.Areas.Admin.Models
{
    public class MyProfileViewModel
    {

        [DisplayName("Adı"), StringLength(50), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), StringLength(50), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Email { get; set; }

        [DisplayName("Telefon-1"), StringLength(15), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Phone1 { get; set; }

        [DisplayName("Telefon-2"), StringLength(15), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string? Phone2 { get; set; }

        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Address { get; set; }

        [DisplayName("Admin Mi?")]
        public bool IsAdmin { get; set; }
    }
}
