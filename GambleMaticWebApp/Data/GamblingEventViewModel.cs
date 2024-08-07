namespace GambleMaticWebApp.Data
{
    public class GamblingEventViewModel
    {



        public GamblingEventViewModel()
        {
            GamblingEvent = new();
        }

        public GamblingEventViewModel(GamblingEvent gamblingEvent)
        {
            GamblingEvent = gamblingEvent;
            GamesInEvent = gamblingEvent.Games.Select(g => new GameModelViewModel(g)).ToList();
            ExtraGamblesInEvent = gamblingEvent.ExtraGamblesInEvent.Select(eg => new ExtraGamblesViewModel(eg)).ToList();
        }

        public GamblingEventViewModel(string eventName, DateTime beginDate, DateTime endDate)
        {
            if (GamblingEvent == null)
            {
                GamblingEvent = new(eventName, beginDate, endDate);
            }
            else
            {
                GamblingEvent.EventName = eventName;
                GamblingEvent.BeginDate = beginDate;
                GamblingEvent.EndDate = endDate;
            }
        }

        public GamblingEvent GamblingEvent = null;

        public int GamblingEventId { get { return GamblingEvent.GamblingEventId; } set { GamblingEvent.GamblingEventId = value; } }

        public string EventName { get { return GamblingEvent.EventName; } set { GamblingEvent.EventName = value; } }
        public DateTime BeginDate { get { return GamblingEvent.BeginDate; } set { GamblingEvent.BeginDate = value; } }
        public DateTime EndDate { get { return GamblingEvent.EndDate; } set { GamblingEvent.EndDate = value; } }

        public string BeginDateStr
        {
            get
            {
                return BeginDate.ToString("yyyy-MM-dd");
            }
        }   

        public string EndDateStr
        {
            get
            {
                return EndDate.ToString("yyyy-MM-dd");
            }
        }

        public List<GameModelViewModel> GamesInEvent = new List<GameModelViewModel>();
        public List<ExtraGamblesViewModel> ExtraGamblesInEvent { get; set; } = new();


        public ExtraGamblesModel? GetExtraGamblesResultModelForEvent()
        {
            foreach (var extraGamble in GamblingEvent.ExtraGamblesInEvent)
            {
                if (extraGamble.IsResultObject)
                {
                    return extraGamble;
                }
            }
            return null;
        }

        public ExtraGamblesModel? GetExtraGamblingModelForPlayer(PlayerModel player)
        {
            foreach( var extraGamble in GamblingEvent.ExtraGamblesInEvent)
            {
                if (extraGamble.PlayerModelId == player.PlayerModelId)
                {
                    return extraGamble;
                }
            }
            return null;
        }
    }
}
