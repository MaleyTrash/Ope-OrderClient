using System;
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

namespace Ope_OrderClient
{
    /// <summary>
    /// Interakční logika pro Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ApiClient cli = new ApiClient();
        Customer user;
        ObservableCollection<Order> orders;

        public Login()
        {
            InitializeComponent();

            Refresh_Click(null, null);
        }

        private async void RefreshList()
        {
            if (user == null) return;

            List<Order> ord = await cli.GetOrders();

            orders = new ObservableCollection<Order>(ord.FindAll(i => i.customer.id == user.id));
            OrderList.ItemsSource = orders;
        }

        private async void LoginUser()
        {
            Customer cus = new Customer();

            cus.firstName = LoginName.Text;
            cus.password = LoginPassword.Text;

            Customer logged = await cli.LoginCustomer(cus);
            
            if(logged.id < 1)
            {
                MessageBox.Show("Invalid credentials");
                return;
            }

            user = logged;
            RefreshList();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            try {
                object selected = OrderList.SelectedItem;

                if (selected == null) return;

                orders.Remove((Order)selected);
            }
            catch
            {
                return;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
        }
    }
}
