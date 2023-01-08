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
/// Interaction logic for CartOrderItem.xaml
/// </summary>
public partial class CartOrderItem : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    

    /// <summary>
    /// Updates the list immediately.
    /// </summary>
    /// <param name="propertyName"></param>
    private void onPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    


    private BlApi.IBl? Bl = BlApi.Factory.Get();
    public ObservableCollection<BO.OrderItem?> myListOrderItem { get; set; }
    public IEnumerable<int> LongIntegerList => Enumerable.Range(1, 100).ToList();


    public BO.Cart temp = new BO.Cart();


    public CartOrderItem(BO.Cart c)
    {
        myListOrderItem = new ObservableCollection<BO.OrderItem?>(c.Items!);
        InitializeComponent();
        temp = c;
    }
    /// <summary>
    /// Transition to confrim order page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GoToConfrimOrder_Click(object sender, RoutedEventArgs e)
    {
        CartOrderItemFram.Content = new ConfrimOrder(temp);
    }


    /// <summary>
    /// Back to main cart view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackToMainCartView_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Content = new MainCartViewPage(temp);
    }


    /// <summary>
    /// Remove orderItem from cart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="BO.NotExistException"></exception>
    private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem removeOrderItem = new BO.OrderItem();
        int id = ((BO.OrderItem)OrdersListView.SelectedItem).ProductID;
        removeOrderItem = myListOrderItem.FirstOrDefault(x => x?.ProductID == id) ?? throw new BO.NotExistException();
        temp.Items!.Remove(removeOrderItem);
        onPropertyChanged(nameof(myListOrderItem)); 
    }

    /// <summary>
    /// Update amount of product in cart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateAmountInCart_Click(object sender, RoutedEventArgs e)
    {
        NumForUpdate.Visibility = Visibility.Visible;
    }

    private void UpdateAmunt_SelectedChanged(object sender, SelectionChangedEventArgs e)
    {
        int id = ((BO.OrderItem)OrdersListView.SelectedItem).ProductID;
        int newAmount = int.Parse(NumForUpdate.SelectedItem.ToString()!);
        Bl?.Cart.UpdateAmountOfProduct(temp, id, newAmount);
        onPropertyChanged(nameof(myListOrderItem));
    }
}
