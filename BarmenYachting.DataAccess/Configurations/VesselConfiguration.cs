using BarmenYachting.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.DataAccess.Configurations
{
    public class VesselConfiguration : EntityConfiguration<Vessel>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Vessel> builder)
        {
            builder.Property(x => x.Model).HasMaxLength(50).IsRequired();
        }
    }
}
