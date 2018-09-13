using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTicTac.Databases;

namespace WebApplicationTicTac.Attributes
{
    public class ExceptionLoggerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request.Method;
            var response = "Error Thrown " + context.RouteData.Values["action"].ToString(); 
                var exception = context.Exception.Message;
            IRepository repo = new SqlDatabase();
            repo.Log(request, response, exception);
            context.Result = new JsonResult(exception);
            
        }
    }
}
