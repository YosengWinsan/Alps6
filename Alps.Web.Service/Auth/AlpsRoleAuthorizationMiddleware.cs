using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Alps.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Auth
{

    public class AlpsRoleAuthorizationMiddleware
    {
        //private readonly AlpsContext _dbContext;
        private readonly RequestDelegate _next;
        public AlpsRoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
            //_dbContext=dbContext;

        }
        public async Task Invoke(HttpContext context, AlpsContext dbContext)
        {
            var endpoint = context.GetEndpoint();
            var authorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Array.Empty<IAuthorizeData>();
            // 如果没有 [Authorize] 就不需要拦截
            if (authorizeData == null || authorizeData.Count == 0)
            {
                await _next(context);
                return;
            }

            // 如果有 [AllowAnonymous]，那也不需要拦截
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }
            if (context.User.IsInRole("Admin"))
            {
                await _next(context);
                return;
            }
            var controllerName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
            var actionName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;
            var userName = context.User.Identity.Name;


            var query = await (
                dbContext.AlpsUsers.Include(p => p.RoleUsers).ThenInclude(p => p.Role).ThenInclude(p => p.Permissions).ThenInclude(p => p.Resource)
                .AnyAsync(p => p.RoleUsers.Any(l => l.Role.Permissions.Any(k => k.Resource.Controller == controllerName && k.Resource.Action == actionName))));
            /*
        from u in dbContext.AlpsUsers.Include(p=>p.RoleUsers).ThenInclude(p=>p.Role).ThenInclude(p=>p.Permissions).ThenInclude(p=>p.Resource)

        from r in dbContext.AlpsResources
        where r.Permissions.
       // from role in dbContext.AlpsRoles
        where u.IDName == userName && u.RoleUsers. &&
        (from r in dbContext.AlpsResources
         join p in dbContext.Permissions on r.ID equals p.ResourceID
         where r.Controller == controllerName && r.Action == actionName
         select p.RoleID).Contains(role.ID)
        select u.ID).CountAsync();
*/
            if (!query)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = 403;
                    context.Response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues("Login authorization failed"));
                    return;
                }
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues("Login authentication failed"));
                    return;
                }
            }

            await _next(context);
            return;
        }
    }
    /*
    public async Task Invoke(HttpContext context, AlpsContext _dbContext)
    {

        if (!IsProtectedAction(context))
            return;

        if (!IsUserAuthenticated(context))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (context.HttpContext.User.IsInRole("Admin"))
            return;
        //var actionId = GetActionId(context);
        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
        var controller = controllerActionDescriptor.ControllerName;
        var action = controllerActionDescriptor.ActionName;

        var userName = context.HttpContext.User.Identity.Name;
        var query = await (
        from u in _dbContext.AlpsUsers
        from role in _dbContext.AlpsRoles
        where u.IDName == userName && u.Roles.Contains(role) &&
        (from r in _dbContext.AlpsResources
         join p in _dbContext.Permissions on r.ID equals p.ResourceID
         where r.Controller == controller && r.Action == action
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

    private bool IsProtectedAction(HttpContext context)
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


}*/

}