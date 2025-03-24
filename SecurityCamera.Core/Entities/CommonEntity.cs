using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityCamera.Core.Entities
{
    public class CommonEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
