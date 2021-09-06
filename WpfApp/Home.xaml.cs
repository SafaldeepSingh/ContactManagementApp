using BLL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        Contact[] contacts;
        TextBlock[] tb_IDs;
        TextBlock[] tb_names;
        TextBlock[] tb_phoneNos;
        TextBlock[] tb_emails;
        TextBlock[] tb_addresses;
        Button[] btn_del;
        Button[] btn_edit;
        string searchBy;
        User user;
        public Home(User user)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.user = user;
            this._home.Title = "Home- Hi, " + user.Name;
            this.contacts = Manager.GetAllContacts();
            populateGridView();
            populateComboBoxes();
        }

        private void populateComboBoxes()
        {
            this.cb_sortBy.Items.Add("------");
            this.cb_sortBy.Items.Add("Name: A-Z");
            this.cb_sortBy.Items.Add("Name: Z-A");
            this.cb_sortBy.Items.Add("Phone No: Ascending");
            this.cb_sortBy.Items.Add("Phone No: Descending");
            this.cb_sortBy.Items.Add("Email: A-Z");
            this.cb_sortBy.Items.Add("Email: Z-A");
            this.cb_sortBy.Items.Add("Address: A-Z");
            this.cb_sortBy.Items.Add("Address: Z-A");
            this.cb_sortBy.SelectedIndex = 0;

            this.cb_searchBy.Items.Add("All");
            this.cb_searchBy.Items.Add("ID");
            this.cb_searchBy.Items.Add("Name");
            this.cb_searchBy.Items.Add("Phone No");
            this.cb_searchBy.Items.Add("Email");
            this.cb_searchBy.Items.Add("Address");
            this.cb_searchBy.SelectedIndex = 0;
            this.searchBy = "All";
        }

        private void populateGridView()
        {
            initializeDataFields();
            for (int i = 0; i < contacts.Length; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                this.gContacts.RowDefinitions.Add(rd);

                tb_IDs[i] = new TextBlock();
                tb_IDs[i].Text = contacts[i].ID.ToString();
                this.gContacts.Children.Add(tb_IDs[i]);
                Grid.SetRow(tb_IDs[i], i + 1);
                Grid.SetColumn(tb_IDs[i], 0);

                tb_names[i] = new TextBlock();
                tb_names[i].Text = contacts[i].Name;
                this.gContacts.Children.Add(tb_names[i]);
                Grid.SetRow(tb_names[i], i + 1);
                Grid.SetColumn(tb_names[i], 1);

                tb_phoneNos[i] = new TextBlock();
                tb_phoneNos[i].Text = contacts[i].PhoneNo.ToString();
                this.gContacts.Children.Add(tb_phoneNos[i]);
                Grid.SetRow(tb_phoneNos[i], i + 1);
                Grid.SetColumn(tb_phoneNos[i], 2);

                tb_emails[i] = new TextBlock();
                tb_emails[i].Text = contacts[i].Email;
                this.gContacts.Children.Add(tb_emails[i]);
                Grid.SetRow(tb_emails[i], i + 1);
                Grid.SetColumn(tb_emails[i], 3);

                tb_addresses[i] = new TextBlock();
                tb_addresses[i].Text = contacts[i].Address;
                this.gContacts.Children.Add(tb_addresses[i]);
                Grid.SetRow(tb_addresses[i], i + 1);
                Grid.SetColumn(tb_addresses[i], 4);

                if (user.Role == Role.Administrator)
                {
                    btn_edit[i] = new Button();
                    btn_edit[i].Content = "Edit";
                    btn_edit[i].Margin = new Thickness(0, 3, 0, 3);
                    btn_edit[i].Click += new RoutedEventHandler(this.EditBtnHandler);
                    btn_edit[i].Name = "e" + i;
                    this.gContacts.Children.Add(btn_edit[i]);
                    Grid.SetRow(btn_edit[i], i + 1);
                    Grid.SetColumn(btn_edit[i], 5);

                    btn_del[i] = new Button();
                    btn_del[i].Content = "Delete";
                    btn_del[i].Margin = new Thickness(5, 3, 0, 3);
                    btn_del[i].Click += new RoutedEventHandler(this.DeleteBtnHandler);
                    btn_del[i].Name = "b" + contacts[i].ID;
                    this.gContacts.Children.Add(btn_del[i]);
                    Grid.SetRow(btn_del[i], i + 1);
                    Grid.SetColumn(btn_del[i], 6);

                }
            }
        }

        private void initializeDataFields()
        {
            tb_IDs = new TextBlock[this.contacts.Length];
            tb_names = new TextBlock[this.contacts.Length];
            tb_phoneNos = new TextBlock[this.contacts.Length];
            tb_emails = new TextBlock[this.contacts.Length];
            tb_addresses = new TextBlock[this.contacts.Length];
            if (user.Role == Role.Administrator)
            {

                btn_del = new Button[this.contacts.Length];
                btn_edit = new Button[this.contacts.Length];
            }
        }

        private void Tb_searchContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearGridData();
            //tmp fix ArrayList to Array
            ArrayList arrayList = Manager.SearchContacts(this.tb_searchContact.Text,this.searchBy);
            this.contacts = new Contact[arrayList.Count];
            for(int i = 0; i < arrayList.Count; i++)
            {
                this.contacts[i] = (Contact)arrayList[i];
            }
            populateGridView();
        }

        private void clearGridData()
        {
            for (int i = 0; i < this.contacts.Length; i++)
            {
                this.gContacts.Children.Remove(this.tb_IDs[i]);
                this.gContacts.Children.Remove(this.tb_names[i]);
                this.gContacts.Children.Remove(this.tb_phoneNos[i]);
                this.gContacts.Children.Remove(this.tb_emails[i]);
                this.gContacts.Children.Remove(this.tb_addresses[i]);
                if (user.Role == Role.Administrator)
                {

                    this.gContacts.Children.Remove(this.btn_del[i]);
                    this.gContacts.Children.Remove(this.btn_edit[i]);
                }
            }
            for (int i = 1; i < this.gContacts.RowDefinitions.Count; i++)
            {
                this.gContacts.RowDefinitions.Remove(this.gContacts.RowDefinitions[i]);
            }
        }

        private void Cb_sortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.contacts = Manager.SortContacts(contacts, this.cb_sortBy.SelectedItem.ToString());
            clearGridData();
            populateGridView();
        }
        private void DeleteBtnHandler(object sender, EventArgs e) {
            int contactID = int.Parse(((Button)sender).Name.Substring(1));
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Do you want to delete this contact", "Delete Contact", buttons);
            if (result == MessageBoxResult.Yes)
            {
                if (Manager.DeleteContact(contactID))
                {
                    clearGridData();

                    this.contacts = Manager.GetAllContacts();
                    populateGridView();
                }
            }

        }
        private void EditBtnHandler(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).Name.Substring(1));
            Edit edit = new Edit(this.contacts[index],user);
            edit.Show();
            this.Close();
        }
        private void Cb_searchBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.searchBy = this.cb_searchBy.SelectedItem.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add(user);
            add.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
