
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using PL.PagesManager;
namespace PL.PlProduct;

/// <summary>
/// Interaction logic for ListProduct.xaml
/// </summary>
public partial class AdminView : Window
{


    public AdminView()
    {
        InitializeComponent();
    }

    private void BackToLastWindowButton_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }

    private void GotoOrdersManager_Click(object sender, RoutedEventArgs e)
    {
        MainList.Content = new OrdersPage();

        ordersCard.Visibility = Visibility.Hidden;
        productCard.Visibility = Visibility.Hidden;
        NewProduct.Visibility = Visibility.Hidden;
    }

    private void GotoProductManager_Click(object sender, RoutedEventArgs e)
    {
        MainList.Content = new ProductsPage();
        NewProduct.Visibility = Visibility.Visible;
        productCard.Visibility = Visibility.Hidden;
        ordersCard.Visibility = Visibility.Hidden;
    }

    private void myFrame_ContentRendered(object sender, EventArgs e)
    {
        MainList.NavigationUIVisibility = NavigationUIVisibility.Hidden;
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        new AddAndUpdate().Show();

        (Window.GetWindow(this)).Close();
    }

}
