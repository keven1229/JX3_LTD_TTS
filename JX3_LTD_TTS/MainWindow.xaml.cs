using JX3_LTD_TTS.Controllers;
using JX3_LTD_TTS.Models;
using JX3_LTD_TTS.Utils;
using System.Windows;


namespace JX3_LTD_TTS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserInfo userInfo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        private void Login()
        {
            userInfo = new UserInfo
            {
                UserAppid = txt_userAppID.Text,
                UserNmae = txt_userName.Text,
                UserPwd = txt_userPwd.Password
            };
            int ret = QTTSAPI.MSPLogin(userInfo);
            if (ret == (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
            {
                TeamWindow tw = new TeamWindow
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                tw.Tag = userInfo;
                tw.Show();
                this.Hide();
                QTTSAPI.MSPLogout();
            }
            else
            {
                MessageBox.Show(string.Format("登录失败!（Error:{0}）", ret));
            }
        }

        private void Chk_saveInfo_Click(object sender, RoutedEventArgs e)
        {

            if ((bool)chk_saveInfo.IsChecked)
            {
                Util.SaveUserInfo(new UserInfo
                {
                    UserAppid = txt_userAppID.Text,
                    UserNmae = txt_userName.Text,
                    UserPwd = txt_userPwd.Password
                });

            }
            else
            {
                Util.DeleteUserInfo();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userInfo = Util.LoadUserInfo();
            if (userInfo != null)
            {
                txt_userAppID.Text = userInfo.UserAppid.Substring(7);
                txt_userName.Text = userInfo.UserNmae;
                txt_userPwd.Password = userInfo.UserPwd;
                chk_saveInfo.IsChecked = true;
            }
        }

        private void Btn_offline_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow tw = new TeamWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen

            };
            tw.Tag = "offline";
            tw.Title += "  离线模式";
            tw.btn_raidUpdate.Content = "读取";
            tw.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
