
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
using System.Windows.Shapes;
using PL;
using PL.PagesManager;
using PL.PlCart;

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {

        private static readonly Random R = new Random(); // Random number generation
        private BlApi.IBl? Bl = BlApi.Factory.Get();
        public Array Categories { get { return Enum.GetValues(typeof(BO.Enums.Category)); } }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private BO.Product _product;
        public BO.Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }
        public string Str { get; set; }
       

        /// <summary>
        /// Constructor for adding product.
        /// </summary>
        public AddAndUpdate()
        {
            Product = new();
            Product.Category = BO.Enums.Category.NONE;
            Str = "Add";
            InitializeComponent(); 
        }
        /// <summary>
        /// Constructor gor Updateing product.
        /// </summary>
        /// <param name="id"></param>
        public AddAndUpdate(int id)
        {
            Product = Bl.Product.GetProduct(id);
            Str = "Update";
            InitializeComponent();
            IdBox.IsReadOnly = true; // blocks the possibility to change the ID.
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
                
                if (Product.ID.ToString() == "")
                    throw new BO.NameIsEmptyException("Field ID is empty !");
                if (ProductNameBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field name is empty !");
                if (ProductPriceBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field price is empty !");
                if (ProductInstokeBox.Text.ToString() == "")
                    throw new BO.NameIsEmptyException("Field in stock is empty !");
                if (CategoryComboBox.Text == "")
                    throw new BO.NameIsEmptyException("Field in Category is empty !");
                if(CategoryComboBox.SelectedIndex == 5)
                    throw new BO.NameIsEmptyException("Require field Category !");


                try
                {
                    BO.Product p = new BO.Product
                    {
                        ID = Product.ID,
                        Category = Product.Category,
                        Name = Product.Name,
                        Price = Product.Price,
                        InStock = Product.InStock,
                    };
                    
                    if (Str == "Add")
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
                        if (Str == "Update")
                        {
                            new AddAndUpdate(Product.ID).Show();
                            flag = false;
                            this.Close();
                        }
                        else if (Str == "Add")
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
                new AdminView().Show();
                this.Close();
            }
         
        }

        private void BackLastWindow(object sender, RoutedEventArgs e)
        {
            new AdminView().Show();
           
            this.Close();
        }

    }
}
