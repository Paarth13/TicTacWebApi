using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplicationTicTac.Attributes;
using WebApplicationTicTac.Models;

namespace WebApplicationTicTac.Controllers
{
    [Produces("application/json")]
    [Route("api/Game")]
    [Authorize]
    [Logger]
    [ExceptionLogger]
    public class GameController : Controller
    {
        static int flag = 0;
        static string p1=null;
        static string p2 = null;
        static int count=0;
        static int[] board = new int[9];
        // GET: api/Game
        [HttpGet]
        public string GetStatus()
        {
            if((board[0]==board[1] && board[1]==board[2]|| board[0] == board[3] && board[3] == board[6] || board[0] == board[4] && board[4] == board[8] || board[2] == board[4] && board[6] == board[6] || board[3] == board[4] && board[4] == board[5] || board[6] == board[7] && board[7] == board[8]|| board[1] == board[4] && board[4] == board[7] || board[2] == board[5] && board[5] == board[8]) && board[0]==1)
            {
                count = 9;
                return "Player 1 wins";
            }
            if ((board[0] == board[1] && board[1] == board[2] || board[0] == board[3] && board[3] == board[6] || board[0] == board[4] && board[4] == board[8] || board[2] == board[4] && board[6] == board[6] || board[3] == board[4] && board[4] == board[5] || board[6] == board[7] && board[7] == board[8] || board[1] == board[4] && board[4] == board[7] || board[2] == board[5] && board[5] == board[8]) && board[0] == 2)
            {
                count = 9;
                return "Player 2 wins";
            }
            if (count == 9)
                return "DRAW";
            else
                return "In Progress";
        }

      
        
        
        
        // PUT: api/Game/5
        [HttpPut("{id}")]
        
        public Response Put(int id,[FromHeader] string apiKey)
        {
            //var apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
           if(count!=9)
            {
                if (p1 == null)
                {
                    p1 = apiKey;
                    flag = 0;

                }
                else if (p2 == null && p1 != apiKey)
                {
                    p2 = apiKey;
                    flag = 1;
                }
                if (apiKey != p1 && apiKey != p2)
                {
                    throw new Exception("Cannot add new player");
                }

                if (apiKey == p1 && board[id] == 0 && flag == 0)
                {
                    board[id] = 1;
                    count += 1;
                    flag = 1;
                }
                else if (apiKey == p2 && board[id] == 0 && flag == 1)
                {
                    board[id] = 2;
                    count += 1;
                    flag = 0;
                }
                else
                {
                    if (board[id] != 0)
                        throw new Exception("Already in place of that id");
                    else
                        throw new Exception("Same player cannot play twice");
                }
              
            }
            GetStatus();
            return new Response { board = board, status = GetStatus() };
        }
        
    }
}
