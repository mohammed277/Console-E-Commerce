using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.DAL
{
    class DataAccessLayer
    {
        SqlConnection con;
        public DataAccessLayer()
        {
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=EcommerceProject;Integrated Security=True");
        }
        public void open()
        {
            if (con.State != ConnectionState.Open)
                con.Open();
        }
        public void close()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        public DataTable GetData(string procedure, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            cmd.Connection = con;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void ExecuteCommand(string procedure, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            cmd.Connection = con;
            if (para != null)
            {
                cmd.Parameters.AddRange(para);
            }
            cmd.ExecuteNonQuery();
        }

    }
}
