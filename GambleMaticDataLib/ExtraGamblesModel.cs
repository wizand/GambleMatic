using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleMaticDataLib
{
    public  class ExtraGamblesModel
    {

        public long ExtraGamblesModelId { get; set; }


        public string SemifinalTeamOne { get; set; }
        public string SemifinalTeamTwo { get; set; }
        public string SemifinalTeamThree { get; set; }
        public string SemifinalTeamFour { get; set; }


        public string SilverTeam { get; set; }
        public string GoldTeam { get; set; }
        public int GoalsInTournamen { get; set; }

        public int PlayerModelId { get; set; }
        public PlayerModel PlayerModel;


    }
}
