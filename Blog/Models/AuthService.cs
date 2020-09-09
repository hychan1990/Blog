using MyToolNetStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public static class AuthService
    {
        static ExternalJsonConfigReader configReader = new ExternalJsonConfigReader(@"C:\Config\config.json");
        public static bool AuthenticateAdmin(string cookieValue)
        {
            if (cookieValue == configReader.GetValue("BlogAdminCode"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
