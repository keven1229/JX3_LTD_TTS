using JX3_LTD_TTS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace JX3_LTD_TTS.Utils
{
    public class Util
    {
        /// <summary>
        /// 保存用户密码信息
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUserInfo(UserInfo user)
        {
            FileStream fs = new FileStream(@"config\data.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, user);
            fs.Close();
        }

        /// <summary>
        /// 读取用户密码信息
        /// </summary>
        /// <returns></returns>
        public static UserInfo LoadUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            if (File.Exists(@"config\data.bin"))
            {
                FileStream fs = new FileStream(@"config\data.bin",FileMode.Open);
                if (fs.Length > 0) {
                    BinaryFormatter bf = new BinaryFormatter();
                    userInfo = bf.Deserialize(fs) as UserInfo;
                    fs.Close();
                    return userInfo;
                }
                fs.Close();
            }
            return null;
        }


        public static void DeleteUserInfo()
        {
            if (File.Exists(@"config\data.bin"))
                File.Delete(@"config\data.bin");
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="l">毫秒</param>
        /// <returns></returns>
        public static String formatLongToTimeStr(long l)
        {

            int hour = 0;
            int minute = 0;
            int second = 0;
            second = (int)(l / 1000);

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return (hour.ToString() + ":" + minute.ToString() + ":"
                + second.ToString());
        }
    }
}

