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
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.Property(e => e.Id)
                .HasColumnName("OrderItemId");

            builder.Property(e => e.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Total)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .IsRequired();
        }
    }


}
