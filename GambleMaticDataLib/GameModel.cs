using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

public class GameModel
{
    public GameModel()
    {
        
    }

    public GameModel(DateTime gameDay, string home, string away, GamblingEvent gamblingEvent,int? result = null)
    {
        GameDay = gameDay;
        Home = home;
        Away = away;
        ResultInt = result;
        GamblingEvent = gamblingEvent;
        GamblingEventId = gamblingEvent.GamblingEventId;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int GameModelId { get; set; }
    public DateTime GameDay { get; set; } = DateTime.MinValue;
    public int GameOrderSortId { get; set; } 
    [MaxLength(255)]
    public string Home { get; set; } = "";
    [MaxLength(255)]
    public string Away { get; set; } = "";

    public int? ResultInt { get; set; } = null;

    public int? GamblingEventId { get; set; } = null;
    public GamblingEvent? GamblingEvent { get; set; } = null;
}