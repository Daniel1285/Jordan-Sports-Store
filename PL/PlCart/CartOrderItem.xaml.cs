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
        public ObservableCollection<BO.OrderItem?> myListOrderItem { get; set; }
        public BO.Cart temp = new BO.Cart();
        public CartOrderItem(BO.Cart c)
        {
            myListOrderItem = new ObservableCollection<BO.OrderItem?>(c.Items!);
            InitializeComponent();
            temp = c;
        }
        private void GoToConfrimOrder_Click(object sender, RoutedEventArgs e)
        {
            CartOrderItemFram.Content = new ConfrimOrder(temp);
        }
        private void BackToMainCartView_Click(object sender, RoutedEventArgs e)
        {
            new MainCartView(temp).Show();
            Window.GetWindow(this).Close();
        }
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem removeOrderItem = new BO.OrderItem();
            int id = ((BO.OrderItem)OrdersListView.SelectedItem).ID;
            removeOrderItem = myListOrderItem.FirstOrDefault(x => x?.ProductID == id) ?? throw new BO.NotExistException();
            temp.Items!.Remove(removeOrderItem);
        }
    }
}
