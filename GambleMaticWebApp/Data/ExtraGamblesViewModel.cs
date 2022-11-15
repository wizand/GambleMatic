namespace GambleMaticWebApp.Data
{
    public class ExtraGamblesViewModel
    {

        public ExtraGamblesViewModel() : this("", "", "", "", "", "", ""){ }
        public ExtraGamblesViewModel(string team1, string team2, string team3, string team4, string silverTeam, string goldTeam, string goals)
        {
            SemiFinalTeam1 = team1;
            SemiFinalTeam1 = team2;
            SemiFinalTeam3 = team3;
            SemiFinalTeam4 = team4;
            SilverMedalTeam = silverTeam;
            GoldMedalTeam = goldTeam;
            GoalAmount = goals;
        }
        public ExtraGamblesViewModel(ExtraGamblesModel extraGambleModel)
        {
            SemiFinalTeam1 = extraGambleModel.SemifinalTeamOne;
            SemiFinalTeam2 = extraGambleModel.SemifinalTeamTwo;
            SemiFinalTeam3 = extraGambleModel.SemifinalTeamThree;
            SemiFinalTeam4 = extraGambleModel.SemifinalTeamFour;
            SilverMedalTeam = extraGambleModel.SilverTeam;
            GoldMedalTeam = extraGambleModel.GoldTeam;
            GoalAmount = extraGambleModel.GoalsInTournament;
        }

        public string SemiFinalTeam1 { get; set; }
        public string SemiFinalTeam2 { get; set; }
        public string SemiFinalTeam3 { get; set; }
        public string SemiFinalTeam4 { get; set; }
        public string GoldMedalTeam { get; set; }
        public string SilverMedalTeam { get; set; }
        public string GoalAmount { get; set; }

    }
}
