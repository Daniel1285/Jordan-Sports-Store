﻿using System;
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
    /// Interaction logic for OrderItemView.xaml
    /// </summary>
    public partial class OrderItemView : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public List<BO.OrderItem?> myListOrderItem;
        
        
        public OrderItemView(int OrderId)
        {
            InitializeComponent();
            myListOrderItem = Bl.Order.GetOrder(OrderId).Items!;
           // OrdersListView.ItemsSource = myListOrderItem;
        }
    }
}
