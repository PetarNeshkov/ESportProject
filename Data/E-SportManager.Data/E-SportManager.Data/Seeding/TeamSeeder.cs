using Microsoft.EntityFrameworkCore;

using static E_SportManager.Common.GlobalConstants.Administrator;

namespace E_SportManager.Data.Seeding
{
    public class TeamSeeder : ISeeder
    {
        public async Task SeedAsync(ESportDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Teams.AnyAsync())
            {
                return;
            }

            var adminId= await dbContext.Users
                            .Where(u=>u.UserName==AdministratorUsername)
                            .Select(u=>u.Id)
                            .FirstOrDefaultAsync();

            var createdOn = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy H:mm");

            var teams=new List<Team>()
            {
                new Team()
                {
                    Title= "International Baddies",
                    ImageUrl=@"https://upload.wikimedia.org/wikipedia/en/thumb/f/f9/T1_logo.svg/1200px-T1_logo.svg.png",
                    Rating= 0,
                    MidLanerId= 1,
                    TopLanerId= 3,
                    BottomLanerId=2,
                    SupportLanerId=4,
                    JungleLanerId=5,
                    AuthorId=adminId,
                    CreatedOn= createdOn,
                },
                new Team()
                {
                    Title ="Omega Five",
                    ImageUrl =@"https://cdn.wionews.com/sites/default/files/styles/story_page/public/2018/09/12/31859-alien1-20171103111635.jpg",
                    Rating=0,
                    MidLanerId=10,
                    TopLanerId= 9,
                    BottomLanerId=6,
                    SupportLanerId=7,
                    JungleLanerId=8,
                    AuthorId=adminId,
                    CreatedOn = createdOn
                }
            };

            await dbContext.Teams.AddRangeAsync(teams);
        }
    }
}
