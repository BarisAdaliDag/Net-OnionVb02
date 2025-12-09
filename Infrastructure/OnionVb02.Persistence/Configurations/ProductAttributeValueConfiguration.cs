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
    public class ProductAttributeValueConfiguration : BaseConfiguration<ProductAttributeValue>
    {
        public override void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductAttributeValues)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductAttribute)
                .WithMany(x => x.ProductAttributeValues)
                .HasForeignKey(x => x.ProductAttributeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.ProductId, x.ProductAttributeId })
                .IsUnique();
        }
    }
}
