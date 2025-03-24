using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class Contact : CommonEntity
    {
        [DisplayName("Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Name { get; set; }

        public string? Email { get; set; }

        [DisplayName("Telefon"),StringLength(15)]
        public string? Phone { get; set; }

        [DisplayName("Konu"), StringLength(100), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Subject { get; set; }

        [DisplayName("Mesaj"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Message { get; set; }

    }
}
