using OOO_Sport_Products.Classes;
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
    /// Логика взаимодействия для WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        List<ProductInOrder> listOrder;
        //Конструктор по умолчанию
        public WindowOrder()
        {
            InitializeComponent();
        }
        ////Конструктор с параметром
        public WindowOrder(List<ProductInOrder> listOrder) 
        {
            InitializeComponent();
            //this.DataContext = this;
            this.listOrder = listOrder;
            List <Model.Point> listPoint = new List <Model.Point>();
            listPoint = Helper.DB.Points.ToList();
            cbPoint.ItemsSource = listPoint;
            cbPoint.DisplayMemberPath = "PointAdress";
            cbPoint.SelectedValuePath = "PointId";
            cbPoint.SelectedIndex = 0;
            ShowInfo();
        }

        private void ShowInfo() 
        {
            listBoxOrders.ItemsSource = null;
            listBoxOrders.ItemsSource = listOrder;
            double sumTotal = 0;
            double sumTotalWithDiscount = 0;
            foreach (var item in listOrder) 
            {
                sumTotal += item.ProductExtended.Product.ProductCost * item.CountProductInOrder;
                sumTotalWithDiscount += item.ProductExtended.ProductCostWithDisount * item.CountProductInOrder;
            }
            tbSumTotal.Text = "Сумма заказа: " + sumTotal.ToString();
            tbSumDiscount.Text = "Cумма скидки:" + Convert.ToString(sumTotal - sumTotalWithDiscount);
            tbSumTotalWithDiscount.Text = "Сумма со скидкой: " + sumTotalWithDiscount.ToString();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void butDeleteProductInOrder_Click(object sender, RoutedEventArgs e)
        {

            string art = (sender as Button).Tag as string;
            ProductInOrder selectProduct = listOrder.Find(pr => pr.ProductExtended.Product.ProductArticle == art);
            //ProductInOrder selectProduct = (sender as Button).DataContext as ProductInOrder;
            listOrder.Remove(selectProduct);
            ShowInfo();
        }
        //Сохранение заказа в БД
        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if (listOrder.Count <= 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }
            else 
            {
                //Создаём оьъект заказ
                Model.Order Order = new Model.Order();
                //Запполняем поля
                Order.OrderDate = DateTime.Now;
                Order.OrderDeliveryDate = Order.OrderDate.AddDays(6);
                Order.OrderFullName = tbFullName.Text;
                Order.OrderCode = new Random().Next(100, 1000);
                Order.OrderStatus = 1;
                Order.OrderPoint = (int)cbPoint.SelectedValue;
                Helper.DB.Orders.Add(Order);
                try
                {
                    Helper.DB.SaveChanges();
                    foreach (Classes.ProductInOrder item in listOrder) 
                    {
                        Model.OrderProduct orderProduct = new Model.OrderProduct();
                        orderProduct.OrderId = Order.OrederId;
                        orderProduct.ProductArticle = item.ProductExtended.Product.ProductArticle;
                        orderProduct.ProductCountInOrder = item.CountProductInOrder;
                        Helper.DB.OrderProducts.Add(orderProduct);
                        Helper.DB.SaveChanges();
                    }
                    MessageBox.Show("Заказ оформлен");
                    listOrder.Clear();
                    this.Close();
                }
                catch 
                {
                    MessageBox.Show("Произошёл сбой при сохранении");
                }
            }
        }
    }
}
