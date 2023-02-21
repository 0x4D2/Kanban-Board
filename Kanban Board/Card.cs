using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;

namespace Kanban_Board
{
  
    public class Card
    {
        public int cardId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string modifier { get; set; }
        public string activity { get; set; }
        public DateTime rowversion { get; set; }
        public DateTime createTimestamp { get; set; }
        public DateTime lastModifikationTimestamp { get; set; }
        public Boolean isArchived { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public override string ToString()
        {
            return title;
        }


    }
}
