using System.Web;
using System.Web.Mvc;

namespace Calendar_OneDrive_Mail_Graph
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
