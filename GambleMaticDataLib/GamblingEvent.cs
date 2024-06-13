using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class GamblingEvent
{

    public GamblingEvent()
    {

    }

    public GamblingEvent(string eventName, DateTime beginDate, DateTime endDate)
    {
        EventName = eventName;
        BeginDate = beginDate;
        EndDate = endDate;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int GamblingEventId { get; set; }
    [Required]
    public string EventName { get; set; }
    [Required]
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public IEnumerable<GameModel> Games { get; set; }
}

