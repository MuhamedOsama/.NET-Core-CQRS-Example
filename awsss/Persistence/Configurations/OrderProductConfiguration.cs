using awsss.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Persistence.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(bc => new { bc.ProductId,bc.OrderId });
            builder.HasOne(bc => bc.Order).WithMany(b => b.OrderProducts).HasForeignKey(bc => bc.OrderId);
            builder.HasOne(bc => bc.Product).WithMany(c => c.ProductOrders).HasForeignKey(bc => bc.ProductId);
        }
    }
}
