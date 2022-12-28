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
using System.Windows.Shapes;

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for OrdersManager.xaml
    /// </summary>
    public partial class OrdersManager : Window
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public List<BO.OrderForList?> myListOrders;
        public OrdersManager()
        {
            InitializeComponent();
            SetProductComboBox();
            myListOrders = Bl.Order.GetOrderLists().ToList(); 
           
        }
        public void SetProductComboBox()
        {
            for (int i = 0; i <= 2; i++) { OrderInformation.Items.Add($"{(BO.Enums.OrderStatus)i}"); }
            OrderInformation.Items.Add("All");
        }
        private void doubleClick_orderItem(object sender, MouseButtonEventArgs e)
        {

        }

        private void OrderInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //OrdersListView.ItemsSource = Bl?.Order.GetOrderLists();
            OrdersListView.ItemsSource = OrderInformation.SelectedItem.ToString() == "All"? Bl?.Order.GetOrderLists(): Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString());
        }

        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();    
            this.Close();
        }
    }
}
