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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Password
{
    //Jan Bezouška

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strPassword;
        public string Password
        {
            private get { return strPassword; }
            set
            {
                if (!(value.Length > 5) | !Regex.IsMatch(value, @".*[0-9]+.*"))
                {
                    bool err = false;
                    tbInfo.Foreground = Brushes.Red;

                    if (!(value.Length > 5))
                    {
                        tbInfo.Text = "Heslo je moc krátké";
                        err = true;
                    }
                    if(!Regex.IsMatch(value, @".*[0-9]+.*"))
                    {
                        if (err)
                            tbInfo.Text += "\n";
                        tbInfo.Text += "Heslo neobsahuje číslici";
                    }
                }
                else 
                {
                    strPassword = value;
                    butEnd.Visibility = Visibility.Visible;

                    tbInfo.Foreground = Brushes.Black;
                    tbInfo.Text = "správné heslo";
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void butLogin_Click(object sender, RoutedEventArgs e)
        {
            tbInfo.Text = string.Empty;
            Password = pbPassword.Password;
        }

        private void butEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
