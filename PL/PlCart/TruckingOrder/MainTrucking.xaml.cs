using PL.PagesManager;
using PL.PlProduct;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.PlCart.TruckingOrder;

/// <summary>
/// Interaction logic for MainTrucking.xaml
/// </summary>
public partial class MainTrucking : Page, INotifyPropertyChanged
{
    private BlApi.IBl? Bl = BlApi.Factory.Get();
    public BO.Order order = new BO.Order();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string _strResult;
    public string strResult
    {
        get { return _strResult; }
        set 
        {
            _strResult = value;
            OnPropertyChanged(nameof(strResult));
        }
    }

    public MainTrucking()
    {
        strResult = "";
        InitializeComponent();
    }

    private void Trucking_Click(object sender, RoutedEventArgs e)
    {
        int IDTrucking = int.Parse(truck.Text);
        try
        {
            BO.Order? a = new BO.Order();
            a = Bl?.Order.GetOrder(IDTrucking);
            strResult = $" {a?.ID} \n {a?.OrderDate} \n {a?.Status}";
            truckingOrder.Visibility = Visibility.Hidden;
            ViewOrderItemButton.Visibility = Visibility.Visible;
            order = Bl?.Order.GetOrder(IDTrucking);
        }
        catch (BO.NotExistException)
        {
            strResult = "ID not found ! \n   please try again.";
        }
    }

    private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Window.GetWindow(this).Close();  
    }

    private void ViewOrderItem_Click(object sender, RoutedEventArgs e)
    {
        truckingFram.Content = new OrderItemView(order.ID);
        ViewOrderItemButton.Visibility = Visibility.Hidden;
    }

    private void myFrame_ContentRendered(object sender, EventArgs e)
    {
        truckingFram.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
    }
}
