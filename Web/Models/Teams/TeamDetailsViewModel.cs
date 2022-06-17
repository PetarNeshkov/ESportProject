namespace E_SportManager.Models.Teams
{
    public class TeamDetailsViewModel:TeamServiceModel
    {
        
        public string MidLaner { get; init; }

        public string MidLanerImageUrl { get; init; }

        public string TopLaner { get; init; }

        public string TopLanerImageUrl { get; init; }

        public string SupportLaner { get; init; }

        public string SupportLanerImageUrl { get; init; }

        public string JungleLaner { get; init; }

        public string JungleLanerImageUrl { get; init; }

        public string BottomLaner { get; init; }

        public string BottomLanerImageUrl { get; init; }

    }
}
