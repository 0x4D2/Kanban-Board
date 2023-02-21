using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kanban_Board
{
    public class CardRepo
    {
        public bool rowversionExists(int cardId, string rowversion)
        {
            try{

                bool result = false;

                string checkRow =
                                @"SELECT CASE WHEN EXISTS (
                                SELECT *
                                FROM cards 
                                WHERE Card_ID =  @cardId
                                AND DATE_FORMAT(ROWVERSION, '%d.%m.%Y %H:%i:%s') = @rowversion
                                )
                                THEN 1 
                                ELSE 0  END as ROW";

                MySqlCommand query = new MySqlCommand(checkRow, DbConnections.Con);
                query.Parameters.Add(new MySqlParameter("@cardId", cardId));
                query.Parameters.Add(new MySqlParameter("@rowversion", rowversion));
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = Convert.ToBoolean(reader["ROW"]);
                    }
                }
                else
                {
                    result = false;
                }

                return result;

            }
            catch(Exception)
            {
                return false;
            }

        }
        public ResultObject UpdateCard(Card card, int newStatusID, bool isArchived) { 

            ResultObject result = new ResultObject();

            try
            {
               
                string updateQuery = "Update Cards Set IsArchived=@isArchived, Status_ID = @Status_ID Where Card_Id = @Card_Id";
                MySqlCommand query = new MySqlCommand(updateQuery, DbConnections.Con);
                query.Parameters.Add(new MySqlParameter("@Status_ID", newStatusID));
                query.Parameters.Add(new MySqlParameter("@Card_Id", card.cardId));
                query.Parameters.Add(new MySqlParameter("@isArchived", isArchived));
                query.ExecuteNonQuery();

                card.statusID = newStatusID; 
                
                if(newStatusID == 1)
                {
                    card.status = "TO DO";

                }else if(newStatusID == 2)
                {
                    card.status = "IN PROGRESS";

                }
                else if(card.statusID == 3)
                {
                    card.status = "TESTBAR";
                }
                else
                {
                    card.status = "DONE";
                }
         

                result.isSuccess = true;
                result.data = card;
                result.message = $"Card with ID: {card.cardId} Updated to {newStatusID}";

                return result;
            

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;
                result.message = $"Card with ID: {card.cardId} not Updated";
                return result;
            }
            
         
        }
        public bool UpdateCardPosition(Card card)
        {
            try
            {
                
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public ResultObject CreateCard(Card newCard)
        {
            

            ResultObject result = new ResultObject();
            try
            {
                String createQuery = @"INSERT INTO CARDS(Title, Description, Modifier, Activity, Rowversion, Create_Timestamp, Last_Modifikation_Timestamp, isArchived, Status_ID)
                                     VALUES(
                                     @Title,
                                     @Description,
                                     @Modifier,
                                     @Activity,
                                     @Rowversion,
                                     @Create_Timestamp,
                                     @Last_Modifikation_Timestamp,
                                     @isArchived,
                                     @Status_Id)";

                MySqlCommand query = new MySqlCommand(createQuery, DbConnections.Con);
                query.Parameters.Add(new MySqlParameter("@Title", newCard.title));
                query.Parameters.Add(new MySqlParameter("@Description", newCard.description));
                query.Parameters.Add(new MySqlParameter("@Modifier", newCard.modifier));
                query.Parameters.Add(new MySqlParameter("@Activity", newCard.activity));
                query.Parameters.Add(new MySqlParameter("@Rowversion", newCard.rowversion));
                query.Parameters.Add(new MySqlParameter("@Create_Timestamp", newCard.createTimestamp));
                query.Parameters.Add(new MySqlParameter("@last_Modifikation_Timestamp", newCard.lastModifikationTimestamp));
                query.Parameters.Add(new MySqlParameter("@isArchived", newCard.isArchived));
                query.Parameters.Add(new MySqlParameter("@Status_Id", newCard.statusID));

                query.ExecuteNonQuery();
                result.isSuccess = true;
                result.message = "Successfull insert into DB-Kanban";

                return result;
                
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;
                return result;
            }

        }

        public ResultObject GetCards()
        {
            ResultObject result = new ResultObject();

            
            try
            {
                List<Card> cardList = new List<Card>();

                string selectQuery = @"SELECT c.Card_ID, c.Title, c.Description, c.Modifier, c.Activity, c.Rowversion, 
                                    c.Create_Timestamp, c.Last_Modifikation_Timestamp, c.isArchived, c.Status_ID, s.status
                                    FROM cards c
                                    LEFT JOIN status s
                                    ON c.Status_ID = s.Status_ID
                                    Where c.isArchived = false
                                    ORDER BY c.Card_ID";

                MySqlCommand query = new MySqlCommand(selectQuery,DbConnections.Con);
                MySqlDataReader reader = query.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Card card = new Card();

                        card.cardId = int.Parse(reader["Card_ID"].ToString());
                        card.title = reader["Title"].ToString();
                        card.description = reader["Description"].ToString();
                        card.modifier = reader["Modifier"].ToString();
                        card.activity = reader["Activity"].ToString();
                        card.rowversion = DateTime.Parse(reader["Rowversion"].ToString());
                        card.createTimestamp = DateTime.Parse(reader["Create_Timestamp"].ToString());
                        card.lastModifikationTimestamp = DateTime.Parse(reader["Last_Modifikation_Timestamp"].ToString());
                        card.isArchived = Boolean.Parse(reader["isArchived"].ToString());
                        card.statusID = int.Parse(reader["Status_Id"].ToString());
                        card.status = reader["Status"].ToString();

                        cardList.Add(card);
                    }

                    reader.Close();
                    result.isSuccess = true;
                    result.data = cardList;
                    result.message = "Fully read";
                    result.exceptionMessage = null;

                    return result;
                }
                else
                {
                    reader.Close();
                    result.isSuccess=false;
                    result.data = cardList;
                    result.message = "Kein Tickets gefunden";
                    result.exceptionMessage = null;

                    return result;
                }

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.message = "No Connection to DB.";
                result.exceptionMessage = ex.Message;

                return result;

            }

        }
    }
}
