using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JX3_LTD_TTS.Models
{
    [Serializable]
    public class UserInfo
    {
        private string userNmae;
        private string userPwd;
        private string userAppid;

        public string UserNmae { get => userNmae; set => userNmae = value; }
        public string UserPwd { get => userPwd; set => userPwd = value; }
        public string UserAppid { get => userAppid; set => userAppid = ("appid =" + value); }
    }
}
