using E_SportManager.Data;
using System.ComponentModel.DataAnnotations;

using static E_SportManager.Common.ErrorMessages.Team;
using static E_SportManager.Common.GlobalConstants.Team;
namespace E_SportManager.Models.Teams
{
    public class AddTeamFormModel
    {
        [Required]
        [StringLength(
           TeamNameMaxLength,
           ErrorMessage = NameLengthErrorMessage,
           MinimumLength = TeamNameMinLength)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        public string MidRole { get; init; }

        [Required]
        public string TopRole { get; init; }

        [Required]
        public string SupportRole { get; init; }

        [Required]
        public string JungleRole { get; init; }

        [Required]
        public string BottomRole { get; init; }

        public IEnumerable<Player> MidLaners { get; init; }
        public IEnumerable<Player> TopLaners { get; init; }
        public IEnumerable<Player> SupportLaners { get; init; }
        public IEnumerable<Player> JungleLaners { get; init; }
        public IEnumerable<Player> BottomLaners { get; init; }
    }
}
