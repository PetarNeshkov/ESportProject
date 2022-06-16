namespace E_SportManager.Models.Players
{
    public class PlayerDetailsViewModel:PlayerServiceModel
    {
        public string CreatedOn { get; init; }

        public int YearsOfExperience { get; init; }

        public string Description { get; init; }
    }
}
