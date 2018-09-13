using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTicTac.Models
{
    public class Response
    {
        public int[] board { get; set; }
        public string status { get; set; }
    }
}
