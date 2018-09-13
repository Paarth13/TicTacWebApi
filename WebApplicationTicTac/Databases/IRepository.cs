using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTicTac.Models;

namespace WebApplicationTicTac.Databases
{
    interface IRepository
    {
        List<Users> GetAll();
        Users GetById(int id);
        string GetByToken(string token);
        void AddUser(Users user);
        void Log(string req,string responseCode,string Exception = null);
    }
}
