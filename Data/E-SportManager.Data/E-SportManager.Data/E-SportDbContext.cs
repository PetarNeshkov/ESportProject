using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_SportManager.Data
{
    public class ESportDbContext : IdentityDbContext
    {
        public ESportDbContext(DbContextOptions<ESportDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; init; }
        public DbSet<Player> Players { get; init; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => SaveChangesAsync(cancellationToken);


        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //Disable cascade delete
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys()
                .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}