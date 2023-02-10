using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace RestTimer
{
    public partial class Settings : Window
    {
        Helper helper = new Helper();
        DataManager dm = new DataManager();

        public Settings()
        {
            InitializeComponent();
            TimeTextBox.Text = (dm.GetWorkSessionTimeSec() / 60).ToString();

            //set window color theme
            BorderMain.BorderBrush = dm.GetAppThemeColor();
            RestText.Foreground = dm.GetAppThemeColor();
            ApplyBTN.Background = dm.GetAppThemeColor();
        }

        // ApplyBtnClicked - Set new settings & close SettingsWindow & init MainWindow
        public void ApplyBtnClick(object sender, RoutedEventArgs e)
        {
            int NewWorkSessionTime = 0;
            bool IsInt_NewWorkSessionTime;
            IsInt_NewWorkSessionTime = Int32.TryParse(TimeTextBox.Text, out NewWorkSessionTime);

            if (IsInt_NewWorkSessionTime && NewWorkSessionTime > 0)
            {
                //get data from settings .txt
                string[] SettingsArr = File.ReadAllLines("Settings.txt");

                //set app color
                switch (ComboBoxColors.Text)
                {
                    case "Blue":
                        SettingsArr[2] = $"color:#5877E8";
                        break;
                    case "Red":
                        SettingsArr[2] = $"color:#B03838";
                        break;
                    case "Green":
                        SettingsArr[2] = $"color:#3A8535";
                        break;
                    default:
                        break;
                }

                //set work session time
                SettingsArr[1] = $"time:{NewWorkSessionTime*60}";

                //save App color & Work session time to settings.txt
                File.WriteAllLines("Settings.txt", SettingsArr);

                DataManager.DataUpdate();

                //Init main window & close settings window
                Window Main = new MainWindow();
                Main.Top = this.Top;
                Main.Left = this.Left;
                Main.Show();
                this.Close();
            }
        }

        //Mouse events
        private bool clicked = false;
        private Point lmAbs = new Point();
        void PnMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            clicked = true;
            this.lmAbs = e.GetPosition(this);
            this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
            this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
        }
        void PnMouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            clicked = false;
        }
        void PnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (clicked)
            {
                Point MousePosition = e.GetPosition(this);
                Point MousePositionAbs = new Point();
                MousePositionAbs.X = Convert.ToInt16(this.Left) + MousePosition.X;
                MousePositionAbs.Y = Convert.ToInt16(this.Top) + MousePosition.Y;
                this.Left = this.Left + (MousePositionAbs.X - this.lmAbs.X);
                this.Top = this.Top + (MousePositionAbs.Y - this.lmAbs.Y);
                this.lmAbs = MousePositionAbs;
            }
        }   
    }
}
