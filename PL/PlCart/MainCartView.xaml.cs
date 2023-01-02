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

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for MainCartView.xaml
    /// </summary>
    public partial class MainCartView : Window
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public List<BO.ProductItem?> myListProductItem;
        public MainCartView()
        {
            InitializeComponent();
            SetProductComboBox();
            //myListProductItem = Bl.Product.GetProduct();
        }
        public void SetProductComboBox()
        {
            for (int i = 0; i <= 4; i++) { AttributeSelector.Items.Add($"{(BO.Enums.Category)i}"); }
            AttributeSelector.Items.Add("All");
        }
        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();    
            this.Close();
        }

        private void OrderInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GoToCartOrderitem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
