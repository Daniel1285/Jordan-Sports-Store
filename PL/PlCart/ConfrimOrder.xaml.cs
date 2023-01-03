using BO;
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
        public BO.Cart Cart = new BO.Cart();
        public ConfrimOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the ditalis to cart. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndOfOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Name is empty !");
                    
                if (EmailTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Email is empty !");

                if (AddressTextBox.Text.ToString() == "") throw new BO.NameIsEmptyException("Field Address is empty !");

                string? nameClaint = NameTextBox.Text.ToString();
                string? emailClaint = EmailTextBox.Text.ToString();
                string? addressClaint = AddressTextBox.Text.ToString();

                //try
                //{
                //    Bl?.Cart.ConfirmOrder(Cart, nameClaint, emailClaint, addressClaint);

                //}
                //catch (EmailInValidException) {
                //    HintAssist.SetHint(NameTextBox, "dskdmsdsd");
                //    MaterialDesignThemes.Wpf.HintAssist.SetBackground(NameTextBox, Brushes.Red);
                //}

            }
            catch (NameIsEmptyException ex)
            {
                ErrorEx.Text = $"{ex.Message}";
                ErrorEx.Visibility= Visibility.Visible;   
            }





        }
    }
}
