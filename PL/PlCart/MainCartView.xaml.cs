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

        public BO.Cart TempCart = new BO.Cart();

        public MainCartView()
        {
            myListProductItem = new ObservableCollection<BO.ProductItem?> (Bl.Product.GetListOfProductItem());
            InitializeComponent();
            SetProductComboBox();
            TempCart.Items = new List<BO.OrderItem?>();
        }
        public MainCartView(BO.Cart cart)
        {
            TempCart = cart;
            myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl.Product.GetListOfProductItem());
            InitializeComponent();
            SetProductComboBox();

        }
        public void SetProductComboBox()
        {
            for (int i = 0; i <= 4; i++) { ProductInfromation.Items.Add($"{(BO.Enums.Category)i}"); }
            ProductInfromation.Items.Add("All");
        }

        /// <summary>
        /// Filter by category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductInfromation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderItemListView.ItemsSource = ProductInfromation.SelectedItem.ToString() == "All" ? Bl?.Product.GetListOfProductItem() : Bl?.Product.GetListByConditionForProductItem(X => X?.Category.ToString() == ProductInfromation.SelectedItem.ToString());
        }

        /// <summary>
        /// Back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();    
            this.Close();
        }

        /// <summary>
        /// Open page of Order item in the Cart. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToCartOrderitem_Click(object sender, RoutedEventArgs e)
        {
            OrderItemListView.Visibility= Visibility.Hidden;
            ProductInfromation.Visibility= Visibility.Hidden;
            Label_P.Visibility= Visibility.Hidden;
            buttonCart.Visibility= Visibility.Hidden;
            backOfMCart.Visibility  = Visibility.Hidden;
            FramCart.Content = new CartOrderItem(TempCart);
        }

        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            BO.ProductItem p = new BO.ProductItem();
            
            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            Bl?.Cart.AddProdctToCatrt(TempCart, id);
            myListProductItem.FirstOrDefault(x => x?.ID == id)!.Amount += 1;
        }

        /// <summary>
        /// Remove product from cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.NotExistException"></exception>
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem removeOrderItem = new BO.OrderItem();
            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            removeOrderItem = TempCart.Items!.FirstOrDefault(x => x?.ProductID == id) ?? throw new BO.NotExistException();
            TempCart.Items!.Remove(removeOrderItem);
        }

    }
}
