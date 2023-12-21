using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOO_Sport_Products.Classes
{
    public class OrderExtended
    {
        //Данные о заказе
        public Model.Order Order { get; set; }
        //Сумаа всего заказа
        public double SumOrder { get; set; }
        //Сумма скидки всего заказа
        public double SumOrderWithDiscount { get; set; }
        //Общая скидка в процентах
        public double SumDiscountPercent 
        {
            get 
            {
                return 100 - (SumOrderWithDiscount * 100 / SumOrder);
            }
        }
        //Метод расчёта общей суммы заказа
        public double CalcSumOrder(List<Model.OrderProduct> orderProducts) 
        {
            double sum = 0;
            foreach (Model.OrderProduct product in orderProducts) 
            {
                if (product.OrderId == Order.OrederId) 
                {
                    double temp = product.Product.ProductCost;
                    sum += temp * product.ProductCountInOrder;
                }
            }
            return sum;
        }

        public double CalcSumOrderWithDiscount(List<Model.OrderProduct> orderProducts) 
        {
            double sum = 0;
            foreach (Model.OrderProduct product in orderProducts)
            {
                if (product.OrderId == Order.OrederId)
                {
                    double temp = product.Product.ProductCost - product.Product.ProductCost * product.Product.ProductDiscountCurrent / 100;
                    sum += temp * product.ProductCountInOrder;
                }
            }
            return sum;
        }

    }
}
