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
    /// Логика взаимодействия для WindowWorkOrder.xaml
    /// </summary>
    public partial class WindowWorkOrder : Window
    {
        List<Model.Order> listOrder = new List<Model.Order>();
        List<Model.OrderProduct> listOrderProduct = new List<Model.OrderProduct>();
        List<OrderExtended> listOrderExtended;
        public WindowWorkOrder()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShowOrders();
        }

        private void ShowOrders() 
        {
            listOrder = Helper.DB.Orders.ToList();
            listOrderProduct = Helper.DB.OrderProducts.ToList();
            listOrderExtended = new List<OrderExtended>();
            foreach (var order in listOrder) 
            {
                Classes.OrderExtended orderExtended = new Classes.OrderExtended();
                orderExtended.Order = order;
                orderExtended.SumOrder = orderExtended.CalcSumOrder(listOrderProduct);
                orderExtended.SumOrderWithDiscount = orderExtended.CalcSumOrderWithDiscount(listOrderProduct);
                listOrderExtended.Add(orderExtended);
            }
            lvOrders.ItemsSource = listOrderExtended;
            lvSelectOrder.ItemsSource = listOrderProduct;
        }
        //Выбор нужного заказа
        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
