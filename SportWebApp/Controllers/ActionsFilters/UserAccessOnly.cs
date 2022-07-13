using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data.Repositories;

namespace SportWebApp.Controllers.ActionsFilters
{
    /// <summary>
    /// Action Filters
    /// </summary>
    public class UserAccessOnly : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private EventRepository _event = new EventRepository();

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            if (context.RouteData.Values.ContainsKey("id"))
            {
                int id = int.Parse((string)context.RouteData.Values["id"]);
                if (context.HttpContext.User != null)
                {
                    var username = context.HttpContext.User.Identity.Name;
                    if (username != null)
                    {
                        var myevent =  _event.GetEvent(id);
                        if (myevent.User != null)
                        {
                            if (myevent.User.UserName != username)
                            {
                                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Calendar", action = "NotFound" }));
                            }
                        }
                    }

                }
            }
        }
    }
}
