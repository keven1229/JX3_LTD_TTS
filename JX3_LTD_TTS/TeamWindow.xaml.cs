using JX3_LTD_TTS.Models;
using JX3_LTD_TTS.Utils;
using JX3_TeamAaitts;
using JX3_TeamAaitts.Controllers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace JX3_LTD_TTS
{
    /// <summary>
    /// TeamWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TeamWindow : Window
    {
        private KeyEventHandler myKeyEventHandeler = null;//按键钩子
        private KeyboardHook k_hook = new KeyboardHook();
        private Controller ctrl = new Controller();
        private SessionBeginParams sbp;
        private List<OnlineSpeech> onlineSpeech = new List<OnlineSpeech>();
        private List<SpeechPath> speechPaths = new List<SpeechPath>();
        private TeamTimer teamTimer;
        private OfflineSpeech offlineSpeech;
        private UserInfo userInfo;


        public TeamWindow()
        {
            InitializeComponent();
            teamTimer = new TeamTimer(this, long.Parse(txt_ping.Text));
            StartListen();
            
        }

        private void Btn_raidStart_Click(object sender, RoutedEventArgs e)
        {
            teamTimer.StartRiadTimer();
        }

        private void Btn_raidStop_Click(object sender, RoutedEventArgs e)
        {
            teamTimer.StopRiadTimer();
        }

        private void Btn_raidReset_Click(object sender, RoutedEventArgs e)
        {
            teamTimer.resetRiadTimer();
        }

        private void Btn_raidUpdate_Click(object sender, RoutedEventArgs e)
        {
            onlineSpeech = ctrl.GET_TeamTTS();
            cbo_raid.Items.Clear();

            foreach (var item in onlineSpeech)
            {
                cbo_raid.Items.Add(item.RaidName);
            }
        }

        private void Btn_bossStart_Click(object sender, RoutedEventArgs e)
        {

            System.Console.WriteLine(cbo_raid.Text.Substring(cbo_raid.Text.LastIndexOf('.')));
            if (cbo_raid.Text.Equals(""))
            {
                Util.PlayWAV(@"wav/error.wav");
            }
            else if (cbo_raid.Text.Substring(cbo_raid.Text.LastIndexOf('.')).Equals(".bin)"))
            {
                //文件后缀为bin时读取离线语音
                //TODO：真是令人头大
                string filePaht = "";
                foreach (var item in speechPaths)
                {
                    if (cbo_raid.Text.Equals(item.RaidName))
                        filePaht = item.RaidPath;
                }
                offlineSpeech = ctrl.GetOfflineSpeech(filePaht);
            }
            else if (cbo_raid.Text.Substring(cbo_raid.Text.LastIndexOf('.')).Equals(".txt"))
            {
                //文件后缀为txt时合成语音并保存到本地
                OnlineSpeech online = null;
                offlineSpeech = new OfflineSpeech();
                foreach (var item in onlineSpeech)
                {
                    if (cbo_raid.Text.Equals(item.RaidName))
                        online = item;
                }
                ctrl.GetOfflineSpeech(online.FilePath, online, offlineSpeech, userInfo, sbp);
            }
            teamTimer.StartBossTimer();

        }

        private void Btn_bossReset_Click(object sender, RoutedEventArgs e)
        {
            teamTimer.ResetBossTimer();
        }

        private void Btn_set_Click(object sender, RoutedEventArgs e)
        {

            SetWindow sw = new SetWindow
            {
                Owner = this
            };
            if (sbp != null)
                sw.Tag = sbp;
            sw.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sbp = Util.LoadBin(@"config\sbp.bin") as SessionBeginParams;
            userInfo = this.Tag as UserInfo;
            System.Console.WriteLine(userInfo.UserNmae);
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.D9 && (int)Control.ModifierKeys == (int)Keys.Alt)
            {
                long temp = long.Parse(txt_ping.Text);
                temp -= 500;
                teamTimer.Ping = temp;
                txt_ping.Text = temp + "";
            }

            if (e.KeyValue == (int)Keys.D0 && (int)Control.ModifierKeys == (int)Keys.Alt)
            {
                long temp = long.Parse(txt_ping.Text);
                temp += 500;
                teamTimer.Ping = temp;
                txt_ping.Text = temp + "";
            }

            if (e.KeyValue == (int)Keys.F9)
            {
                Btn_bossStart_Click(sender, new RoutedEventArgs());
            }

            if (e.KeyValue == (int)Keys.F10)
            {
                Btn_bossReset_Click(sender, new RoutedEventArgs());
            }
        }


        public void StartListen()
        {
            myKeyEventHandeler = new KeyEventHandler(Hook_KeyDown);
            k_hook.KeyDownEvent += myKeyEventHandeler;//钩住键按下
            k_hook.Start();//安装键盘钩子
        }

        public void StopListen()
        {
            if (myKeyEventHandeler != null)
            {
                k_hook.KeyDownEvent -= myKeyEventHandeler;//取消按键事件
                myKeyEventHandeler = null;
                k_hook.Stop();//关闭键盘钩子
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            StopListen();
            Owner.Close();
        }
    }
}
