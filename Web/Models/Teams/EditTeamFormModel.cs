using E_SportManager.Service.Data.Teams;
using System.ComponentModel.DataAnnotations;
using static E_SportManager.Common.ErrorMessages.Team;
using static E_SportManager.Common.GlobalConstants.Team;

namespace E_SportManager.Models.Teams
{
    public class EditTeamFormModel:EditTeamImageUrlFormModel
    {
        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength,
          ErrorMessage = NameLengthErrorMessage)]
        public string Title { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        public string MidLaner { get; init; }

        [Required]
        public string TopLaner { get; init; }

        [Required]
        public string SupportLaner { get; init; }

        [Required]
        public string JungleLaner { get; init; }

        [Required]
        public string BottomLaner { get; init; }

        public IEnumerable<PlayerListServiceModel>? MidLaners { get; set; }
        public IEnumerable<PlayerListServiceModel>? TopLaners { get; set; }
        public IEnumerable<PlayerListServiceModel>? SupportLaners { get; set; }
        public IEnumerable<PlayerListServiceModel>? JungleLaners { get; set; }
        public IEnumerable<PlayerListServiceModel>? BottomLaners { get; set; }
    }
}
