using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTicTac.Models;

namespace WebApplicationTicTac.Databases
{
    public class SqlDatabase : IRepository
    {
       
        private SqlConnection con;
        private void connection()
        {
            string constr = @"Data Source=TAVDESK026;Initial Catalog=UserDatabase;User Id=sa;Password=test123!@#";
            con = new SqlConnection(constr);

        }
        public List<Users> GetAll()
        {
            connection();
            List<Users> userList = new List<Users>();
            string query = "SELECT * FROM Users";
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                userList.Add(
                    new Users
                    {
                        
                        Name = Convert.ToString(dr["Name"]),
                        Username= Convert.ToString(dr["Username"]),
                        AccessToken=Convert.ToString(dr["AccessToken"]),
                    }
                );

            }
            return userList;
        }

        public Users GetById(int id)
        {
            connection();
            List<Users> productList = new List<Users>();
            string query = "SELECT * FROM Users where Id=" + id;
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                productList.Add(
                    new Users
                    {
                        
                        Name = Convert.ToString(dr["Name"]),
                        Username = Convert.ToString(dr["Username"]),
                        AccessToken = Convert.ToString(dr["AccessToken"]),

                    }
                );

            }
            return productList[0];
        }


        public string GetByToken(string token)
        {
            connection();
            List<Users> productList = new List<Users>();
            string query = "SELECT * FROM Users where AccessToken='" + token + "'";
            SqlCommand command = new SqlCommand(query, con)
            {
                CommandType = CommandType.Text
            };
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                productList.Add(
                    new Users
                    {
                       
                        Name = Convert.ToString(dr["Name"]),
                        Username = Convert.ToString(dr["Username"]),
                        AccessToken = Convert.ToString(dr["AccessToken"]),

                    }
                );

            }
            if (productList.Count > 0)
                return token;
            else
                return null;
        }


        public void AddUser(Users user)
        {
            connection();
            con.Open();
            string query = "Insert into Users(Name,Username,AccessToken) values(@Name,@userName,@accessToken)";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.Add(new SqlParameter("Name", user.Name));
            
            sqlCommand.Parameters.Add(new SqlParameter("userName", user.Username));
            sqlCommand.Parameters.Add(new SqlParameter("accessToken", user.AccessToken));
            sqlCommand.ExecuteNonQuery();
            
            con.Close();
        }

        public void Log(string req, string responseCode, string Exception = "null")
        {
            string constr = @"Data Source=TAVDESK026;Initial Catalog=LogDatabase;User Id=sa;Password=test123!@#";
            con = new SqlConnection(constr);
            con.Open();
            string query = "Insert into logger(Request,Response,Exception) values(@Request,@Response,@Exception)";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.Add(new SqlParameter("Request", req));

            sqlCommand.Parameters.Add(new SqlParameter("Response", responseCode));
            sqlCommand.Parameters.Add(new SqlParameter("Exception", Exception));
            sqlCommand.ExecuteNonQuery();

            con.Close();
        }
    }
}
