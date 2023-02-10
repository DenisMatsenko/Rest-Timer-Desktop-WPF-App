using System.Windows;

namespace RestTimer
{
    public partial class NotifMSG : Window
    {
        public NotifMSG()
        {
            InitializeComponent();
        }

        private void CloseNotificationWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
