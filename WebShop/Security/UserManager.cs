using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DAL;
using WebShop.Models;

namespace WebShop.Security
{
    public class UserManager
    {
        private MarketContext db = new MarketContext();

        public bool Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == username && u.Password == password);
            if (user != null)
            {
                SessionContainer.UserID = user.ID.ToString();
                SessionContainer.Role = user.Role.ToString();
                return true;
            }
            return false;
        }

        public void LogOff()
        {
            SessionContainer.Remove("userid");
            SessionContainer.Remove("role");
        }

        public bool IsLogged
        {
            get { return !string.IsNullOrEmpty(SessionContainer.UserID); }
        }

        public bool Authorize(string allowedRoles)
        {
            if (!IsLogged) return false;
            if (!string.IsNullOrEmpty(allowedRoles.Trim()))
            {
                string[] roles = allowedRoles.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (roles.Count() > 0 && !roles.Contains(SessionContainer.Role)) return false;
            }
            return true;
        }

        public bool Register(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}