using System.Web;
using System.Web.Mvc;

namespace ASP_Georgi_Minkov
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
