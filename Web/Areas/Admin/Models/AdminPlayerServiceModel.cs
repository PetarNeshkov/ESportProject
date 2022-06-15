namespace E_SportManager.Models.Players
{
    public class AdminPlayerServiceModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public int YearsOfExperience { get; init; }

        public string Role { get; init; }

        public string Division { get; init; }

        public bool IsPublic { get; init; }
    }
}
