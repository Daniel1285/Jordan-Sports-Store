using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL.PagesManager
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class OrdersPage : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChangd(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public ObservableCollection<BO.OrderForList?> myListOrders => new ObservableCollection<BO.OrderForList?>(Bl!.Order.GetOrderLists());
        public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.OrderStatus)); } }
        public OrdersPage()
        {
           
            InitializeComponent();
             
        }
        private void doubleClick_orderItem(object sender, MouseButtonEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                OrdersListView.Visibility = Visibility.Hidden;
                OrderInformation.Visibility = Visibility.Hidden;

                int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
                MyOrderPage.Content = new OrderItemView(id);
            }
            else
                MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OrderInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //OrdersListView.ItemsSource = OrderInformation.SelectedItem.ToString() == "All" ? Bl?.Order.GetOrderLists() : Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString());
        }


        private void UpdateToSent_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
            Bl?.Order.ShippingUpdate(id);
            OnPropertyChangd(nameof(myListOrders));
        }

        private void UpdateToProvided_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
            Bl?.Order.SupplyUpdateOrder(id);
            OnPropertyChangd(nameof(myListOrders));
        }

    }
}
