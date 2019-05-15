using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX3_TeamAaitts
{
    public class TeamTts
    {
        private string raidName;
        private Dictionary<long, string> raidTts = new Dictionary<long, string>();

        public string RaidName { get => raidName; set => raidName = value; }
        public Dictionary<long, string> RaidTts { get => raidTts; set => raidTts = value; }

    }
}
