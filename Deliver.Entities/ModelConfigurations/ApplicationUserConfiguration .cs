using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deliver.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deliver.Entities.ModelConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //builder.Property(x => x.FirstName)
        //    .IsRequired()
        //        .HasMaxLength(100);
        //builder.Property(x => x.LastName)
        //    .IsRequired()
        //        .HasMaxLength(100);
        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner
            ().HasForeignKey("UserId");
    }
}
