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

namespace PL.PlCart.TruckingOrder;

/// <summary>
/// Interaction logic for OrderDetails.xaml
/// </summary>
public partial class OrderDetails : Page, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChangd(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private BlApi.IBl? Bl = BlApi.Factory.Get();
    public BO.Order myOrder { get; set; }   


    public OrderDetails(BO.Order order)
    {     
        myOrder = order;    
        OnPropertyChangd(myOrder.ToString());

        InitializeComponent();
    }


    private void BackToChosenWindow_listBox(object sender, SelectionChangedEventArgs e)
    {
        int chosenWindow = BackToChosenWindow.SelectedIndex;
        if (chosenWindow == 0)
        {
            new MainWindow().Show();
            Window.GetWindow(this).Close();
        }
        else if (chosenWindow == 2)
        {
            Window.GetWindow(this).Content = new MainCartViewPage();
        }
        //else
        //    Window.GetWindow(this).Content = new CartOrderItem(temp);

    }
}
