using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityCamera.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
               Id = 1,
               Name = "Yalçın GÜNEY",
               Phone1= "1111111111",
               Phone2="22222222222",
               Email="deneme@gmail.com",
               Address= "Örnek mah. Hastane sok. Merkez/Kars",
               Password="2121",
               IsAdmin=true,
               UserGuid=Guid.NewGuid(),
               CreatedDate= DateTime.Now,
            });
        }
    }
}
