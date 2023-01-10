using PL.PlProduct;
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

namespace PL.PagesManager;

/// <summary>
/// Interaction logic for Orders.xaml
/// </summary>
public partial class OrdersPage : Page,INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChangd(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    private BlApi.IBl? Bl = BlApi.Factory.Get();
    private ObservableCollection<BO.OrderForList?> _myListOrders;
    public ObservableCollection<BO.OrderForList?> myListOrders
    {
        get {return _myListOrders;}
        set
        {
            _myListOrders = value;
            OnPropertyChangd(nameof(myListOrders));
        }
    }
 
    public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.OrderStatus)); } } 
    public OrdersPage()
    {
        myListOrders = new ObservableCollection<BO.OrderForList?>(Bl!.Order.GetOrderLists());
        InitializeComponent();  
    }

    /// <summary>
    /// Show order item in cart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void doubleClick_orderItem(object sender, MouseButtonEventArgs e)
    {
        if (OrdersListView.SelectedItem != null)
        {     
            int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
            MyOrderPage.Content = new OrderItemView(id);
        }
        else
            MessageBox.Show("Please chose only from the products", "EROOR", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// ComboBox option. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (OrderInformation.SelectedItem.ToString() == BO.Enums.OrderStatus.NONE.ToString())
        {
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl!.Order.GetOrderLists());
        }
        else
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString())!);
    }

    /// <summary>
    /// Update to sent.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateToSent_Click(object sender, RoutedEventArgs e)
    {
        int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
        Bl?.Order.ShippingUpdate(id);
        if (OrderInformation.SelectedItem.ToString() == BO.Enums.OrderStatus.NONE.ToString())
        {
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl!.Order.GetOrderLists());
        }
        else
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString())!);

    }

    /// <summary>
    /// Update order to Provided.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateToProvided_Click(object sender, RoutedEventArgs e)
    {
        int id = ((BO.OrderForList)OrdersListView.SelectedItem).ID;
        Bl?.Order.SupplyUpdateOrder(id);
        if (OrderInformation.SelectedItem.ToString() == BO.Enums.OrderStatus.NONE.ToString())
        {
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl!.Order.GetOrderLists());
        }
        else
            myListOrders = new ObservableCollection<BO.OrderForList?>(Bl?.Order.GetListByCondition(X => X?.Status.ToString() == OrderInformation.SelectedItem.ToString())!);
    }
}
