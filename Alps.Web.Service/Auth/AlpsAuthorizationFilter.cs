using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Alps.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Auth
{
    public class AlpsAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly AlpsContext _dbContext;

        public AlpsAuthorizationFilter(AlpsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!IsProtectedAction(context))
                return;

            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //var actionId = GetActionId(context);
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;

            var userName = context.HttpContext.User.Identity.Name;
            var query = await (
            from u in _dbContext.AlpsUsers
            from role in _dbContext.AlpsRoles
            where u.IDName == userName && u.Roles.Contains(role)  && 
            (from r in _dbContext.AlpsResources
                join p in _dbContext.Permissions on r.ID equals p.ResourceID
                where r.Controller==controller && r.Action == action 
                select p.RoleID).Contains(role.ID)
            select u.ID).CountAsync();

            if (query > 0)
                return;
            // foreach (var role in roles)
            // {
            //     if(role.Access == null)
            //         continue;

            //     var accessList = JsonConvert.DeserializeObject<IEnumerable<MvcControllerInfo>>(role.Access);
            //     if (accessList.SelectMany(c => c.Actions).Any(a => a.Id == actionId))
            //         return;
            // }
            context.Result = new ForbidResult();
        }

        private bool IsProtectedAction(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            return false;
        }

        private bool IsUserAuthenticated(AuthorizationFilterContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }

        private string GetActionId(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;

            return $"{area}:{controller}:{action}";
        }


    }

}