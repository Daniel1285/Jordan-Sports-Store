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

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for CartOrderItem.xaml
    /// </summary>
    public partial class CartOrderItem : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        //public List<BO.OrderItem?> myOrderItem { get; set; }
        public BO.Cart temp = new BO.Cart();
        public CartOrderItem(BO.Cart c)
        {
            InitializeComponent();
            OrdersListView.ItemsSource = c.Items!;
            temp = c;
        }


        private void GoToConfrimOrder_Click(object sender, RoutedEventArgs e)
        {
            CartOrderItemFram.Content = new ConfrimOrder(temp);
        }

        private void BackToMainCartView_Click(object sender, RoutedEventArgs e)
        {
            new MainCartView().Show();
            Window.GetWindow(this).Close();
        }
    }
}
