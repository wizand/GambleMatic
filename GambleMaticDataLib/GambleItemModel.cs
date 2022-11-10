using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GambleItemModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long GambleItemModelId { get; set; }


    public int? GameModelId { get; set; }
    [Required]
    public GameModel GameModel { get; set; }
    public int? PlayerModelId { get; set; }
    public PlayerModel PlayerModel { get; set; }
    public int Guess { get; set; }

}
