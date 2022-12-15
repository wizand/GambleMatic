using System.Text;

using GambleMaticWebApp.Data;

public class GameModelViewModel
{
    public GameModelViewModel()
    {
        _gameModel = new();
    }

    public GameModelViewModel(GameModel gameModel)
    {
        _gameModel = gameModel;
        if (gameModel.ResultInt == null)
        {
            OriginalResult = -99;
        }
        else
        {
            OriginalResult = gameModel.ResultInt.Value;
        }
    }

    public GameModelViewModel(DateTime gameDay, string home, string away, int? result = null)
    {
        if (_gameModel == null)
        {
            _gameModel = new GameModel(gameDay, home, away, result);
        }
        else
        {
            _gameModel.GameDay = gameDay;
            _gameModel.Home = home;
            _gameModel.Away = away;
            _gameModel.ResultInt = result;
            if (result == null)
            {
                OriginalResult = -99;
            }
            else
            {
                OriginalResult = result.Value;
            }
        }
    }


    private GameModel _gameModel = null;

    public DateTime GameDay { get { return _gameModel.GameDay; } set { _gameModel.GameDay = value; } }
    public string GameDayStr => _gameModel.GameDay.ToString("dd.MM yyyy");


    public string Home { get { return _gameModel.Home; } set { _gameModel.Away = value; } }
    public string Away { get { return _gameModel.Away; } set { _gameModel.Away = value; } }
    public int ResultInt
    {
        get
        {
            if (_gameModel.ResultInt.HasValue)
            {
                return _gameModel.ResultInt.Value;
            }
            return -99;
        }

        set { _gameModel.ResultInt = value; }
    }
    public string ResultStr
    {
        get
        {
            switch (ResultInt)
            {
                case -1:
                    return "Home";
                case 0:
                    return "Draw";
                case 1:
                    return "Away";
                default:
                    return "";
            }
        }
    }

    public int OriginalResult { get; set; } = -99;

    public string MatchTitle => Home + "-" + Away;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("[" + GameDayStr + "]: [");

        sb.Append(Home + "] vs [");
        sb.Append(Away + "] resulted in [");
        sb.Append(ResultStr + "] team win!");
        return sb.ToString();
    }

    internal GameModel GetGameModel()
    {
        return _gameModel;
    }

    internal bool IsGambleForGame(GambleItemViewModel gambleitem)
    {
        var gameModel = gambleitem.GambleItem.GameModel;
        if ( gameModel != null)
        {
            if (gameModel.Home.Equals(this.Home) && gameModel.Away.Equals(this.Away) && gameModel.GameDay.Equals(this.GameDay))
            {
                return true;
            }
        }

        return false;
    }
}