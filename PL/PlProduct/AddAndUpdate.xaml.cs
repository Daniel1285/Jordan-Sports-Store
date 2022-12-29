
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
using System.Windows.Shapes;
using PL;

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {

        private static readonly Random R = new Random(); // Random number generation
        private BlApi.IBl? Bl = BlApi.Factory.Get();

        //((BO.ProductForList)ProductsListView.SelectedItem).ID;


        /// <summary>
        /// Constructor for adding product.
        /// </summary>
        public AddAndUpdate()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            AddOrUpdateProductButton.Content = "Add";
        }
        /// <summary>
        /// Constructor gor Updateing product.
        /// </summary>
        /// <param name="id"></param>
        public AddAndUpdate(int id)
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            IdBox.Text = id.ToString();
            CategoryComboBox.SelectedItem = Bl.Product.GetProduct(id).Category;
            ProductNameBox.Text = Bl.Product.GetProduct(id).Name;
            ProductPriceBox.Text = Bl.Product.GetProduct(id).Price.ToString();
            ProductInstokeBox.Text = Bl.Product.GetProduct(id).InStock.ToString();  

            IdBox.IsReadOnly = true; // blocks the possibility to change the ID.
            AddOrUpdateProductButton.Content = "Update";
        }

        /// <summary>
        /// add and update Product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrUpdateProductToList_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true; // for Checks if there is any exception 

            try
            {
                
                if (IdBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field ID is empty !");
                if (ProductNameBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field name is empty !");
                if (ProductPriceBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field price is empty !");
                if (ProductInstokeBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field in stock is empty !");
                if (CategoryComboBox.Text == "")
                    throw new BO.NameIsEmptyException("Field in Category is empty !");

                try
                {
                    BO.Product p = new BO.Product
                    {
                        ID = int.Parse(IdBox.Text),
                        Category = (BO.Enums.Category)CategoryComboBox.SelectedItem,
                        Name = ProductNameBox.Text.ToString(),
                        Price = double.Parse(ProductPriceBox.Text),
                        InStock = int.Parse(ProductInstokeBox.Text)
                    };
                    if (AddOrUpdateProductButton.Content.ToString() == "Add")
                        Bl?.Product.AddProduct(p);
                    else
                        Bl?.Product.UpdateProduct(p);
                }
                catch (BO.AlreadyExistException) { MessageBox.Show("ID exists !","EROOR"); }
                catch (BO.IdSmallThanZeroException) { MessageBox.Show("ID smalll than zero !", "EROOR"); }
                catch (BO.InStokeSmallThanZeroException) { MessageBox.Show("In stoke smalll than zero !", "EROOR"); }
                catch (BO.PriceSmallThanZeroException) { MessageBox.Show("Price smalll than zero !", "EROOR"); }
                
            }
                
            catch (BO.NameIsEmptyException ex ) 
            {
                // If there is an exception, a window will open with the option to try again or not.
                MessageBoxResult mbresult = MessageBox.Show(ex.Message + "\n do you want try again ?", "ERROR", MessageBoxButton.YesNo, MessageBoxImage.Error);
                
                switch (mbresult) // Checks the user's choice and acts accordingly.
                {
                    case MessageBoxResult.Yes:
                        if (AddOrUpdateProductButton.Content.ToString() == "Update")
                        {
                            new AddAndUpdate(int.Parse(IdBox.Text)).Show();
                            flag = false;
                            this.Close();
                        }
                        else if (AddOrUpdateProductButton.Content.ToString() == "Add")
                        {
                            new AddAndUpdate().Show();
                            flag = false;
                            this.Close();
                        }

                        else // if the choice is "No"
                            new AddAndUpdate().Show(); 
                        break;

                    default:
                        break;
                } 

            }
            /* If the flag is true then it will open the windows...
               And if it's a false, he will already open them by calling the window in catch */
            if (flag) 
            {
                new ListProduct().Show();
                this.Close();
            }
         
        }

        private void BackLastWindow(object sender, RoutedEventArgs e)
        {
            new ListProduct().Show();
           
            this.Close();
        }

        private void ProductIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ProductNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ProductInstokeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
