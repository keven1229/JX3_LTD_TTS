using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX3_LTD_TTS.Models
{
    public class SpeechPath
    {
        private string raidName;
        private string raidPath;

        public string RaidName { get => raidName; set => raidName = value; }
        public string RaidPath { get => raidPath; set => raidPath = value; }

        public SpeechPath(string raidName, string raidPath)
        {
            this.raidName = raidName;
            this.raidPath = raidPath;
        }

        public SpeechPath()
        {
        }
    }
}
