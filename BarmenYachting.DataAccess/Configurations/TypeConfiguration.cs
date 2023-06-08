using BarmenYachting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.DataAccess.Configurations
{
    public class TypeConfiguration : EntityConfiguration<Domain.Type>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Domain.Type> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Vessels)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
