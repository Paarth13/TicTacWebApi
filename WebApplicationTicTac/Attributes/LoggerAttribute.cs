using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTicTac.Databases;

namespace WebApplicationTicTac.Attributes
{
    public class LoggerAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var request = context.HttpContext.Request.Method;
            var response = "Success " + context.RouteData.Values["action"].ToString();
                     
            IRepository repo = new SqlDatabase();
          
                repo.Log(request, response,"NULL");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request.Method;
            var response = "In "+context.RouteData.Values["action"].ToString();
            string exception = "NULL";
            
            IRepository repo = new SqlDatabase();
            repo.Log(request, response,exception);
        }
    }
}
