
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

namespace PL.PlCart
{
    /// <summary>
    /// Interaction logic for ConfrimOrder.xaml
    /// </summary>
    public partial class ConfrimOrder : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        private BO.Cart _temp;
        public BO.Cart temp 
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged(nameof(temp));
            }
        }
        private string _str; 
        public string str
        {
            get { return _str; }
            set { _str = value; OnPropertyChanged(nameof(str)); }
        }
        public ConfrimOrder(BO.Cart c)
        {
            str = "";
            temp = c;
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
                Window.GetWindow(this).Content = new MainCartViewPage(temp);
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
                str = "No items in order!";
                
            }

            else
            {
                try
                {
                    if (temp.CustomerName == "") throw new BO.NameIsEmptyException("Field Name is empty !");

                    if (temp.CustomerEmail == "") throw new BO.NameIsEmptyException("Field Email is empty !");

                    if (temp.CustomerAddress == "") throw new BO.AddresIsempty("Field Address is empty !");

                    try
                    {
                        Bl?.Cart.ConfirmOrder(temp, temp.CustomerName, temp.CustomerEmail, temp.CustomerAddress);
                        str = "Order confrim :)";
                        temp = new BO.Cart();
                        temp.Items = new();


                    }
                    catch (BO.EmailInValidException) { str = "Invalid email !"; }

                }
                catch (BO.NameIsEmptyException ex)
                {
                    str = $"{ex.Message}";
                   
                }

                catch (BO.AddresIsempty ex)
                {
                    str = $"{ex.Message}";
                    
                }
            }
        }
    }
}
