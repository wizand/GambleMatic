namespace GambleMaticWebApp.Data
{
    public class GamblingEventViewModel
    {



        public GamblingEventViewModel()
        {
            _gamblingEvent = new();
        }

        public GamblingEventViewModel(GamblingEvent gamblingEvent)
        {
            _gamblingEvent = gamblingEvent;
        }

        public GamblingEventViewModel(string eventName, DateTime beginDate, DateTime endDate)
        {
            if (_gamblingEvent == null)
            {
                _gamblingEvent = new(eventName, beginDate, endDate);
            }
            else
            {
                _gamblingEvent.EventName = eventName;
                _gamblingEvent.BeginDate = beginDate;
                _gamblingEvent.EndDate = endDate;
            }
        }

        private GamblingEvent _gamblingEvent = null;

        public string EventName { get { return _gamblingEvent.EventName; } set { _gamblingEvent.EventName = value; } }
        public DateTime BeginDate { get { return _gamblingEvent.BeginDate; } set { _gamblingEvent.BeginDate = value; } }
        public DateTime EndDate { get { return _gamblingEvent.EndDate; } set { _gamblingEvent.EndDate = value; } }

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
    }
}
