
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for ConfrimOrder.xaml
    /// </summary>
    public partial class ConfrimOrder : Page
    {
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public BO.Cart temp = new BO.Cart();
        public ConfrimOrder(BO.Cart c)
        {
            InitializeComponent();
            temp = c;      
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
                new MainCartView().Show();
                Window.GetWindow(this).Close();
            }
            else
                Window.GetWindow(this).Content = new CartOrderItem(temp);

        }

        /// <summary>
        /// Add the ditalis to cart. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndOfOrder_Click(object sender, RoutedEventArgs e)
        {

            if (temp.Items!.Count == 0)
            {
                ErrorEx.Text = "No items in order!";
                ErrorEx.Visibility = Visibility.Visible;
            }

            else
            {
                try
                {
                    if (NameTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Name is empty !");

                    if (EmailTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Email is empty !");

                    if (AddressTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Address is empty !");

                    string? nameClaint = NameTextBox.Text.ToString();
                    string? emailClaint = EmailTextBox.Text.ToString();
                    string? addressClaint = AddressTextBox.Text.ToString();

                    try
                    {
                        Bl?.Cart.ConfirmOrder(temp, nameClaint, emailClaint, addressClaint);
                        ErrorEx.Text = "Order confrim :)";
                        ErrorEx.Visibility = Visibility.Visible;

                    }
                    catch (BO.EmailInValidException) { ErrorEx.Text = "Invalid email !"; }

                }
                catch (BO.NameIsEmptyException ex)
                {
                    ErrorEx.Text = $"{ex.Message}";
                    ErrorEx.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
