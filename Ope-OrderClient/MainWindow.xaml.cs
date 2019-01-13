using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Ope_OrderClient
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApiClient cli = new ApiClient();

        ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        ObservableCollection<Order> orders = new ObservableCollection<Order>();

        public MainWindow()
        {
            InitializeComponent();

            CustomerList.ItemsSource = customers;
            ItemList.ItemsSource = items;
            OrderList.ItemsSource = orders;

            UpdateCustomers();
            UpdateItems();
            UpdateOrders();

            new Login().Show();
        }

        public void ResetList<T>(ListView list, IEnumerable<T> data)
        {
            list.ItemsSource = new ObservableCollection<T>(data);
        }

        public async void UpdateCustomers()
        {
            ResetList(CustomerList, await cli.GetCustomers());
        }   

        public async void UpdateItems()
        {
            ResetList(ItemList, await cli.GetItems());
        }

        public async void UpdateOrders()
        {
            ResetList(OrderList, await cli.GetOrders());
        }

        private void CustomerList_Selected(object sender, RoutedEventArgs e)
        {
            Customer cus = CustomerList.SelectedItem as Customer;
            if (cus == null) return;

            CustomerId.Text = cus.id.ToString();
            CustomerFirstName.Text = cus.firstName;
            CustomerLastName.Text = cus.lastName;
            CustomerPassword.Text = cus.password;
        }

        private void ItemList_Selected(object sender, RoutedEventArgs e)
        {
            Item cus = ItemList.SelectedItem as Item;
            if (cus == null) return;

            ItemId.Text = cus.id.ToString();
            ItemName.Text = cus.name;
            ItemPrice.Text = cus.price.ToString();
        }

        private void OrderList_Selected(object sender, RoutedEventArgs e)
        {
            Order cus = OrderList.SelectedItem as Order;
            if (cus == null) return;

            string orderIds = cus.items[0].id.ToString();
            for(int i = 1; i < cus.items.Count; i++)
            {
                orderIds += $",{cus.items[i].id}";
            }

            OrderId.Text = cus.id.ToString();
            OrderCustomerId.Text = cus.customer.id.ToString();
            OrderItemIds.Text = orderIds;
        }

        private async void CustomerNew_Click(object sender, RoutedEventArgs e)
        {
            Customer i = new Customer();

            i.firstName = CustomerFirstName.Text;
            i.lastName = CustomerLastName.Text;
            i.password = CustomerPassword.Text;

            await cli.CreateCustomer(i);
            UpdateCustomers();
        }

        private async void CustomerUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = toId(CustomerId.Text);
            if (id < -1) return;

            Customer i = new Customer();

            i.id = id;
            i.firstName = CustomerFirstName.Text;
            i.lastName = CustomerLastName.Text;
            i.password = CustomerPassword.Text;

            await cli.PatchCustomer(i);
            UpdateCustomers();
        }

        private void CustomerDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = toId(CustomerId.Text);
            if (id < -1) return;

            cli.DeleteCustomerById(id);
            UpdateCustomers();
        }

        private async void ItemNew_Click(object sender, RoutedEventArgs e)
        {
            Item i = new Item();

            i.name = ItemName.Text;
            i.price = toId(ItemPrice.Text);

            await cli.CreateItem(i);
            UpdateItems();
        }

        private async void ItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = toId(CustomerId.Text);
            if (id < -1) return;

            Item i = new Item();

            i.id = id;
            i.name = ItemName.Text;
            i.price = toId(ItemPrice.Text);

            await cli.PatchItem(i);
            UpdateItems();
        }

        private void ItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = toId(ItemId.Text);
            if (id < -1) return;

            cli.DeleteItemById(id);
            UpdateItems();
        }

        private async void OrderNew_Click(object sender, RoutedEventArgs e)
        {
            Order i = new Order();

            int customerId = toId(OrderCustomerId.Text);
            if (customerId < 0) return;

            List<Item> items = parseItems(OrderItemIds.Text);
            if (items == null) return;

            Customer cus = new Customer();
            cus.id = customerId;

            i.customer = cus;
            i.items = items;

            await cli.CreateOrder(i);
            UpdateOrders();
        }

        private async void OrderUpdate_Click(object sender, RoutedEventArgs e)
        {
            Order i = new Order();

            int id = toId(OrderId.Text);
            if (id < -1) return;

            int customerId = toId(OrderCustomerId.Text);
            if (customerId < 0) return;

            List<Item> items = parseItems(OrderItemIds.Text);
            if (items == null) return;

            Customer cus = new Customer();
            cus.id = customerId;

            i.id = id;
            i.customer = cus;
            i.items = items;

            await cli.PatchOrder(i);
            UpdateOrders();
        }

        private void OrderDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = toId(OrderId.Text);
            if (id < -1) return;

            cli.DeleteOrderById(id);
            UpdateOrders();
        }

        private List<Item> parseItems(string s)
        {
            List<Item> items = new List<Item>();
            foreach (string item in OrderItemIds.Text.Split(','))
            {
                int id = toId(item);
                if (id < 0)
                {
                    MessageBox.Show("Spatny id...");
                    return null;
                }

                Item itm = new Item();
                itm.id = id;

                items.Add(itm);
            }

            return items;
        }

        private int toId(string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch
            {
                return -1;
            }
        }
    }
}
