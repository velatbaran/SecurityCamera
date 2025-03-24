using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Core.Entities
{
    public class Galery : CommonEntity
    {
        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Description { get; set; }

        [DisplayName("Resim-1")]
        public string? Image1 { get; set; }

        [DisplayName("Resim-2")]
        public string? Image2 { get; set; }

        [DisplayName("Resim-3")]
        public string? Image3 { get; set; }

        [DisplayName("Resim-4")]
        public string? Image4 { get; set; }

        [DisplayName("Resim-5")]
        public string? Image5 { get; set; }

        [DisplayName("İş Adı"), StringLength(100), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string WorkingName { get; set; }

        [DisplayName("Kategori"), StringLength(100), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Category { get; set; }

        [DisplayName("Müşteri"), StringLength(100), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Client { get; set; }

        [DisplayName("İş Tarihi"),Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public DateTime WorkingDate { get; set; }

        public IList<Comment>? Comments { get; set; }
    }
}
