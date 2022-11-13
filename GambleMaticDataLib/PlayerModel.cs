using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GambleMaticDataLib;

public class PlayerModel
{

    public PlayerModel()
    {

    }

    public PlayerModel(string name, string shortName, string? email)
    {
        Name = name;
        ShortName = shortName;
        Email = email;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int PlayerModelId { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string ShortName { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    public int Paid { get; set; } = 0;
    public int Ticket { get; set; } = 0;


    public ExtraGamblesModel ExtraGambles { get; set; }

    [InverseProperty("PlayerModel")]
    public List<GambleItemModel> GambleItemModels { get; set; } = new List<GambleItemModel>();
    //public ICollection<GambleItemModel> GambleItemModels { get; set; }

    public override string ToString()
    {
        
        return @"["+ PlayerModelId + "] " + "[{Name}] [{ShortName}] [{Email}] [{Paid}] [{Ticket}]";
    }

}
