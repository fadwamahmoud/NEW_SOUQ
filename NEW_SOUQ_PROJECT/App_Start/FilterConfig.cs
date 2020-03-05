using System.Web;
using System.Web.Mvc;

namespace NEW_SOUQ_PROJECT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
