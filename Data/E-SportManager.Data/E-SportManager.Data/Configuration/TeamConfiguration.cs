using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static E_SportManager.Data.Common.DataValidation.Team;

namespace E_SportManager.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team
                .Property(t => t.Title)
                .HasMaxLength(TeamNameMaxLength)
                .IsRequired();

            team
                .HasOne(p=>p.MidLaner)
                .WithMany(t=>t.MidLaners)
                .HasForeignKey(t=>t.MidLanerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            team
               .HasOne(p => p.TopLaner)
               .WithMany(t => t.TopLaners)
               .HasForeignKey(t => t.TopLanerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            team
               .HasOne(p => p.JungleLaner)
               .WithMany(t => t.JungleLaners)
               .HasForeignKey(t => t.JungleLanerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            team
               .HasOne(p => p.BottomLaner)
               .WithMany(t => t.BottomLaners)
               .HasForeignKey(t => t.BottomLanerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            team
               .HasOne(p => p.SupportLaner)
               .WithMany(t => t.SupportLaners)
               .HasForeignKey(t => t.SupportLanerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
