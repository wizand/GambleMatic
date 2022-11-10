namespace GambleMaticWebApp.Data
{
    public class GameAndPredictionViewModel
    {
        public GameAndPredictionViewModel(GameModelViewModel gameViewModel)
        {
            GameView = gameViewModel;
        }

        public GameAndPredictionViewModel(GambleItemModel gambleItemModel)
        {
            GameView = new GameModelViewModel(gambleItemModel.GameModel);
            Prediction = gambleItemModel.Guess;
        }

        public GameModelViewModel GameView { get; set; }
        public int Prediction { get; set; } = 0;
    }
}

