using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Sport_Products.Classes
{
    public class Helper
    {
        //Объявдление объект связи с БД
        public static Model.DBSportProducts DB { get; set; }
        //
        public static Model.User User { get; set; }
    }

    public class PruductExtended 
    {
        public Model.Product Product { get; set; }


    }
}
