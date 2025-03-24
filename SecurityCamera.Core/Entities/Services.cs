using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class Services : CommonEntity
    {
        [DisplayName("Başlık"), StringLength(100), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Title { get; set; }

        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Description { get; set; }

    }
}
