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
        public ObservableCollection<BO.OrderItem?> myListOrderItem { get; set; }
        
        
        public OrderItemView(int OrderId)
        {
            myListOrderItem = new ObservableCollection<BO.OrderItem?>(Bl.Order.GetOrder(OrderId).Items!);
            InitializeComponent();
            
           // OrdersListView.ItemsSource = myListOrderItem;
        }
    }
}
