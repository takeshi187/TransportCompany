using System.Windows;

namespace TransportCompany
{
    public partial class LogInWin : Window
    {
        public LogInWin()
        {
            InitializeComponent();
        }

        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Login.Clear();
            PassBox_Password.Clear();
        }
    }
}
