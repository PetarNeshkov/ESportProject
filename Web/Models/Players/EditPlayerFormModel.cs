namespace E_SportManager.Models.Players
{
    public class EditPlayerFormModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public string Role { get; init; }

        public string Division { get; init; }

        public int YearsOfExperience { get; init; }

        public string Description { get; init; }

        public string AuthorId { get; init; }
    }
}
