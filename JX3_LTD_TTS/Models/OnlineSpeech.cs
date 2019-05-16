using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX3_TeamAaitts
{
    public class OnlineSpeech
    {
        private string raidName;
        private string filePath;
        private Dictionary<long, string> raidTts = new Dictionary<long, string>();

        public string RaidName { get => raidName; set => raidName = value; }
        public Dictionary<long, string> RaidTts { get => raidTts; set => raidTts = value; }
        public string FilePath { get => filePath; set => filePath = value; }
    }
}
