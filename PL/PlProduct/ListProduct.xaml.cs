using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            ProductsListView.ItemsSource = AttributeSelector.SelectedItem.ToString() == "All"? Bl.Product.GetProductList() : Bl.Product.GetListByCondition(X => X?.Category.ToString() == AttributeSelector.SelectedItem.ToString());              
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new AddAndUpdate().Show();
        
            this.Close();
        }


        private void doubleClick_Update(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)ProductsListView.SelectedItem).ID;
            new AddAndUpdate(id).Show();
            this.Close();
        }
        
        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        
        private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            this.Close();
        }
    }
}
