using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;

namespace OOO_Sport_Products.Classes
{
    public class ProductExtended
    {
        public Model.Product Product { get; set; }
        public string ProductPathPhoto
        {
            get
            {
                if (!File.Exists(Environment.CurrentDirectory + "/Images/" + Product.ProductPhoto))
                    return "/Resources/picture.png";
                else
                    return Environment.CurrentDirectory + "/Images/" + Product.ProductPhoto;
            }
        }
        public double ProductCostWithDisount
        {
            get
            {
                double discountAmount = Product.ProductCost * (Product.ProductDiscountCurrent / 100.0);
                return Product.ProductCost - discountAmount;
            }
        }

        public SolidColorBrush SolidColorBrush 
        {
            get 
            {
                SolidColorBrush sb = new SolidColorBrush();
                if (Product.ProductDiscountMax > 15)
                {
                    sb.Color = Colors.Brown;
                }
                else 
                {
                    sb.Color = Colors.White;
                }
                return sb;
            }
        
        }


    }
}
