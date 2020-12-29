using Blazor.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(e => e.Id)
                .HasColumnName("OrderId");

            builder
                .HasMany(c => c.Items)
                .WithOne(e => e.Order)
                .IsRequired();

            builder
                .HasOne(e => e.Client)
                .WithMany(e => e.Orders)
                .IsRequired();
        }
    }


}
