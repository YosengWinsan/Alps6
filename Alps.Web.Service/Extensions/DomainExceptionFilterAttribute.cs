using Alps.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Extensions
{
    public class DomainExceptionFilterAttribute: ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      if(context.Exception.GetType()==typeof(DomainException))
      {
        context.Result = new Microsoft.AspNetCore.Mvc.OkObjectResult(new Alps.Web.Service.Model.AlpsActionResponse(Model.AlpsActionResultCode.Warning).AddMessage(context.Exception.Message));
        context.Exception = null;
      }
      base.OnException(context);
    }
  }
}
