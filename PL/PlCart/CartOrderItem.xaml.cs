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

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for CartOrderItem.xaml
    /// </summary>
    public partial class CartOrderItem : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public List<BO.OrderItem?> myListOrderItem;
        public CartOrderItem()
        {
            InitializeComponent();
            listProductForClient();
            
        }

        public void listProductForClient()
        {
            
        }

        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConfrimOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
