using BLL;
using Model;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_userName.Text.CompareTo("") == 0 || this.pb_userPwd.Password.CompareTo("") == 0){
                this.lbl_errorMsg.Content = "Please Fill in Empty Field(s)";
                return;
            }

            User user = Manager.GetUser(this.tb_userName.Text, this.pb_userPwd.Password);
            if (user !=null)
            {
                this.lbl_errorMsg.Content = "";
                Home home = new Home(user);
                this.Close();
                home.Show();
            }
            else
            {
                this.lbl_errorMsg.Content = "Wrong User Name or Password! Please try Again";
            }

        }


     
    }
}
