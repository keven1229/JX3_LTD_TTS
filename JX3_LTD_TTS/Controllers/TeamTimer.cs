
using JX3_LTD_TTS;
using JX3_LTD_TTS.Utils;
using System;
using System.Collections.Generic;
using System.Media;
using System.Timers;
using System.Windows;

namespace JX3_TeamAaitts.Controllers
{
    public class TeamTimer
    {
        private Timer raidTimer;
        private Timer bossTimer;
        private readonly TeamWindow tw;
        private long raidT;
        private long bossT;
        private  long ping;
        public long RaidT { get => raidT; set => raidT = value; }
        public long BossT { get => bossT; set => bossT = value; }
        public  long Ping { get => ping; set => ping = value; }

        public TeamTimer()
        {
        }

        public TeamTimer(TeamWindow tw, long ping)
        {
            this.tw = tw;
            RaidT = raidT;
            BossT = bossT;
            Ping = ping;
        }


        /// <summary>
        /// 更新副本语音列表
        /// </summary>
        /// <param name="teams"></param>
        public void UpdateComboBox_Raid(List<OnlineSpeech> teams)
        {
            tw.cbo_raid.Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        tw.cbo_raid.Items.Clear();
                    }
                    ));

            foreach (var item in teams)
            {

                tw.cbo_raid.Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        tw.cbo_raid.Items.Add(item.RaidName);
                    }
                    ));
            }

        }



        private bool raidFlag = false;
        /// <summary>
        /// 启动副本计时
        /// </summary>
        public void StartRiadTimer()
        {
            if (!raidFlag)
            {
                raidTimer = new Timer(1000)
                {
                    AutoReset = true
                };
                raidTimer.Elapsed += RaidTimer_Elapsed;
                raidTimer.Start();
                raidFlag = true;
            }
        }

        /// <summary>
        /// 停止副本计时
        /// </summary>
        public void StopRiadTimer()
        {
            if (raidFlag)
            {
                raidTimer.Stop();
                raidFlag = false;
            }
        }

        public void resetRiadTimer()
        {
                raidTimer.Stop();
                raidTimer.Close();
                RaidT = 0;
                tw.lab_raidTimer.Dispatcher.Invoke(
                    new Action(
                        delegate
                        {
                            tw.lab_raidTimer.Content = "未开始";
                        }
                        )
                    );
                raidFlag = false;  
        }

        private void RaidTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RaidT += 1000;

            tw.lab_raidTimer.Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        tw.lab_raidTimer.Content = Util.formatLongToTimeStr(RaidT);
                    }
                    )
                );
        }


        private bool bossFlag = false;
        public void StartBossTimer( )
        {
            try
            {
                
                
                if (!bossFlag)
                {
                    bossTimer = new Timer(1000)
                    {
                        AutoReset = true
                    };
                    bossTimer.Elapsed += BossTimer_Elapsed;
                    bossTimer.Start();
                    bossFlag = true;
                    Util.PlayWAV(@"wav/start.wav");
                }
            }
            catch (Exception)
            {
                Util.PlayWAV(@"wav/error.wav");
                MessageBox.Show("未选中BOSS语音文件！", "ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// 停止首领计时
        /// </summary>
        public void ResetBossTimer()
        {
            if (bossFlag)
            {
                BossT = 0;
                bossTimer.Stop();
                bossTimer.Close();
                tw.lab_bossTimer.Dispatcher.Invoke(
               new Action(
                   delegate
                   {
                       tw.lab_bossTimer.Content = "未开始";
                   }
                   )
               );
                bossFlag = false;
                Util.PlayWAV(@"wav/stop.wav");
            }
        }

        private void BossTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BossT += 1000;
            tw.lab_bossTimer.Dispatcher.Invoke(
               new Action(
                   delegate
                   {
                       tw.lab_bossTimer.Content = Util.formatLongToTimeStr(BossT);
                   }
                   )
               );
           
        }
    }
}
