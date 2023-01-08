using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class MainCartView : Window, INotifyPropertyChanged
    {
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        //private ObservableCollection<BO.ProductItem?> _myListProductItem;
        public ObservableCollection<BO.ProductItem?> myListProductItem
        {
            get
            {
                if (ProductInfromation.SelectedItem.ToString() == BO.Enums.Category.NONE.ToString())
                {
                    return new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListOfProductItem(TempCart)!);
                }
                else
                {
                    var group = (Bl?.Product.GetListProductIGrouping(Bl?.Product.GetListOfProductItem(TempCart) ?? throw new NullReferenceException()) ?? throw new NullReferenceException()).ToList();
                    foreach (var g in group)
                    {
                        if (g.Key == (BO.Enums.Category)ProductInfromation.SelectedItem)
                        {
                            return new ObservableCollection<BO.ProductItem?>(g);

                        }
                    }
                    return new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListOfProductItem(TempCart)!);

                }


            }


        }
        public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.Category)); } }
        public BO.Cart TempCart = new BO.Cart();
        

        public MainCartView()
        {
            TempCart.Items = new ObservableCollection<BO.OrderItem?>().ToList();
            //_myListProductItem = new ObservableCollection<BO.ProductItem?> (Bl.Product.GetListOfProductItem(TempCart));
            InitializeComponent();
            //SetProductComboBox();
            TempCart.Items = new List<BO.OrderItem?>();
        }
        public MainCartView(BO.Cart cart)
        {
            TempCart = cart;
            //_myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl.Product.GetListOfProductItem(TempCart));
            InitializeComponent();
            //SetProductComboBox();

        }
        //public void SetProductComboBox()
        //{
        //    for (int i = 0; i <= 4; i++) { ProductInfromation.Items.Add($"{(BO.Enums.Category)i}"); }
        //    ProductInfromation.Items.Add("All");
        //}

        /// <summary>
        /// Filter by category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductInfromation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var group = (Bl?.Product.GetListProductIGrouping(Bl?.Product.GetListOfProductItem(TempCart) ?? throw new NullReferenceException()) ?? throw new NullReferenceException()).ToList();
            //myListProductItem.Clear();
            //OnPropertyChanged(nameof(myListProductItem));
            //if (ProductInfromation.SelectedItem.ToString() == BO.Enums.Category.NONE.ToString())
            //{

            //    foreach (var x in Bl?.Product.GetListOfProductItem(TempCart)!)
            //        myListProductItem.Add(x);
            //    OnPropertyChanged(nameof(myListProductItem));

            //}
            //else
            //{
            //    foreach (var g in group)
            //    {
            //        if (g.Key == (BO.Enums.Category)ProductInfromation.SelectedItem)
            //        {
            //            foreach (var shorts in g)
            //                myListProductItem.Add(shorts);
            //            OnPropertyChanged(nameof(myListProductItem));

            //        }
            //    }
            //}
            OnPropertyChanged(nameof(myListProductItem));
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
            if (OrderItemListView.SelectedItem != null)
            {
               // BO.ProductItem p = new BO.ProductItem();

                int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
                Bl?.Cart.AddProdctToCatrt(TempCart, id);
                //OnPropertyChanged(nameof(_myListProductItem));
                Bl?.Product.GetListOfProductItem(TempCart);
               // OnPropertyChanged(nameof(_myListProductItem));
                
            }
            else
                MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);

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
