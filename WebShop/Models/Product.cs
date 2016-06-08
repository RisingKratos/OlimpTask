using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebShop.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        [NotMapped]
        public string StatusText { get { return Status ? "Pending" : "Sold"; } }
        public bool Delivery { get; set; }
        [NotMapped]
        public string DeliveryText { get { return Delivery ? "Able to deliver" : "Should bring yourself"; } }
        public string Image { get; set; }

        public virtual Category Category { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}