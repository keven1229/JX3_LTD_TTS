using JX3_LTD_TTS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static XFTTS.TTS;

namespace JX3_LTD_TTS.Controllers
{
    public class QTTSAPI
    {
        public static byte[] SpeechSynthesizer(UserInfo userInfo, SessionBeginParams sbp, string text)
        {
            int ret = 0;
            IntPtr session_ID;
            ret = MSPLogin(userInfo);
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;

            session_ID = QTTSSessionBegin(sbp, ret);
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;


            ret = QTTSTextPut(session_ID,text);
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;

            byte[] array = QTTSAudioGet(session_ID,ret);
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;

            ret = QTTSSessionEnd(session_ID);
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;

            ret = MSPLogout();
            if (ret != (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
                return null;

            return array;
        }

        public static int MSPLogin(UserInfo userInfo)
        {
            int ret = XFTTS.TTS.MSPLogin(userInfo.UserNmae, userInfo.UserPwd, userInfo.UserAppid);
            Console.WriteLine("登录状态：code（{0}）", ret);
            return ret;
        }

        public static IntPtr QTTSSessionBegin(SessionBeginParams sbp, int ret)
        {
            IntPtr session_ID = XFTTS.TTS.QTTSSessionBegin(sbp.GetParams(), ref ret);
            Console.WriteLine("申请合成句柄：code（{0}）", ret);
            return session_ID;
        }

        public static int QTTSTextPut(IntPtr session_ID, string text)
        {
            int ret = XFTTS.TTS.QTTSTextPut(Ptr2Str(session_ID), text, (uint)Encoding.Default.GetByteCount(text), string.Empty);
            Console.WriteLine("推送合成内容：code（{0}）", ret);
            return ret;

        }

        public static byte[] QTTSAudioGet(IntPtr session_ID,int ret)
        {
            uint audio_len = 0;
            SynthStatus synth_status = 0;
            byte[] array;
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(new byte[44], 0, 44);
            while (true)
            {
                IntPtr source = XFTTS.TTS.QTTSAudioGet(Ptr2Str(session_ID), ref audio_len, ref synth_status, ref ret);
                array = new byte[(int)audio_len];
                if (audio_len > 0)
                {
                    Marshal.Copy(source, array, 0, (int)audio_len);
                }
                memoryStream.Write(array, 0, array.Length);
                Thread.Sleep(200);
                if (synth_status == SynthStatus.MSP_TTS_FLAG_DATA_END || ret != 0)
                    break;
                Console.WriteLine("正在获取合成内容：code（{0}），Audio_length:{1}，SynthStatus：{2}"
                    , ret, audio_len, synth_status.ToString());
            }
            WAVE_Header wave_Header = getWave_Header((int)memoryStream.Length - 44);
            byte[] array2 = StructToBytes(wave_Header);
            memoryStream.Position = 0L;
            memoryStream.Write(array2, 0, array2.Length);
            memoryStream.Position = 0L;
            return memoryStream.ToArray();
        }

        public static int QTTSSessionEnd(IntPtr session_ID)
        {
            int ret = XFTTS.TTS.QTTSSessionEnd(Ptr2Str(session_ID), string.Empty);
            Console.WriteLine("结束合成：code（{0}）", ret);
            return ret;
        }

        public static int MSPLogout()
        {

            int ret = XFTTS.TTS.MSPLogout();
            Console.WriteLine("退出登录：code（{0}）", ret);
            return ret;
        }


        /// <summary>
        /// 结构体转字符串
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        private static byte[] StructToBytes(object structure)
        {
            int num = Marshal.SizeOf(structure);
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            byte[] result;
            try
            {
                Marshal.StructureToPtr(structure, intPtr, false);
                byte[] array = new byte[num];
                Marshal.Copy(intPtr, array, 0, num);
                result = array;
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
            return result;
        }
        /// <summary>
        /// 结构体初始化赋值
        /// </summary>
        /// <param name="data_len"></param>
        /// <returns></returns>
        private static WAVE_Header getWave_Header(int data_len)
        {
            return new WAVE_Header
            {
                RIFF_ID = 1179011410,
                File_Size = data_len + 36,
                RIFF_Type = 1163280727,
                FMT_ID = 544501094,
                FMT_Size = 16,
                FMT_Tag = 1,
                FMT_Channel = 1,
                FMT_SamplesPerSec = 16000,
                AvgBytesPerSec = 32000,
                BlockAlign = 2,
                BitsPerSample = 16,
                DATA_ID = 1635017060,
                DATA_Size = data_len
            };
        }
        /// <summary>
        /// 语音音频头
        /// </summary>
        private struct WAVE_Header
        {
            public int RIFF_ID;
            public int File_Size;
            public int RIFF_Type;
            public int FMT_ID;
            public int FMT_Size;
            public short FMT_Tag;
            public ushort FMT_Channel;
            public int FMT_SamplesPerSec;
            public int AvgBytesPerSec;
            public ushort BlockAlign;
            public ushort BitsPerSample;
            public int DATA_ID;
            public int DATA_Size;
        }
        /// 指针转字符串
        /// </summary>
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        public static string Ptr2Str(IntPtr p)
        {
            List<byte> lb = new List<byte>();
            while (Marshal.ReadByte(p) != 0)
            {
                lb.Add(Marshal.ReadByte(p));
                p = p + 1;
            }
            byte[] bs = lb.ToArray();
            return Encoding.Default.GetString(lb.ToArray());
        }

    }
}
