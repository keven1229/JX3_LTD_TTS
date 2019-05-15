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
            int ret = XFTTS.TTS.MSPLogin(userInfo.UserNmae, userInfo.UserPwd, userInfo.UserAppid);
            if (ret == (int)XFTTS.TTS.ErrorCode.MSP_SUCCESS)
            {
               

            }
            else
            {
                MessageBox.Show(string.Format("登录失败!（Error:{0}）",ret));
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
    }
}
