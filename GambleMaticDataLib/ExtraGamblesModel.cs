using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExtraGamblesModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long ExtraGamblesModelId { get; set; }

    [MaxLength(255)]
    public string SemifinalTeamOne { get; set; }
    [MaxLength(255)]
    public string SemifinalTeamTwo { get; set; }
    [MaxLength(255)]
    public string SemifinalTeamThree { get; set; }
    [MaxLength(255)]
    public string SemifinalTeamFour { get; set; }

    [MaxLength(255)]
    public string SilverTeam { get; set; }
    [MaxLength(255)]
    public string GoldTeam { get; set; }
    [MaxLength(16)]
    public string GoalsInTournament { get; set; }

    public int? PlayerModelId { get; set; }
    public PlayerModel? PlayerModel { get; set; }

    public bool IsResultObject { get; set; } = false;

    public List<string> GetSemiFinalTeamsAsList() 
    {
        List<string> teams = new();
        teams.Add(SemifinalTeamOne);
        teams.Add(SemifinalTeamTwo);
        teams.Add(SemifinalTeamThree);
        teams.Add(SemifinalTeamFour);
        return teams;

    }

    public List<string> GetFinalTeamsAsList()
    {
        List<string> teams = new();
        teams.Add(SilverTeam);
        teams.Add(GoldTeam);
        return teams;
    }

    public int? GamblingEventId { get; set; } = null;
    public GamblingEvent? GamblingEvent { get; set; } = null;

}

