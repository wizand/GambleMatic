using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

public class GameModel
{
    public GameModel()
    {
        
    }

    public GameModel(DateTime gameDay, string home, string away, int? result = null)
    {
        GameDay = gameDay;
        Home = home;
        Away = away;
        ResultInt = result;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int GameModelId { get; set; }
    public DateTime GameDay { get; set; } = DateTime.MinValue;
    [MaxLength(255)]
    public string Home { get; set; } = "";
    [MaxLength(255)]
    public string Away { get; set; } = "";

    public int? ResultInt { get; set; } = null;


}