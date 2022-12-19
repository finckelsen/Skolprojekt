using System;
using System.Data;
using System.Data.SqlClient;

namespace FlagGame.Models
{
    public class FlagMethods
    {
        public FlagMethods() { }

        public List<FlagDetails> SelectFlags(int difficulty, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FlagGame;Integrated Security=True";

            string sqlString = $"SELECT * FROM Table_Flags WHERE Flag_Difficulty = {difficulty}";
            //string sqlString = $"SELECT * FROM Table_Flags";

            SqlCommand cmd = new SqlCommand(sqlString, dbConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            List<FlagDetails> flagList = new List<FlagDetails>();

            try
            {
                dbConnection.Open();
                adapter.Fill(dataSet, "flagDetails");
                int count = 0;
                int i = 0;
                count = dataSet.Tables["flagDetails"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        FlagDetails flag = new FlagDetails();
                        flag.name = dataSet.Tables["flagDetails"].Rows[i]["Flag_Name"].ToString();
                        flag.difficulty = Convert.ToInt16(dataSet.Tables["flagDetails"].Rows[i]["Flag_Difficulty"]);
                        flag.imagePath = dataSet.Tables["flagDetails"].Rows[i]["Flag_ImagePath"].ToString();

                        

                        i++;
                        flagList.Add(flag);
                    }
                    errormsg = "";
                    return flagList;
                }
                else
                {
                    errormsg = "No flags retrieved.";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
