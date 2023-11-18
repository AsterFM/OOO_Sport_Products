using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlTypes;
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
            //Заполнены скидки
            cbDiscount.Items.Clear();
            cbDiscount.Items.Add("Все диапозоны");
            cbDiscount.Items.Add("0 - 9,99%");
            cbDiscount.Items.Add("10 - 14,99%");
            cbDiscount.Items.Add("15% и более");
            cbDiscount.SelectedIndex = 0;
            //Заполние списка категорий
            List<Model.Cutegory> cutegories = new List<Model.Cutegory>();
            cutegories = Classes.Helper.DB.Cutegories.ToList();
            Model.Cutegory cutegory = new Cutegory();
            cutegory.CategoryId = 0;
            cutegory.CategoryName = "Все категории";
            cutegories.Insert(0, cutegory);
            cbCategory.ItemsSource = cutegories;
            cbCategory.DisplayMemberPath = "CategoryName";
            cbCategory.SelectedValuePath = "CategoryId";
            cbCategory.SelectedIndex = 0;
            rbSortAsc.IsChecked = true;
            ShowProducts();
            //
            //    List<Model.Product> listProducts = new List<Model.Product>();
            //    listProducts = Helper.DB.Products.ToList();
            //    //listBoxProducts.ItemsSource = listProducts;  
            //    List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            //    foreach (Model.Product product in listProducts) 
            //    {
            //        Classes.ProductExtended productExtended = new Classes.ProductExtended();
            //        productExtended.Product = product;
            //        productExtendeds.Add(productExtended);
            //    }

            //    listBoxProducts.ItemsSource = productExtendeds;
            //}


        }
        //Отображает инофрмацию обо всех товарах с фильтрацией
        private void ShowProducts()
        {
            List<Model.Product> listProducts = new List<Model.Product>();
            listProducts = Helper.DB.Products.ToList();
            //listBoxProducts.ItemsSource = listProducts;  
            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            int totalCount = listProducts.Count;
            foreach (Model.Product product in listProducts)
            {
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                productExtended.Product = product;
                productExtendeds.Add(productExtended);
            }
            //Сортировка
            if (rbSortAsc.IsChecked == true)
            {
                productExtendeds = productExtendeds.OrderBy(x => x.Product.ProductCost).ToList();
            }
            else 
            {
                productExtendeds = productExtendeds.OrderByDescending(x => x.Product.ProductCost).ToList();
            }
            double min = 0; //Все диапазоны скидок
            double max = 100;
            //Выбор диапазона скидки
            switch (cbDiscount.SelectedIndex) 
            {
                case 1:
                    min = 0;
                    max = 9.99;
                    break;
                case 2:
                    min = 10;
                    max = 14.99;
                    break;
                case 3:
                    min = 15;
                    max = 100;
                    break;
            }
            //Сортировка по скидке
            productExtendeds = productExtendeds.Where(x => x.Product.ProductDiscountMax >= min && x.Product.ProductDiscountMax <= max).ToList();
            if (cbCategory.SelectedValue != null) 
            {
                int idCat = (int)cbCategory.SelectedValue;
                if (idCat > 0) 
                {
                    productExtendeds = productExtendeds.Where(x => x.Product.ProductCutegoryId == idCat).ToList();
                }
            }
            //Поиск по названию
            string searchName = tbSearch.Text.ToUpper();
            if (!string.IsNullOrEmpty(searchName))
                productExtendeds = productExtendeds.Where(x => x.Product.ProductName.ToUpper().Contains(searchName)).ToList();
            int filterCount = productExtendeds.Count;
            tbCount.Text = filterCount.ToString() + " из " + totalCount.ToString();
            listBoxProducts.ItemsSource = productExtendeds;
        }
        //Сортировка по возростанию
        private void rbSortAsc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProducts();
        }
        //Сортировка по убыванию
        private void rbSortDesc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProducts();
        }
        //Сотрировка по скидке
        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProducts();
        }
        //Фильтрация по категории
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProducts();
        }
        //Ввод в строку поиска
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProducts();
        }
    }
}
