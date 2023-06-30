using Escrutinio.Database;
using System.Web;

namespace Escrutinio.Helpers
{
    public class AccountHelper
    {
        public static bool IsAuthorized(string definicion)
        {
            return true;
        }

        public static USUARIO GetCurrentUser()
        {
            return HttpContext.Current.Session["ApplicationUser"] as USUARIO;
        }

        public static void SetCurrentUser(USUARIO user)
        {
            if (user == null)
                HttpContext.Current.Session.RemoveAll();
            else
                HttpContext.Current.Session["ApplicationUser"] = user;
        }

        public static string GetCurrentUserName()
        {
            var user = GetCurrentUser();
            if (user != null)
                return user.USUARIO1;
            else
                return "";
        }

        public static string GetCurrentNyA()
        {
            var user = GetCurrentUser();
            if (user != null)
                return user.APELLIDO + ' ' + user.NOMBRE;
            else
                return "";
        }
    }
}
