using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RestTimer
{
    public partial class MainWindow : Window
    {
        public bool HardStop = false;
        public bool inWork = false;

        DataManager dm = new DataManager();

        //Window init
        public MainWindow()
        {
            InitializeComponent();
            UIAnull();

            //set window color theme
            BorderMain.BorderBrush = dm.GetAppThemeColor();
            RestText.Foreground = dm.GetAppThemeColor();
            RPB1.Foreground = dm.GetAppThemeColor();
            RPB2.Foreground = dm.GetAppThemeColor();
            StartBTN.Background = dm.GetAppThemeColor();
            StopBTN.Background = dm.GetAppThemeColor();
            ActiveCircle.Foreground = dm.GetAppThemeColor();
        }

        //Set UI timer 00.00
        private void UIAnull()
        {
            TimeTextBox.Text = FormatTime(dm.GetWorkSessionTimeSec());
            PercentTextBox.Text = "0%";
        }

        //Start btn Click event
        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            inWork = true;
            StartTimer();
        }

        private void StopBtnClick(object sender, RoutedEventArgs e)
        {
            inWork = false;
        }

        private async void StartTimer()
        {
            UIAnull();
            for (int PastSeconds = 0; PastSeconds <= dm.GetWorkSessionTimeSec(); PastSeconds++)
            {
                //Stop the timer
                if (!inWork) break;
                if(PastSeconds == dm.GetWorkSessionTimeSec())
                {
                    Helper.PlayWorkSessionFinishSound();
                    Window NotifMSG = new NotifMSG();
                    NotifMSG.Top = SystemParameters.WorkArea.Height - 100;
                    NotifMSG.Left = SystemParameters.WorkArea.Width - 350;
                    NotifMSG.Show();
                }

                //Set to UI new time(mm:ss) and new percent value 
                TimeTextBox.Text = FormatTime(dm.GetWorkSessionTimeSec() - PastSeconds);
                PercentTextBox.Text = $"{CalculatePastTimePercent(PastSeconds, dm.GetWorkSessionTimeSec())}%";
                RPB1.Value = CalculatePastTimePercent(PastSeconds, dm.GetWorkSessionTimeSec());
                RPB2.Value = CalculatePastTimePercent(PastSeconds, dm.GetWorkSessionTimeSec());


                await Task.Delay(500);
            }

            //Work sesion finish
            UIAnull();
        }


        /// <summary>
        /// Format time from seconds to mm:ss.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatTime(int num)
        {
            return $"{(num / 60 < 10 ? $"0{num / 60}" : $"{num / 60}")}:{(num % 60 < 10 ? $"0{num % 60}" : $"{num % 60}")}";
        }

        public double CalculatePastTimePercent(int PastSeconds, int FullSeconds)
        {
            if (FullSeconds == 0) return 0;
            return (Int32) (PastSeconds / (FullSeconds / 100M));
        }

        /// <summary>
        /// Close MainWindow and Open Settings window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Window Settings = new Settings();
            Settings.Top = this.Top;
            Settings.Left = this.Left;
            Settings.Show();
            this.Close();
        }

        //private async void Start(object sender, RoutedEventArgs e)
        //{
        //    if(inWork == false)
        //    {
        //        inWork = true;
        //        ActiveCircle.Foreground = dm.GetAppThemeColor();

        //        for (int i = 0; i <= dm.GetWorkSessionTimeSec(); i++)
        //        {
        //            await Task.Delay(1000);
        //            if (HardStop)
        //            {
        //                inWork = false;
        //                HardStop = false;
        //                return;
        //            } else
        //            {
                        
        //                TimeTextBox.Text = CalcTime(dm.GetWorkSessionTimeSec() - i);
        //                PercentTextBox.Text = $"{CalcPerc(i, dm.GetWorkSessionTimeSec())}%";

        //                RPB1.Value = CalcPerc(i, dm.GetWorkSessionTimeSec());
        //                RPB2.Value = CalcPerc(i, dm.GetWorkSessionTimeSec());

        //                if (CalcPerc(i, dm.GetWorkSessionTimeSec()) == 100)
        //                {
        //                    Helper.PlayWorkSessionFinishSound();
        //                    inWork = false;

        //                    //await Task.Delay(1000);
        //                    //double workHeight = SystemParameters.WorkArea.Height;
        //                    //double workWidth = SystemParameters.WorkArea.Width;
        //                    //this.Top = (workHeight - this.Height);
        //                    //this.Left = (workWidth - this.Width);

        //                    Window NotifMSG = new NotifMSG();
        //                    NotifMSG.Top = SystemParameters.WorkArea.Height - 100;
        //                    NotifMSG.Left = SystemParameters.WorkArea.Width - 350;
        //                    NotifMSG.Show();

        //                }
        //            }
        //        }
        //    }

        //}

        //private async void Stop(object sender, RoutedEventArgs e)
        //{
        //    if(inWork) HardStop = true;

        //    await Task.Delay(100);
        //    var bc = new BrushConverter();
        //    ActiveCircle.Foreground = (Brush)bc.ConvertFrom("#FFD7D7D7");
        //    TimeTextBox.Text = CalcTime(Props.time);
        //    PercentTextBox.Text = "0%";
        //    RPB1.Value = CalcPerc(0, Props.time);
        //    RPB2.Value = CalcPerc(0, Props.time);

           
        //}

        //Navigation menu buttons events
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Hide(object sender, RoutedEventArgs e)
        {
            myWin.WindowState = WindowState.Minimized;
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
