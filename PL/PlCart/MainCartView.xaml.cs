using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for MainCartView.xaml
    /// </summary>
    public partial class MainCartView : Window
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductItem?> myListProductItem { get; set; }
        public ObservableCollection<BO.OrderItem?> myListOrderItem { get; set; }

        public BO.Cart TempCart = new BO.Cart();
        
        public MainCartView()
        {
            myListProductItem = new ObservableCollection<BO.ProductItem?> (Bl.Product.GetListOfProductItem());
            InitializeComponent();
            SetProductComboBox();
            TempCart.Items = new List<BO.OrderItem?>();
        }
        public void SetProductComboBox()
        {
            for (int i = 0; i <= 4; i++) { ProductInfromation.Items.Add($"{(BO.Enums.Category)i}"); }
            ProductInfromation.Items.Add("All");
        }

        private void ProductInfromation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderItemListView.ItemsSource = ProductInfromation.SelectedItem.ToString() == "All" ? Bl?.Product.GetListOfProductItem() : Bl?.Product.GetListByConditionForProductItem(X => X?.Category.ToString() == ProductInfromation.SelectedItem.ToString());

        }
        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();    
            this.Close();
        }

        private void GoToCartOrderitem_Click(object sender, RoutedEventArgs e)
        {
            OrderItemListView.Visibility= Visibility.Hidden;
            ProductInfromation.Visibility= Visibility.Hidden;
            Label_P.Visibility= Visibility.Hidden;
            buttonCart.Visibility= Visibility.Hidden;
            backOfMCart.Visibility  = Visibility.Hidden;
            FramCart.Content = new CartOrderItem(TempCart);
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            Bl?.Cart.AddProdctToCatrt(TempCart, id);
            myListProductItem.FirstOrDefault(x => x?.ID == id).Amount += 1;
            //Bl?.Cart.UpdateAmountOfProduct(TempCart, id,1);
            //myListOrderItem = new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListProductItemInCart(TempCart).ToList()!);
            //myListOrderItem = new ObservableCollection<BO.OrderItem?>(TempCart.Items!);
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
