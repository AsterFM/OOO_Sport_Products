﻿using OOO_Sport_Products.Classes;
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

namespace OOO_Sport_Products.View
{
    /// <summary>
    /// Логика взаимодействия для WindowsCatalog.xaml
    /// </summary>
    public partial class WindowsCatalog : Window
    {
        public WindowsCatalog()
        {
            InitializeComponent();
        }
        //Возврат на авторизацию
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Загрузка окна - отображение товаров

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Product> listProducts = new List<Model.Product>();
            listProducts = Helper.DB.Products.ToList();
            listBoxProducts.ItemsSource = listProducts;  
        }
    }
}
