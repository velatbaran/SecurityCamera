using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class Comment : CommonEntity
    {
        [DisplayName("Adı"), StringLength(50), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Name { get; set; }

        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Description { get; set; }

        [DisplayName("Onaylandı Mı?")]
        public bool IsApproved { get; set; }

        [ScaffoldColumn(false)]
        public int? GaleryId { get; set; }
        public Galery? Galery { get; set; }
    }
}
