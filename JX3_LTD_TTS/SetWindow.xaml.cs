using JX3_LTD_TTS.Models;
using JX3_LTD_TTS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JX3_LTD_TTS
{
    /// <summary>
    /// SetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetWindow : Window
    {

        private SessionBeginParams sbp;

        public SetWindow()
        {
            InitializeComponent();
        }


        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (sbp == null)
            {
                sbp = new SessionBeginParams();
            }
            //   sbp.Background_sound = 0;
            sbp.Pitch = (byte)sld_pitch.Value;
            sbp.Speed = (byte)sld_speed.Value;
            sbp.Volume = (byte)sld_volume.Value;
            sbp.Voice_name = cbo_voice_name.Text;
            sbp.Text_encoding = cbo_text_encoding.Text;
            Util.SaveBin(@"config\sbp.bin", sbp);
            this.Close();
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Tag != null)
            {
                sbp = Tag as SessionBeginParams;
                cbo_text_encoding.Text = sbp.Text_encoding;
                cbo_voice_name.Text = sbp.Voice_name;
                sld_pitch.Value = sbp.Pitch;
                sld_speed.Value = sbp.Speed;
                sld_volume.Value = sbp.Volume;
                txt_pitch.Text = sbp.Pitch.ToString();
                txt_speed.Text = sbp.Speed.ToString();
                txt_volume.Text = sbp.Volume.ToString();
            }
        }




        private void Sld_speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (txt_speed != null)
                txt_speed.Text = (int)sld_speed.Value + "";



        }

        private void Sld_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (txt_volume != null)
                txt_volume.Text = (int)sld_volume.Value + "";

        }

        private void Sld_pitch_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (txt_pitch != null)
                txt_pitch.Text = (int)sld_pitch.Value + "";
        }

        private void Txt_speed_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            try
            {
                string strNum = t.Text;
                if ("" == strNum || null == strNum)
                {
                    return;
                }
                int num = int.Parse(t.Text);
                t.Text = num.ToString();
                if (num <= 100)
                {
                    sld_speed.Value = int.Parse(txt_speed.Text);

                    return;
                }
                else
                {
                    t.Text = t.Text.Remove(2);
                    t.SelectionStart = t.Text.Length;
                }
            }
            catch (Exception)
            {
                // MessageBox.Show(ex.ToString());
            }


        }

        private void Txt_volume_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            try
            {
                string strNum = t.Text;
                if ("" == strNum || null == strNum)
                {
                    return;
                }
                int num = int.Parse(t.Text);
                t.Text = num.ToString();
                if (num <= 100)
                {
                    sld_volume.Value = int.Parse(txt_volume.Text);
                    return;
                }
                else
                {
                    t.Text = t.Text.Remove(2);
                    t.SelectionStart = t.Text.Length;
                }

            }
            catch (Exception)
            {
                //  MessageBox.Show(ex.ToString());
            }



        }

        private void Txt_pitch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            try
            {
                string strNum = t.Text;
                if ("" == strNum || null == strNum)
                {
                    return;
                }
                int num = int.Parse(t.Text);
                t.Text = num.ToString();
                if (num <= 100)
                {
                    sld_pitch.Value = int.Parse(txt_pitch.Text);
                    return;
                }
                else
                {
                    t.Text = t.Text.Remove(2);
                    t.SelectionStart = t.Text.Length;
                }

            }
            catch (Exception)
            {
                //  MessageBox.Show(ex.ToString());
            }
        }

        private void Txt_speed_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }

        private void Txt_volume_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }

        private void Txt_pitch_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }
    }
}
