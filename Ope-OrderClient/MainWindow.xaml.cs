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
    }
}
