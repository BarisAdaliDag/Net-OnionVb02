using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Persistence.Configurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.AppUserProfile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.AppUserId); //Burada endişelenmeyin bu noktada HasOne ve WithOne metotları kullanıldıgı icin Foreign Key olsa bile otomatik birebir ve unique bir ilişki kurulacak...Ve aynı zamanda Id management serbestligi de kazanacaksınız...

        
        }
    }
}
