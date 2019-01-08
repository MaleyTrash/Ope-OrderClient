using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            testCustomer();
            testOrder();
        }

        private async void testCustomer()
        {
            ApiClient ac = new ApiClient();

            List<Customer> c = await ac.GetCustomers();

            Customer c2 = await ac.GetCustomerById(3);

            Customer newC = new Customer();
            newC.firstName = "Test";
            newC.lastName = "FromC#";
            Customer c3 = await ac.CreateCustomer(newC);

            c3.firstName = "TestPatch";
            Customer c4 = await ac.PatchCustomer(c3);

            ac.DeleteCustomerById(c4.id);
        }

        private async void testOrder()
        {
            ApiClient ac = new ApiClient();

            Customer customer = (await ac.GetCustomers())[0];
            List<Item> items = await ac.GetItems();

            List<Order> o = await ac.GetOrders();

            Order o2 = await ac.GetOrderById(3);

            Order newO = new Order();
            newO.items = items;
            newO.customer = customer;
            Order o3 = await ac.CreateOrder(newO);

            o3.paid = true;
            Order o4 = await ac.PatchOrder(o3);
        }
    }
}
