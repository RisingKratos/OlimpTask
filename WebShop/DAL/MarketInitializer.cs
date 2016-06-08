using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.DAL
{
    public class MarketInitializer : System.Data.Entity.DropCreateDatabaseAlways<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            var users = new List<User> { 
                new User{FirstName="Ayaulym",LastName="Mukhametkali",Email="ai.muha@mail.ru",ContactInfo="Search in KBTU",Password="pass1", Role = 0, Login = "Poel", RePassword = "pass1",}, 
                new User{FirstName="Muslim",LastName="Beybituly",Email="risingkratos@gmail.com",ContactInfo="Search in KBTU too",Password="pass2", Role = 0, Login = "Lulz", RePassword = "pass2"},
                new User{FirstName="Admin", LastName="Admin",ContactInfo="Admin",Email="Admin",Password="qwerty",RePassword="qwerty", Login="Admin", Role = 1}
            };
            users.ToList().ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            var categories = new List<Category> { 
                new Category{Name="Food", CategoryID = 1},
                new Category{Name="Clothes", CategoryID = 2}, 
                new Category{Name="Footwear", CategoryID = 3}, 
                new Category{Name="Tableware", CategoryID = 4},
            };
            categories.ToList().ForEach(category => context.Categories.Add(category));
            context.SaveChanges();

            var products = new List<Product> { 
            //    new Product {Title="Adal", CreatedDate=DateTime.Parse("2016-05-24"),Delivery=true,Price=250,Status=false,Description="Milk", 
            //        Category = categories[0], Seller = users.First(), Image = 
            //    },
                new Product{Title="President", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=990, Status=false, Description="feta cheese in brine 250g", Image = "/Images/pres.jpg", Category = categories[0], Seller = users.First()}, 
                new Product{Title="Sausage Bekker and K", CreatedDate=DateTime.Parse("2016-05-26"), Delivery=true, Price=1052, Status=false, Description="horsehead semi-smoked sausage kg", Image = "/Images/saus.jpg", Category = categories[0], Seller = users[1]},
                new Product{Title="Yogurt Campina",CreatedDate=DateTime.Parse("2016-05-20"),Delivery=true,Price=200,Status=false, Description="drinking fruttis with watermelon juice 285g", Image = "/Images/yog.jpg", Category = categories[0], Seller = users[1]},
             new Product{Title="Cofee Jacobs Monarch ",CreatedDate=DateTime.Parse("2016-01-21"),Delivery=true,Price=2800,Status=false, Description="instant freeze-dries coffee", Image = "/Images/cof.jpg", Category = categories[0], Seller = users[1]},
            new Product{Title="Tomatoes", CreatedDate=DateTime.Parse("2016-05-26"), Delivery=true, Price=257, Status=false, Description="bullish heart kg", Image = "/Images/tom.jpg", Category = categories[0], Seller = users[1]},
               new Product{Title="Dress", CreatedDate=DateTime.Parse("2016-01-09"), Delivery=true, Price=6935, Status=false, Description="Floral print round neck sleeveless back lace up dress", Image = "/Images/dr1.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="T-Shirt", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=false, Price=5475, Status=false, Description="Letter pattern round collar short sleeve T-Shirt", Image = "/Images/dr2.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Blazer", CreatedDate=DateTime.Parse("2016-03-14"), Delivery=true, Price=8760, Status=false, Description="PU leather spliced long sleeve blazer", Image = "/Images/dr3.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Skirt", CreatedDate=DateTime.Parse("2016-07-27"), Delivery=true, Price=8030, Status=false, Description="Sweet floral print high waist A-Line Skirt", Image = "/Images/dr4.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Leggings", CreatedDate=DateTime.Parse("2016-03-12"), Delivery=false, Price=7300, Status=false, Description="Solid color elastic high waist leggings", Image = "/Images/dr5.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Nike", CreatedDate=DateTime.Parse("2016-04-15"), Delivery=true, Price=24500, Status=false, Description="NIKE FS LITE RUN 3", Image = "/Images/f1.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Incult", CreatedDate=DateTime.Parse("2016-03-09"), Delivery=true, Price=4500, Status=false, Description="Grey lifestyle shoes", Image = "/Images/f23.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Lee Cooper", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=false, Price=11300, Status=false, Description="Tan formal shoes", Image = "/Images/f3.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Call It Spring", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=9200, Status=false, Description="Silver stilettos", Image = "/Images/f4.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Carlton London", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=6900, Status=false, Description="Black ankle strap belly shoes", Image = "/Images/f5.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Mariefleur Sugar Bowl", CreatedDate=DateTime.Parse("2016-02-02"), Delivery=true, Price=4500, Status=false, Description="dishwasher safe, microwave safe", Image = "/Images/t1.jpg", Category = categories[3], Seller = users.First()}, 
               new Product{Title="Mariefleur Latte Mug", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=3100, Status=false, Description=" weight 0.35kg, content 0,48l", Image = "/Images/t2.jpg", Category = categories[3], Seller = users.First()}, 
               new Product{Title="Scotch Whisky", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=false, Price=2960, Status=false, Description="material: Crystal glass, content:0,47l", Image = "/Images/t3.jpg", Category = categories[3], Seller = users.First()}, 
               new Product{Title="For Me Oval Platter", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=1500, Status=false, Description="material: Premium Porcelain", Image = "/Images/t4.jpg", Category = categories[3], Seller = users.First()}, 
               new Product{Title="NWC Surgeon Fish Espresso Cup", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=1250, Status=false, Description="microwave safe, content:0,08l", Image = "/Images/t5.jpg", Category = categories[3], Seller = users.First()}, 
              new Product{Title="White Chocolate Layer Cake", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=true, Price=3450, Status=false, Description=" White chocolate cake with cream cheese frosting and pecans sprinkled on top", Image = "/Images/g1.jpg", Category = categories[0], Seller = users.First()}, 
               new Product{Title="Kymyz", CreatedDate=DateTime.Parse("2016-05-29"), Delivery=false, Price=525, Status=false, Description="1L KYMYZ ALTAY-SARZHAILAU", Image = "/Images/g2.jpg", Category = categories[0], Seller = users.First()}, 
              new Product{Title="French Garden Kitchen Tray", CreatedDate=DateTime.Parse("2016-03-02"), Delivery=true, Price=4380, Status=false, Description="material:	Melamine", Image = "/Images/g3.jpg", Category = categories[3], Seller = users.First()}, 
               new Product{Title="Floreana Red Coffee Cup", CreatedDate=DateTime.Parse("2016-02-20"), Delivery=false, Price=480, Status=false, Description="material: Premium Porcelain,content:0,23l", Image = "/Images/g4.jpg", Category = categories[3], Seller = users.First()}, 
                new Product{Title="Women's Body Blouse Shirt", CreatedDate=DateTime.Parse("2016-01-19"), Delivery=true, Price=4900, Status=false, Description="Hand wash seperately use mild detergent mild iron", Image = "/Images/g5.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Pants", CreatedDate=DateTime.Parse("2016-04-16"), Delivery=false, Price=5950, Status=false, Description="Printed Wide Leg Palazzo Pants", Image = "/Images/g6.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Flat shoes", CreatedDate=DateTime.Parse("2016-03-13"), Delivery=false, Price=2400, Status=false, Description="Hollow Out Flock Lace-Up Flat Shoes", Image = "/Images/g7.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Sandals", CreatedDate=DateTime.Parse("2016-04-05"), Delivery=false, Price=3200, Status=false, Description="Solid Color Weaving Lace-Up Sandals", Image = "/Images/g8.jpg", Category = categories[2], Seller = users.First()}, 
               new Product{Title="Sweater", CreatedDate=DateTime.Parse("2016-02-23"), Delivery=false, Price=7000, Status=false, Description="Argyle Jacquard Round Collar Sweater", Image = "/Images/g9.jpg", Category = categories[1], Seller = users.First()}, 
               new Product{Title="Jacket", CreatedDate=DateTime.Parse("2016-01-28"), Delivery=false, Price=11500, Status=false, Description="Embossed PU Leather Motocycle Jacket", Image = "/Images/g10.jpg", Category = categories[1], Seller = users.First()}, 
            };
            products.ToList().ForEach(product => context.Products.Add(product));
            context.SaveChanges();
        }
    }
}