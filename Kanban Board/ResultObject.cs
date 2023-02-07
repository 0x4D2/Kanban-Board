using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban_Board
{
    public  class ResultObject
    {
        public bool isSuccess { get; set; } 
        public string message { get; set; }
        public object data { get; set; }
        public string exceptionMessage { get; set; }

       
    }
}
