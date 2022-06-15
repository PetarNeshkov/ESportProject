using System.ComponentModel.DataAnnotations;

using static E_SportManager.Common.ErrorMessages.Player;
using static E_SportManager.Common.GlobalConstants.Player;

namespace E_SportManager.Models.Players
{
    public class EditPlayerFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(
            PlayerNameMaxLength,
            ErrorMessage = NameLengthErrorMessage,
            MinimumLength = PlayerNameMinLength)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        public string Role { get; init; }

        [Required]
        public string Division { get; init; }

        [Range(0, 50)]
        public int YearsOfExperience { get; init; }

        [Required]
        [StringLength(
            PlayerDescriptionMaxLength,
            ErrorMessage = DescriptionLengthErrorMessage,
            MinimumLength = PlayerDescriptionMinLength)]
        public string Description { get; init; }

        public bool IsPublic { get; init; }
    }
}
