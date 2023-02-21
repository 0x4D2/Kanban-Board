using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban_Board
{
    public class Datahandler
    {
        public static ResultObject PrepareCard(Card card)
        {
            ResultObject result = new ResultObject();

            try
            {
                if (!string.IsNullOrEmpty(card.title))
                {
                    if (card.title.Length > 500)
                    {
                        card.title = card.title.Substring(0, 500);
                    }
                }
                else
                {
                    card.title = "";
                }

                if (!string.IsNullOrEmpty(card.description))
                {
                    if (card.description.Length > 500)
                    {
                        card.description = card.description.Substring(0, 500);
                    }
                }
                else
                {
                    card.description = "";
                }

                if (!string.IsNullOrEmpty(card.activity))
                {
                    if (card.activity.Length > 500)
                    {
                        card.activity = card.activity.Substring(0, 500);
                    }
                }
                else
                {
                    card.activity = "";
                }

                card.modifier = "System";
                card.createTimestamp = Convert.ToDateTime(string.Format("{0:s}", DateTime.Now));
                card.lastModifikationTimestamp = Convert.ToDateTime(string.Format("{0:s}", DateTime.Now));
                card.rowversion = Convert.ToDateTime(string.Format("{0:s}", DateTime.Now));
                card.isArchived = false;
                card.statusID = 1;
                card.status = "To Do";

                result.isSuccess = true;
                result.data = card;
                result.message = "Card prepared";

                return result;

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;

                return result;
            }
        }

    }
}
