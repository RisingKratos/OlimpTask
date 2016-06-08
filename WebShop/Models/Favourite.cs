using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Favourite
    {
        public int FavouriteID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}