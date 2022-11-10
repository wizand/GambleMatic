namespace GambleMaticWebApp.Data
{
    public class GambleItemViewModel
    {
        public GambleItemViewModel(GambleItemModel gambleItemModel)
        {
            GambleItem = gambleItemModel;
            PlayerViewModel = new PlayerModelViewModel(GambleItem.PlayerModel);
            GameViewModel = new GameModelViewModel(GambleItem.GameModel);
        }

        public PlayerModelViewModel PlayerViewModel { get; set; }
        public GameModelViewModel GameViewModel { get; set; }
        public GambleItemModel GambleItem { get; set; }
        public string MatchTitle => GambleItem.GameModel.Home + "-" + GambleItem.GameModel.Away;
        public string WinnerGuess 
        {
            get
            {
                return MatchResultAsStr(GambleItem.Guess);
            }
        }

        

        private string MatchResultAsStr(int resultInt)
        {
            switch (resultInt)
            {
                case -1:
                    return "HOME";
                case 0:
                    return "DRAW";
                case 1:
                    return "AWAY";
                default:
                    return "N/A";
            }
        }
    }
}
