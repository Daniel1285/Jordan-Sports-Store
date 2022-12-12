﻿using BlApi;
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

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {

        private static readonly Random R = new Random(); // Random number generation
        private IBl Bl = new BlImplementation.Bl();

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrUpdateProductToList_Click(object sender, RoutedEventArgs e)
        {
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
                        Bl.Product.AddProduct(p);
                    else
                        Bl.Product.UpdateProduct(p);
                }
                catch (BO.AlreadyExistException) { MessageBox.Show("ID exists !"); }
                catch (BO.IdSmallThanZeroException) { MessageBox.Show("ID smalll than zero !"); }
                catch (BO.InStokeSmallThanZeroException) { MessageBox.Show("In stoke smalll than zero !"); }
                catch (BO.PriceSmallThanZeroException) { MessageBox.Show("Price smalll than zero !"); }
                
            }
                
            catch (BO.NameIsEmptyException ex ) {MessageBox.Show(ex.Message);}


            new ListProduct().Show();
            this.Close();
        }

        private void BackLastWindow(object sender, RoutedEventArgs e)
        {
            new ListProduct().Show();
            this.Close();
        }

        private void ProductIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
