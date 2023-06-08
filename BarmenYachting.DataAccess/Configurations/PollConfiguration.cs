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
    public class PollConfiguration : EntityConfiguration<Poll>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Poll> builder)
        {
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Description).IsRequired();


            builder.HasOne(x => x.Vessel)
                .WithMany(x=>x.Polls)
                .HasForeignKey(x => x.VesselId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x=>x.Polls)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
