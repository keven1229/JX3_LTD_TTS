using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX3_LTD_TTS.Models
{
    [Serializable]
    public class OfflineSpeech
    {
        private string raidName;
        private string fileName;
        private Dictionary<long, byte[]> speechData =new Dictionary<long, byte[]>();


        public Dictionary<long, byte[]> SpeechData { get => speechData; set => speechData = value; }
        public string RaidName { get => raidName; set => raidName = value; }
        public string FileName { get => fileName; set => fileName = value; }

        public OfflineSpeech(string raidName)
        {
            this.raidName = raidName;
            if (speechData==null)
            {
                speechData = new Dictionary<long, byte[]>();
            }
        }

        public OfflineSpeech()
        {

        }
    }
}
