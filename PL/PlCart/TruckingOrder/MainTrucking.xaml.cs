using PL.PagesManager;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace PL.PlCart.TruckingOrder
{
    /// <summary>
    /// Interaction logic for MainTrucking.xaml
    /// </summary>
    public partial class MainTrucking : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public BO.Order order = new BO.Order();
        public MainTrucking()
        {
            InitializeComponent();
        }

        private void Trucking_Click(object sender, RoutedEventArgs e)
        {
            int IDTrucking = int.Parse(truck.Text);
            try
            {
                BO.Order? a = new BO.Order();
                a = Bl?.Order.GetOrder(IDTrucking);
                ResultTrucking.Text = "";
                ResultTrucking.Text = (a?.ID + "\n" + a?.OrderDate + "\n" + a?.Status).ToString();
                order = Bl?.Order.GetOrder(IDTrucking)!;
                truckingOrder.Visibility = Visibility.Hidden;
                ViewOrderItemButton.Visibility = Visibility.Visible;
                order = Bl.Order.GetOrder(IDTrucking);
            }
            catch (BO.NotExistException)
            {
                ResultTrucking.Text = "ID not found !" + "\n   please try again.";
            }
        }

        private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Window.GetWindow(this).Close();  
        }

        private void ViewOrderItem_Click(object sender, RoutedEventArgs e)
        {
            truckingFram.Content = new OrderItemView(order.ID);
            ViewOrderItemButton.Visibility = Visibility.Hidden;
        }

        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            truckingFram.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}
