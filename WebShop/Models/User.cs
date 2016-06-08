using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required, MaxLength(20), Index(IsUnique = true)]
        public string Login { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped, Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Password does not match")]
        public string RePassword { get; set; }
        public int Role { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string ContactInfo { get; set; }

        public virtual ICollection<Product> MyProducts { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
    }

    public class LoginUser
    {
        [Required]
        public string Login { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}