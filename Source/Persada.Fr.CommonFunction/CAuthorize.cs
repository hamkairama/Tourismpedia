using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace Persada.Fr.CommonFunction
{
   public class CAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isValid = false;
            if (CurrentUser.IsSa())
            {
                isValid = true;
            }
            return isValid;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
    }
}
