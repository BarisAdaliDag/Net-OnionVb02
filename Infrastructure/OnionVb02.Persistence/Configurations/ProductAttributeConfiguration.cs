using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Persistence.Configurations
{
    public class ProductAttributeConfiguration : BaseConfiguration<ProductAttribute>
    {
        public override void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.AttributeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.AttributeType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasIndex(x => x.AttributeName)
                .IsUnique();

            builder.HasMany(x => x.ProductAttributeValues)
                .WithOne(x => x.ProductAttribute)
                .HasForeignKey(x => x.ProductAttributeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}