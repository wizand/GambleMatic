namespace GambleMaticWebApp.Data
{
    public class SingleGameStatisticsViewModel
    {
        public SingleGameStatisticsViewModel(GameModelViewModel game, List<GambleItemViewModel> allGambles)
        {
            Game = game;
            foreach (var gambleitem in allGambles)
            {
                if (game.IsGambleForGame(gambleitem))
                {
                    switch (gambleitem.GambleItem.Guess)
                    {
                        case -1:
                            HomeGuesses = HomeGuesses + 1;
                            break;
                        case 0:
                            DrawGuesses = DrawGuesses + 1;
                            break;
                        case 1:
                            AwayGuesses = AwayGuesses + 1;
                            break;
                    }
                }
            }

            TotalGuesses = HomeGuesses + DrawGuesses + AwayGuesses;

        }


        public GameModelViewModel Game { get; set; }

        public int HomeGuesses { get; set; } = 0;
        public float HomeGuessesPercent 
        {
            get
            {
                if (HomeGuesses == 0)
                    return 0;
                return (float)HomeGuesses / TotalGuesses;
            }
        }

        public string HomeGuessPercentStr => (HomeGuessesPercent * 100).ToString("0.");
        public int DrawGuesses { get; set; } = 0;
        public float DrawGuessesPercent
        {
            get
            {
                if (DrawGuesses == 0)
                    return 0;
                return (float)DrawGuesses / TotalGuesses;
            }
        }
        public string DrawGuessesPercentStr => (DrawGuessesPercent * 100).ToString("0.");

        public int AwayGuesses { get; set; } = 0;
        public float AwayGuessesPercent
        {
            get
            {
                if (AwayGuesses == 0)
                    return 0;
                return (float)AwayGuesses / TotalGuesses;
            }
        }
        public string AwayGuessesPercentStr => (AwayGuessesPercent * 100).ToString("0.");
        public int TotalGuesses { get; set; } = 0;
        public int TotalGames { get; set; } = 0;

        public string HslColorPerc(float percent, int start, int end)
        {
            var a = percent;
            var b = (end - start) * a;
            var c = b + start;

            // Return a CSS HSL string
            return "background-color: hsl("+ (int)c + ", 100%, 50%);";
        }

    }
}
