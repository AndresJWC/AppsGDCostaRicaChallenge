using System.Web;
using System.Web.Mvc;

namespace Apps_GD_Costa_Rica___Challenge
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}