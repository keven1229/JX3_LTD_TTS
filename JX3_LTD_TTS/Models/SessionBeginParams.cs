using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX3_LTD_TTS.Models
{
    [Serializable]
    public class SessionBeginParams
    {
        public enum voice_nameEnum
        {
            xiaoyan,
            aisjiuxu,
            aisxping,
            aisjinger,
            aisbabyxu,
        }


        public enum text_encodingEnum
        {
            GB2312, GBK, BIG5, UNICODE, GB18030, UTF8
        }

        private string voice_name;
        private byte speed;
        private byte volume;
        private byte pitch;
        private string text_encoding;
        //private byte background_sound;
       // private string aue;

        public string Voice_name { get => voice_name; set => voice_name = value; }
        public byte Speed { get => speed; set => speed = value; }
        public byte Volume { get => volume; set => volume = value; }
        public byte Pitch { get => pitch; set => pitch = value; }
        public string Text_encoding { get => text_encoding; set => text_encoding = value; }
       // public byte Background_sound { get => background_sound; set => background_sound = value; }
       // public string Aue { get => aue; set => aue = value; }

        public SessionBeginParams(string voice_name, byte speed, byte volume, byte pitch, string text_encoding)
        {
            this.voice_name = voice_name;
            this.speed = speed;
            this.volume = volume;
            this.pitch = pitch;
            this.text_encoding = text_encoding;
            //this.background_sound = 0;
           // this.aue = "speex-wb;7";
        }

        public SessionBeginParams()
        {
            
        }

        public string GetParams() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("voice_name={0},",Voice_name);
            sb.AppendFormat("speed={0},", Speed);
            sb.AppendFormat("volume={0},", Volume);
            sb.AppendFormat("pitch={0},", Pitch);
            //sb.AppendFormat("text_encoding={0},", Text_encoding);
            sb.AppendFormat("aue=speex-wb;7,");
            //sb.AppendFormat("background_sound={0}", Background_sound);
            return sb.ToString();
        }
    }
}
