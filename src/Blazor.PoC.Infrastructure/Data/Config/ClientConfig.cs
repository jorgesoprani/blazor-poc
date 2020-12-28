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
    class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ClientId");

            builder.Property(e => e.Name).HasMaxLength(100);

            builder.Property(e => e.Country).HasMaxLength(100);
        }
    }


}
