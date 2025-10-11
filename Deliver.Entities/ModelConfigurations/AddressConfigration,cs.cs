using Deliver.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.ModelConfigurations
{
    public class AddressConfigration_cs : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
               builder.HasIndex(a => new { a.UserId, a.StreetId })
                    .IsUnique();
        }
    }

    public class governmentConfigration_cs : IEntityTypeConfiguration<Government>
    {
        public void Configure(EntityTypeBuilder<Government> builder)
        {
                builder.HasIndex(g => g.Name)
                      .IsUnique(); 

            builder.HasIndex(c => new { c.Name, c.Id })
                .IsUnique();

 
        }
    }
    public class cityConfigration_cs : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasIndex(g => g.Name)
                  .IsUnique();

            builder.HasIndex(c => new { c.Name, c.GovernmentId })
                    .IsUnique();
        }
    }
    public class ZoneConfigration_cs : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasIndex(g => g.Name)
                  .IsUnique();

            builder.HasIndex(c => new { c.Name, c.CityId })
                    .IsUnique();
        }
    }
    public class streetConfigration_cs : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasIndex(g => g.Name)
                  .IsUnique();

            builder.HasIndex(c => new { c.Name, c.ZoneId })
                    .IsUnique();
        }
    }

}
