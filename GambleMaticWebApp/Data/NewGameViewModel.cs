namespace GambleMaticWebApp.Data
{
    public class NewGameViewModel
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Home { get; set; }
        public string Away { get; set; }

        public override string ToString()
        {
            return Date.ToShortDateString() + " " + Home + " vs " + Away;
        }
    }
}
