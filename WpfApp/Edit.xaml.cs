using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        Contact contact;
        User user;
        public Edit(Contact contact,User user)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.user = user;
            this.contact = contact;
            this.lbl_ID.Content = contact.ID;
            this.tb_name.Text = contact.Name;
            this.tb_phoneNo.Text = contact.PhoneNo.ToString();
            this.tb_email.Text = contact.Email;
            this.tb_address.Text = contact.Address;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            Home home = new Home(this.user);
            home.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string validationMsg;
            if (!ValidateInputs(out validationMsg))
            {
                this.lbl_validationMsg.Content = validationMsg;
                return;
            }

            this.contact.Name = this.tb_name.Text;
            this.contact.PhoneNo = long.Parse(this.tb_phoneNo.Text);
            this.contact.Email = this.tb_email.Text;
            this.contact.Address = this.tb_address.Text;
            Manager.EditContact(this.contact.ID, this.contact);
            GoBack();
        }
        private bool ValidateInputs(out string message)
        {
            Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (this.tb_name.Text.CompareTo("") == 0
                || this.tb_phoneNo.Text.CompareTo("") == 0
                || this.tb_email.Text.CompareTo("") == 0
                || this.tb_address.Text.CompareTo("") == 0
                )
            {
                message = "Please Fill in all Fields";
                return false;
            }
            else if (this.tb_phoneNo.Text.Length != 10)
            {
                message = "Please Fill in Valid Phone No.";
                return false;
            }
            else if (!regexEmail.Match(this.tb_email.Text).Success)
            {
                message = "Please Fill in Valid Email";
                return false;
            }
            message = "Success";
            return true;
        }
    }
}
