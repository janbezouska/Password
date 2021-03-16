using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace Password
{
    //Jan Bezouška

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> logDic = new Dictionary<string, string>();

        bool passwordSet = false;
        bool errPassLenght = false;
        bool errPassNum = false;
        private string strPassword;
        public string Password
        {
            private get { return strPassword; }
            set
            {
                errPassLenght = false;
                errPassNum = false;
                tbInfo.Foreground = Brushes.Red;

                if (!(value.Length > 5) | !Regex.IsMatch(value, @".*[0-9]+.*"))
                {
                    tbInfo.Text = "Heslo je moc krátké";
                    errPassLenght = true;
                }
                if(!Regex.IsMatch(value, @".*[0-9]+.*"))
                {
                    if (errPassLenght)
                        tbInfo.Text += "\n";
                    tbInfo.Text += "Heslo neobsahuje číslici";
                    errPassNum = true;
                }
                if(!(errPassNum | errPassLenght))
                {
                    strPassword = value;
                    passwordSet = true;

                    tbInfo.Foreground = Brushes.Black;
                    //tbInfo.Text = "správné heslo";
                }
            }
        }

        bool usernameSet = false;
        bool errUnContains = false;
        bool errUnLenght = false;
        private string strUsername;
        public string Username
        {
            get { return strUsername; }
            set
            {
                bool errUnContains = false;
                bool errUnLenght = false;

                if (!(value.Length > 2))
                {
                    tbInfo.Foreground = Brushes.Red;
                    if (errPassLenght | errPassNum)
                        tbInfo.Text += "\n";
                    tbInfo.Text += "Username musí mít alespoň tři znaky";
                    errUnLenght = true;
                }
                if(logDic.ContainsKey(value))
                {
                    tbInfo.Foreground = Brushes.Red;
                    if (errPassLenght | errPassNum | errUnLenght)
                        tbInfo.Text += "\n";
                    tbInfo.Text += "username již existuje";
                    errUnContains = true;
                }
                else if(!errUnLenght)
                {
                    strUsername = value;
                    usernameSet = true;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void butLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void butSingup_Click(object sender, RoutedEventArgs e)
        {
            Singup();
        }

        private void butEnd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Singup()
        {
            tbInfo.Text = string.Empty;

            Password = pbPassword.Password;
            Username = tbName.Text;

            if (passwordSet && usernameSet)
            {
                logDic.Add(Username, Password);
                tbInfo.Text = "jméno a heslo přidáno";
            }

            passwordSet = false;
            usernameSet = false;
        }

        public void Login()
        {
            tbInfo.Text = string.Empty;

            if(logDic.ContainsKey(tbName.Text))
            {
                if(logDic[tbName.Text] == pbPassword.Password)
                {
                    tbInfo.Foreground = Brushes.Black;
                    tbInfo.Text = "přihlášení proběhlo úspěšně";

                    butEnd.Visibility = Visibility.Visible;
                }
                else
                {
                    tbInfo.Foreground = Brushes.Red;
                    tbInfo.Text = "špatné heslo";
                }
            }
            else
            {
                tbInfo.Foreground = Brushes.Red;
                tbInfo.Text = "neexistující username";
            }
        }
    }
}
