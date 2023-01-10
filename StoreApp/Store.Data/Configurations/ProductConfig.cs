using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(20);
            builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.SalePrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DiscountPercent).HasColumnType("decimal(18,2)");
            builder.Property(x => x.StockStatus);
            builder.Property(x => x.ImgUrl).IsRequired(true).HasMaxLength(100);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
