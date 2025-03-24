using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class AppUser : CommonEntity
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

        [DisplayName("Şifre")]
        public string Password { get; set; }

        [DisplayName("Admin Mi?")]
        public bool IsAdmin { get; set; }

        [DisplayName("Kullanıcı Guid"), ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
    }
}
