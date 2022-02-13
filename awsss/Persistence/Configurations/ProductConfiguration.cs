using awsss.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Price).IsRequired();
            builder.Property(r => r.IsFree).IsRequired().HasDefaultValue(false);
        }
    }
}
