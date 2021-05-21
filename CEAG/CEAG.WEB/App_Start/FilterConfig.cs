using CEAG.WEB.Helpers;
using System.Web;
using System.Web.Mvc;

namespace CEAG.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RevalidarUsuario());
        }
    }
}
