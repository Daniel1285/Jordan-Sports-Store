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
using System.Windows.Shapes;

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for TruckingOrder.xaml
    /// </summary>
    public partial class TruckingOrder : Window
    {
        public TruckingOrder()
        {
            InitializeComponent();
        }

        private void Trucking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Truckingresult_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
