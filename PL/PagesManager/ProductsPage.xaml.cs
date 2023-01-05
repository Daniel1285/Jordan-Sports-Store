using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
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

namespace PL.PagesManager
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> myListProduct { get; set;}
        public Array Categories { get {return Enum.GetValues(typeof(BO.Enums.Category)); }
}
        public ProductsPage()
        {
            myListProduct = new ObservableCollection<BO.ProductForList?>(Bl.Product.GetProductList());
            InitializeComponent();
            //SetProductComboBox();
        }

        //public void SetProductComboBox()
        //{
        //    for (int i = 0; i <= 4; i++) { AttributeSelector.Items.Add($"{(BO.Enums.Category)i}"); }
        //    AttributeSelector.Items.Add("All");
        //}

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductsListView.ItemsSource = AttributeSelector.SelectedItem.ToString() == "All" ? Bl?.Product.GetProductList() : Bl?.Product.GetListByCondition(X => X?.Category.ToString() == AttributeSelector.SelectedItem.ToString());
        }

        private void doubleClick_Update(object sender, MouseButtonEventArgs e)
        {

            if (ProductsListView.SelectedItem != null)
            {
                int id = ((BO.ProductForList)ProductsListView.SelectedItem).ID;
                new AddAndUpdate(id).Show();  
                (Window.GetWindow(this)).Close();

            }
            else
                MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteProductManager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = ((BO.ProductForList)ProductsListView.SelectedItem).ID;
                Bl?.Product.DeleteProduct(id);
                deleteEx.Text = "Product deleted successfully";
                deleteEx.Visibility = Visibility.Visible;

            }
            catch (BO.CanNotDeleteProductException)
            {
                deleteEx.Text = "Can't delete a product because it's in the middle of an order.";
                deleteEx.Visibility = Visibility.Visible;
            }
        }
    }
}
