using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
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

            //
            List<Model.Product> listProducts = new List<Model.Product>();
            listProducts = Helper.DB.Products.ToList();
            //listBoxProducts.ItemsSource = listProducts;  
            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            foreach (Model.Product product in listProducts) 
            {
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                productExtended.Product = product;
                productExtendeds.Add(productExtended);
            }

            listBoxProducts.ItemsSource = productExtendeds;
        }


    }
}
