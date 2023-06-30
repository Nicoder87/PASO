using Escrutinio.Database;
using Escrutinio.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Escrutinio.Filters
{
    public class AuthorizeRuleAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string rule = string.Empty;
        private bool ignore = false;
        private bool permisoComun = false;

        public AuthorizeRuleAttribute()
        {

        }

        public AuthorizeRuleAttribute(string rule)
        {
            this.rule = rule;
            permisoComun = false;
        }

        public AuthorizeRuleAttribute(bool ignore)
        {
            this.ignore = ignore;
            if (ignore == false)
            {
                permisoComun = true;
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            USUARIO usuario = new USUARIO();
            if (AccountHelper.GetCurrentUser() != null)
            {
                if (!ignore)
                {
                    usuario = AccountHelper.GetCurrentUser();

                    string ruleDefinition = string.Empty;

                    if (string.IsNullOrEmpty(rule))
                        ruleDefinition = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "." + filterContext.ActionDescriptor.ActionName;
                    else
                        ruleDefinition = this.rule;

                    var entityID = filterContext.RouteData.Values["id"];

                    if (!permisoComun)
                    {
                        if (!AccountHelper.IsAuthorized(ruleDefinition) ||
                            (entityID != null && usuario.ORID.ToString() != entityID.ToString()
                            && filterContext.ActionDescriptor.ActionName == "EditProfile"
                            && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Usuario"))
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                        }
                    }
                }

            }
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery }));
        }
    }
}