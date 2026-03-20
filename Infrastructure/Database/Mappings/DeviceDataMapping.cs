using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class DeviceDataMapping : IEntityTypeConfiguration<DeviceData>
    {
        public void Configure(EntityTypeBuilder<DeviceData> entity)
        {



            entity.HasKey(e => e.Id)
                .HasName("PRIMARY");

            entity.ToTable("tb_device");

            entity.Property(e => e.Id)
                .HasColumnName("id_device")
                .HasColumnType("varchar(36)")
                .IsRequired();

            entity.Property(e => e.Brand)
                .HasColumnName("brand")
                .HasColumnType("varchar(36)");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(36)");

            entity.Property(e => e.State)
             .HasColumnName("state")
             .HasColumnType("varchar(9)");

            entity.Property(e => e.CreationTime)
            .HasColumnName("creation_time")
            .HasColumnType("datetime");
        }
    }
}
