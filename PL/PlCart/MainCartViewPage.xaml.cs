using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.PlCart;

/// <summary>
/// Interaction logic for MainCartViewPage.xaml
/// </summary>
public partial class MainCartViewPage : Page, INotifyPropertyChanged    
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
    public ObservableCollection<BO.ProductItem?> myListProductItem { get; set; }    

    public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.Category)); } }
    public BO.Cart TempCart = new BO.Cart();

    public MainCartViewPage()
    {
        TempCart.Items = new ObservableCollection<BO.OrderItem?>().ToList();
        myListProductItem = new ObservableCollection<BO.ProductItem?> (Bl.Product.GetListOfProductItem(TempCart));
        InitializeComponent();
    }

    public MainCartViewPage(BO.Cart cart)
    {
        TempCart = cart;
        myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl.Product.GetListOfProductItem(TempCart));
        InitializeComponent();

    }


    /// <summary>
    /// Filter by category.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductInfromation_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProductInfromation.SelectedItem.ToString() == BO.Enums.Category.NONE.ToString())
        {
            myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListOfProductItem(TempCart)!);
        }
        else
        {
            myListProductItem = new ObservableCollection<BO.ProductItem?>((Bl?.Product.GetListProductIGrouping(Bl?.Product.GetListOfProductItem(TempCart) ?? throw new NullReferenceException(), (BO.Enums.Category)ProductInfromation.SelectedItem) ?? throw new NullReferenceException()));   
        }
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
        Window.GetWindow(this).Close(); 
       
    }

    /// <summary>
    /// Open page of Order item in the Cart. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GoToCartOrderitem_Click(object sender, RoutedEventArgs e)
    {
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


            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            Bl?.Cart.AddProdctToCatrt(TempCart, id);
            myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListOfProductItem(TempCart)!);
            OnPropertyChanged(nameof(myListProductItem));

        }
        else
            MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);
        OnPropertyChanged(nameof(myListProductItem));

    }

    /// <summary>
    /// Remove product from cart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="BO.NotExistException"></exception>
    private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
    {
        if (OrderItemListView.SelectedItem != null)
        {
            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            TempCart.Items!.Remove(TempCart.Items!.FirstOrDefault(x => x?.ProductID == id) ?? throw new BO.NotExistException());
            myListProductItem = new ObservableCollection<BO.ProductItem?>(Bl?.Product.GetListOfProductItem(TempCart)!);
            OnPropertyChanged(nameof(myListProductItem));
        }
        else
            MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// Show details of product.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewDetalisProduct(object sender, MouseButtonEventArgs e)
    {
        if (OrderItemListView.SelectedItem != null)
        {
            int id = ((BO.ProductItem)OrderItemListView.SelectedItem).ID;
            new PlProduct.AddAndUpdate(id).Show();
            Window.GetWindow(this).Close();
        }
        else
            MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
