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
    public partial class ProductsPage : Page,INotifyPropertyChanged
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));   
        }
         

        public ObservableCollection<BO.ProductForList?> myListProduct
        {
            get
            {
                if (AttributeSelector.SelectedItem.ToString() == BO.Enums.Category.NONE.ToString())
                {
                    return new ObservableCollection<BO.ProductForList?>(Bl!.Product.GetProductList());

                }
                else
                    return new ObservableCollection<BO.ProductForList?>(Bl!.Product.GetListByCondition(x => x?.Category.ToString() == AttributeSelector.SelectedItem.ToString()));
            }
        }

        public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.Category));}}
        public ProductsPage()
        {
            InitializeComponent();          
        }
        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(myListProduct));
        }

        private void doubleClick_Update(object sender, MouseButtonEventArgs e)
        {

            if (ProductsListView.SelectedItem != null)
            {
                int id = ((BO.ProductForList)ProductsListView.SelectedItem).ID;
                new AddAndUpdate(id).Show();  
                (Window.GetWindow(this)).Close();
                OnPropertyChanged(nameof(myListProduct));   
            }
            else
                MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void DeleteProductManager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = ((BO.ProductForList)ProductsListView.SelectedItem).ID;
                Bl?.Product.DeleteProduct(id);
                deleteEx.Text = "Product deleted successfully";
                deleteEx.Visibility = Visibility.Visible;
                OnPropertyChanged(nameof(myListProduct));
            }
            catch (BO.CanNotDeleteProductException)
            {
                deleteEx.Text = "Can't delete a product because it's in the middle of an order.";
                deleteEx.Visibility = Visibility.Visible;
            }
        }
    }
}
