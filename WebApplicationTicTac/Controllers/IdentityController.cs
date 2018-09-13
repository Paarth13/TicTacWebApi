using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTicTac.Attributes;
using WebApplicationTicTac.Databases;
using WebApplicationTicTac.Models;

namespace WebApplicationTicTac.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    
    [ExceptionLogger]
    [Logger]
    public class IdentityController : Controller
    {
        //Muddy Waters
       
        [HttpGet]
        
        public List<Users> GetRegisteredUsers()
        {
            IRepository repo = new SqlDatabase();
            List<Users> users = new List<Users>();
            users=repo.GetAll();
            return users;
        }

        [HttpPost]
        public void PostCreateUsers([FromBody]Users user)
        {
            IRepository repo = new SqlDatabase();
            AccessOperations ops = new AccessOperations();
            user=ops.TokenGenerate(user);
            repo.AddUser(user);
            

        }
    }
}