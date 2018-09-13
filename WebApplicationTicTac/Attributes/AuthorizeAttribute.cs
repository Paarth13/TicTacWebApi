using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTicTac.Databases;
using WebApplicationTicTac.Models;

namespace WebApplicationTicTac.Attributes
{   
    
       public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
        [ExceptionLogger]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            IRepository repo = new SqlDatabase();
            string token = repo.GetByToken(apiKey);
            
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Invalid Key");
            }
            
        }
    }
}
