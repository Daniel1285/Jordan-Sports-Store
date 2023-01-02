﻿using PL.PlProduct;
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

namespace PL.PagesManager
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {

        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public List<BO.OrderForList?> myListOrders;
        public OrdersPage()
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
            //OrdersListView.ItemsSource = Bl?.Order.GetOrderLists();
            OrdersListView.ItemsSource = OrderInformation.SelectedItem.ToString() == "All" ? Bl?.Order.GetOrderLists() : Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString());
        }


        private void UpdateToSent_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
            Bl?.Order.ShippingUpdate(id);
            this.NavigationService.Refresh();
            
        }

        private void UpdateToProvided_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
            Bl?.Order.SupplyUpdateOrder(id);
        }

    }
}
