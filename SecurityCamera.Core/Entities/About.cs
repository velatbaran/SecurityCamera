using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class About : CommonEntity
    {
        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Description { get; set; }

        [DisplayName("Resim")]
        public string? Image { get; set; }

    }
}
