using System.ComponentModel.DataAnnotations;

namespace E_SportManager.Models.Teams
{
    public class BattleTeamFormModel
    {
        [Required]
        public string FirstTeamName { get; init; }

        [Required]
        public string SecondTeamName { get; init; }

        public IEnumerable<TeamServiceModel>? FirstTeam { get; set; }
        public IEnumerable<TeamServiceModel>? SecondTeam { get; set; }
    }
}
