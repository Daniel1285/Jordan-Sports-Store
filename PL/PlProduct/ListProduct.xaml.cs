using BlApi;
using BlImplementation;
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
    /// Interaction logic for ListProduct.xaml
    /// </summary>
    public partial class ListProduct : Window
    {
        private IBl Bl = new BlImplementation.Bl();

        public ListProduct()
        {
            InitializeComponent();
            SetProductComboBox();
            ProductsListView.ItemsSource = Bl.Product.GetProductList();
            //ProductsListView.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        public void SetProductComboBox()
        {
            for(int i = 0; i < 4;i++) { AttributeSelector.Items.Add($"{(BO.Enums.Category)i}");}     
            AttributeSelector.Items.Add("All");
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = AttributeSelector.Text.ToString();
            
            /*
            switch (choice)
            {
                case "SHOES":
                    //ProductsListView.ItemsSource = Bl.Product.GetProductList();
                    break;
                case "SHIRTS":
                    //ProductsListView.ItemsSource = Bl.Product.GetProductList()

                    break;
                case "SHORTS":
                    //ProductsListView.ItemsSource = Bl.Product.GetAll()
                    break;
                case "HOODIES":
                    //ProductsListView.ItemsSource = Bl.Product.GetAll()  
                    break;
                case "SOCKS":
                    //ProductsListView.ItemsSource = Bl.Product.GetAll()
                    break;
                case "All":
                    //ProductsListView.ItemsSource = Bl.Product.GetAll()
                    break;

                default:
                    ProductsListView.ItemsSource = Bl.Product.GetProductList();
                    break;
            }
            */
            
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.PlProduct.AddAndUpdate().Show();
            this.Close();
        }

        private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            this.Close();
        }
    }
}
