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
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {
        public AddAndUpdate()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

        }

        private void ProductIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductInstokeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddProductToList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackLastWindow(object sender, RoutedEventArgs e)
        {
            new PL.PlProduct.ListProduct().Show();
            this.Close();
        }
    }
}
