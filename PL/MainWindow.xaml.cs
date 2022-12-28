
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            new PlProduct.ListProduct().Show();

            this.Close();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.PlProduct.OrdersManager().Show();
            this.Close();
        }

        private void TruckingButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.PlProduct.TruckingOrder().Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
