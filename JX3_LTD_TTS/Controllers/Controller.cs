using JX3_LTD_TTS.Controllers;
using JX3_LTD_TTS.Models;
using JX3_LTD_TTS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace JX3_TeamAaitts
{
    public class Controller
    {

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="e">异常信息</param>
        public static void WriteLog(Exception e)
        {
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string strFu = dt.ToString("yyyy-MM-dd HH:mm:ss");
            FileStream fs = new FileStream(@"log.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(string.Format("{0}-{1}-{2}", strFu, e.Source, e.Message));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 获取语音文件组
        /// </summary>
        /// <returns>返回TeamTTS数据对象</returns>
        public List<OnlineSpeech> GET_TeamTTS()
        {
            List<OnlineSpeech> teamTts = new List<OnlineSpeech>();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\data\", "*.txt");
            foreach (var item in files)
            {
                teamTts.Add(LoadTTS_FOR_FileName(item));
            }
            return teamTts;
        }

        /// <summary>
        /// 解析单个文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>TTS解析对象</returns>
        private OnlineSpeech LoadTTS_FOR_FileName(string filePath)
        {
            OnlineSpeech tts = new OnlineSpeech();
            ArrayList lst = new ArrayList();
            StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                lst.Add(str);
            }
            tts.RaidName = filePath.Substring(filePath.LastIndexOf('\\') + 1); ; //合成文件名称
            tts.FilePath = filePath;
            foreach (string item in lst)
            {
                string[] temp = item.Split('-');//分割时间轴，合成内容
                tts.RaidTts.Add(long.Parse(temp[0]) * 1000, temp[1]);
            }
            return tts;
        }

        public OfflineSpeech GetOfflineSpeech(string filePath)
        {
            OfflineSpeech offlineSpeech = null;
            if (filePath.Substring(filePath.LastIndexOf('.')).Equals(".bin)"))
            {
                offlineSpeech = Util.LoadBin(filePath) as OfflineSpeech;
            }
            return offlineSpeech;
        }

        public void GetOfflineSpeech(string filePath, OnlineSpeech onlineSpeech, OfflineSpeech offlineSpeech, UserInfo userInfo, SessionBeginParams sbp)
        {
            offlineSpeech.RaidName = onlineSpeech.RaidName.Substring(0, onlineSpeech.RaidName.LastIndexOf('.')) + ".bin";
            onlineSpeech.FilePath = onlineSpeech.FilePath.Substring(0, onlineSpeech.FilePath.LastIndexOf('.')) + ".bin";
            Thread thread = null;
            try
            {
                //TODO：多线程修改
                thread = new Thread(new ThreadStart(QTTSThread(onlineSpeech, offlineSpeech, userInfo, sbp)));
                thread.Start();
            }
            finally
            {
                thread.Abort();
                Console.WriteLine(offlineSpeech.SpeechData.Count);
            }
        }

        private ThreadStart QTTSThread(OnlineSpeech onlineSpeech, OfflineSpeech offlineSpeech, UserInfo userInfo, SessionBeginParams sbp)
        {

            foreach (var item in onlineSpeech.RaidTts.Keys)
            {
                byte[] temp = QTTSAPI.SpeechSynthesizer(userInfo, sbp, onlineSpeech.RaidTts[item]);
                offlineSpeech.SpeechData.Add(item, temp);
            }
            //  throw new NotImplementedException();
            return null;
        }
    }
}
