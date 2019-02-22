using System.Web;
using System.Web.Mvc;

namespace IdleMachinery.Extensions.Framework.Stubs.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
