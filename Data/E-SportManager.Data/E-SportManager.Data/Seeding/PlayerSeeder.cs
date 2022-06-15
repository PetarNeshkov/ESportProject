using E_SportManager.Data.enums;
using Microsoft.EntityFrameworkCore;

using static E_SportManager.Common.GlobalConstants.Administrator;

namespace E_SportManager.Data.Seeding
{
    public class PlayerSeeder : ISeeder
    {
        public async Task SeedAsync(ESportDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Players.AnyAsync())
            {
                return;
            }

            var adminId = await dbContext.Users
                            .Where(u=>u.UserName== AdministratorUsername)
                            .Select(u=>u.Id)
                            .FirstOrDefaultAsync();

            var createdOn= DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy H:mm");

            var players = new List<Player>()
            {
                new Player()
                {
                    Name="Faker",
                    ImageUrl=@"https://upload.wikimedia.org/wikipedia/commons/e/e6/Faker_at_the_2015.10.02_S5_Paris_day2.jpeg",
                    YearsOfExperience=8,
                    Role="Mid",
                    Division= "Challenger",
                    Description="Lee Sang-hyeok (Korean: 이상혁; born May 7, 1996), better known as Faker (Korean: 페이커), is a South Korean professional League of Legends player for T1. Formerly known as GoJeonPa (Korean: 고전파) on the Korean server, he was picked up by LCK team SK Telecom T1 in 2013 and has played as the team's mid laner since. " +
                    "He is widely considered to be the best League of Legends player of all time.Lee is renowned for his high mechanical skill and extremely versatile champion pool.He is best known for playing LeBlanc, Zed, Syndra, Azir, Ahri, and Ryze.He is the first player to have reached both 1, 000 and 2, 000 kills in the LCK, as well as first to have played 600 and 700 games.",
                    IsPublic=true,
                    AuthorId= adminId,
                    CreatedOn= createdOn
                },
                new Player()
                {
                    Name="Wild Turtle",
                    ImageUrl=@"https://th.bing.com/th/id/R.b8ab73f26bf83d7f2ca08e59e24fec77?rik=5PyltfN%2bk9owFg&pid=ImgRaw&r=0",
                    YearsOfExperience=7,
                    Role="Bottom",
                    Division="GrandMaster",
                    Description="Jason Tran, better known by his in-game name WildTurtle, is a Canadian professional League of Legends player who is the bot laner for Counter Logic Gaming of the LCS. He previously played for Cloud9, Team SoloMid, Immortals, and FlyQuest. WildTurtle played in the 2013, 2014, 2015, and 2020 World Championships.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn= createdOn
                },
                new Player()
                {
                    Name="Dyrus",
                    ImageUrl=@"https://gamepedia.cursecdn.com/twitch_gamepedia/9/9a/Tsm_dyrus.png",
                    YearsOfExperience=6,
                    Role= "Top",
                    Division="Platinium",
                    Description=@"Marcus ""Dyrus"" Hill is a retired League of Legends esports player, previously top laner for Delta Fox. He is best known for being the top laner for TSM.",
                    IsPublic=true,
                    AuthorId = adminId,
                    CreatedOn = createdOn
                },
                new Player()
                {
                    Name ="Hylissang",
                    ImageUrl=@"https://th.bing.com/th/id/OIP.sOtLXdF5XyOAZTwSSTi1rAHaE8?pid=ImgDet&rs=1",
                    YearsOfExperience=5,
                    Role= "Support",
                    Division= "Challenger",
                    Description=@"Zdravets ""Hylissang"" (listen) Iliev Galabov (Cyrillic: Здравец Илиев Гълъбов) is a League of Legends esports player, currently support for Fnatic. Born in Sofia, Hylissang has been playing video games since 2004. In 2006, he started playing Heroes of Might and Magic III, then in 2008 he started playing World of Warcraft: The Burning Crusade. In 2011, he moved onto League of Legends.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn= createdOn
                },
                new Player()
                {
                    Name="TheOddOne",
                    ImageUrl=@"https://i.ytimg.com/vi/U1tkIPhVciI/hqdefault.jpg",
                    YearsOfExperience=4,
                    Role= "Jungle",
                    Division="Diamond",
                    Description=@"Brian ""TheOddOne"" Wyllie is a League of Legends esports player, currently streamer for TSM. He is best known for his time as jungler for TSM. TheOddOne has played video games for most of his life. He used to play on the Sega Genesis then moved from console to PC, playing Starcraft, Warcraft 3, and Phantasy Star Online, until he moved onto League of Legends. Other games he enjoys include the Mass Effect series, Left 4 Dead 2, and Plants vs Zombies. He started his League of Legends career with Haters Make Us Famous before moving on to Team SoloMid as their designated jungler. He is from Calgary, Alberta, Canada. Throughout Season 1 he was regarded as one of the best junglers in the game.",
                    IsPublic=true,
                    AuthorId= adminId,
                    CreatedOn=createdOn
                },
                new Player()
                {
                    Name="Tyler1",
                    ImageUrl =@"https://external-preview.redd.it/fbYkycgPr1_tupCyCiakwmenaKyL3uGVuiMI9qrOca8.jpg?auto=webp&s=6df6466884612ef0fcbc7e1aa9fd1b318e9cd75d",
                    YearsOfExperience=3,
                    Role="Bottom",
                    Division ="Challenger",
                    Description=@"Tyler Steinkamp (born March 7, 1995), better known as tyler1 (T1 or TT for short), is an American internet personality and streamer on Twitch. He is one of the most popular League of Legends online personalities with more than 5 million followers on Twitch. Steinkamp was banned from playing League of Legends from April 2016 to January 2018 for disruptive behavior towards other players, earning him the nickname of ""The Most Toxic Player in North America"". His first League of Legends stream after reinstatement peaked at over 382,000 viewers on Twitch, a figure that was noted as the website's largest non-tournament concurrent viewership at the time. In October 2020, he was signed by the South Korean esports team T1 as a content creator.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn=createdOn
                },
                new Player()
                {
                    Name="aphromoo",
                    ImageUrl=@"https://th.bing.com/th/id/OIP.sntdQ1vWEexfZ4j6q1tjdgHaF3?pid=ImgDet&rs=1",
                    YearsOfExperience=1,
                    Role="Support",
                    Division="Gold",
                    Description=@"Zaqueri ""aphromoo"" Black is a League of Legends esports player, currently support for FlyQuest.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn=createdOn
                },
                new Player()
                {
                    Name="Canyon",
                    ImageUrl=@"https://media.trackingthepros.com/profile/p700.png",
                    YearsOfExperience=1,
                    Role="Jungle",
                    Division="GrandMaster",
                    Description=@"Kim “Canyon” Geon-bu comes from South Korea and he’s a professional jungle player for the team “DAMWON Gaming”. Sadly he got second place in the Worlds 2021 Championship but don’t think that’s no small feat.
                        Even getting into the top 5 is something huge. This player showed what it really means to be the best jungler of League of Legends. He started this year strong and ended it on a strong note and he doesn’t show any signs of stopping.
                        Not only that but Canyon has also won two LCK Tournaments this year back to back showing everyone he’s someone not to be messed with and people should fear him! He’s the first player ever to get a penta kill in the LCK on the 24th of July 2020. Canyon is often nicknamed “Polar Bear” by his teammates and fans.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn=createdOn
                },
                new Player()
                {
                    Name="Box box",
                    ImageUrl=@"https://images.chesscomfiles.com/uploads/v1/master_player/0cc91c7c-05e9-11ec-874c-ffd1f6641467.a613f392.250x250o.51f0076c613f.png",
                    YearsOfExperience=10,
                    Role="Top",
                    Division="Silver",
                    Description=@"Albert ""잘못"" Zheng, better known as ""BoxBox"" began playing League of Legends during its beta. Though he originally played PoppySquare.pngPoppy, he eventually began to consider RivenSquare.pngRiven his favorite champion. Zheng reached prominence primarily due to advertisement of his stream through reddit, and streaming remains his primary claim to fame. On April 24, 2013, BoxBox joined Velocity eSports as a substitute player, and he remained with the organization until August 2013. Although he never went pro, he has attended 3 Twitch Rivals tournaments. BoxBox also likes to do a lot of cosplays, some of the most famous are the Arcade Riven, Star Guardian Soraka, KDA Akali and Battle Bunny Riven. All this success has yielded him a good audience, as of July 2021, his Twitch channel has over 1,9 million followers. ",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn=createdOn
                },
                new Player()
                {
                    Name = "Bjergsen",
                    ImageUrl= @"https://www.kindpng.com/picc/m/230-2303802_tsm-bjergsen-2019-summer-sren-bjerg-hd-png.png",
                    YearsOfExperience= 9,
                    Role= "Mid",
                    Division="Challenger",
                    Description=@"Bjergsen debuted on the Western Wolves team and played a short stint on Team LDLC.com. Ahead of the 2013 season, Bjergsen joined Copenhagen Wolves, resulting in a 5-6th place finish in the 2013 EU LCS Spring Season. Ahead of Summer 2013, Bjergsen moved to Ninjas in Pyjamas, once again placing 5-6th. He joined TSM for the 2014 NA LCS Spring Season, resulting in a total of 6 NA LCS titles. He announced his retirement on October 24th, 2020, becoming TSM's head coach. After 8 years with the organisation, Bjergsen left TSM with the intention of returning to pro play.",
                    IsPublic=true,
                    AuthorId=adminId,
                    CreatedOn=createdOn
                }
            };
            await dbContext.Players.AddRangeAsync(players);
        }
    }
}
