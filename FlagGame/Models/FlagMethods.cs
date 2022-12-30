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
            var os = Environment.OSVersion;
            var temp = os.Platform.ToString();

            if (temp == "Unix")
            {
                dbConnection.ConnectionString = "Data Source=localhost,1433;Database=FlagGame;User Id=sa;Password=NoccoFocus1";
            }

            else
            {
                dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FlagGame;Integrated Security=True";
            }

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

        public string formatFlagName(string name)
        {
            string formattedName = "";
            if (name.Contains("_"))
            {
                string[] split = name.Split("_");
                if (split.Length > 1)
                {
                    for (int i = 0; i < split.Length; i++)
                    {
                        split[i] = char.ToUpper(split[i][0]) + split[i].Substring(1);
                        if (split[i].Equals("And"))
                        {
                            split[i] = "&";
                        }
                        formattedName = formattedName + split[i] + " ";
                    }
                }
            }
            else
            {
                formattedName = char.ToUpper(name[0]) + name.Substring(1);
            }
            return formattedName;
        }

        public List<String> shuffleStringList(List<String> list)
        {
            Random rand = new Random();

            for (int i = 0; i < list.Count; i++)
            {
                int j = rand.Next(i, list.Count);
                string temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            return list;
        }
    }
}
