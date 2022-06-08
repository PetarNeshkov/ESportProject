using E_SportManager.Data.enums;
using System.ComponentModel.DataAnnotations;

using static E_SportManager.Common.GlobalConstants.Player;
using static E_SportManager.Common.ErrorMessages.Player;

namespace E_SportManager.Models.Players
{
    public class AddPlayerFormModel
    {
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
        [EnumDataType(typeof(Role))]
        public string Role { get; init; }

        [Required]
        [EnumDataType(typeof(Division))]
        public string Division { get; init; }

        [Range(0, 50)]
        public int YearsOfExperience { get; init; }

        [Required]
        [StringLength(
            PlayerDescriptionMaxLength,
            ErrorMessage =DescriptionLengthErrorMessage,
            MinimumLength =PlayerDescriptionMinLength)]
        public string Description { get; init; }

    }
}
