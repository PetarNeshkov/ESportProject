namespace E_SportManager.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(ESportDbContext dbContext, IServiceProvider serviceProvider);
    }
}
