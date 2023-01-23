
using PL.PlCart;
using PL.PlCart.TruckingOrder;
using PL.Simulator123;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PL
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart Cart = new BO.Cart();
        public MainWindow()
        {
            Cart.Items = new ObservableCollection<BO.OrderItem?>().ToList();
            InitializeComponent();
        }        
        public MainWindow(BO.Cart c)
        {
            Cart = c;
            InitializeComponent();

        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            new PlProduct.AdminView().Show();
            this.Close();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            mainFram.Content = new MainCartViewPage(Cart);
        }

        private void TrackingButton_Click(object sender, RoutedEventArgs e)
        {
            mainFram.Content =  new MainTrucking();
        }
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            mainFram.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            new Simulator123.Simulator123().Show();
        }
    }
}
