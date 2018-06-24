using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Alps.Web.Service.Model
{
  public enum AlpsActionResultCode
  {
    Ok = 1,
    Error = -1,
    Warning = 0
  }


  public class AlpsActionResponse
  {
    public AlpsActionResultCode ResultCode { get; set; }
    public ICollection<string> Messages { get; set; }
    public object Data { get; set; }
    public AlpsActionResponse(AlpsActionResultCode resultCode)
    {
      this.ResultCode = resultCode;
      this.Messages = new HashSet<string>();
    }
    public AlpsActionResponse AddMessage(string message)
    {
      if (message != string.Empty)
        this.Messages.Add(message);
      return this;
    }
  }
  public static class ControllerHelper
  {
    public static IActionResult AlpsActionOk(this ControllerBase controller,object data=null)
    {
      AlpsActionResponse response = new AlpsActionResponse(AlpsActionResultCode.Ok);
      response.Data = data;
      return controller.Ok(response);
    }
    public static IActionResult AlpsActionError(this ControllerBase controller, string message = "")
    {
      AlpsActionResponse response = new AlpsActionResponse(AlpsActionResultCode.Error);
      response.AddMessage(message);
      return controller.Ok(response);
    }
    public static IActionResult AlpsActionWarning(this ControllerBase controller, string message = "")
    {
      AlpsActionResponse response = new AlpsActionResponse(AlpsActionResultCode.Warning);
      response.AddMessage(message);
      return controller.Ok(response);
    }
  }

}
