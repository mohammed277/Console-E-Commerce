using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAppUsingADO.NET.PL;
using E_CommerceAppUsingADO.NET.BL.Enums;
using E_CommerceAppUsingADO.NET.BL.Dtos;

namespace E_CommerceAppUsingADO.NET.BL.Methods
{
    static class UserMethods
    {
        public static DataTable Login(LoginDto login)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@useremail", SqlDbType.NVarChar, 50);
            para[0].Value = login.Email;

            para[1] = new SqlParameter("@userpass", SqlDbType.NVarChar, 50);
            para[1].Value = login.Password;

            DA.open();

            DataTable dataTable = new DataTable();
            dataTable = DA.GetData("getUser", para);
            DA.close();
            return dataTable;
        }
        public static void Register(User user)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DA.open();
            SqlParameter[] para = new SqlParameter[6];

            para[0] = new SqlParameter("@firstname", SqlDbType.VarChar, 50);
            para[0].Value = user.FirstName;

            para[1] = new SqlParameter("@lastname", SqlDbType.VarChar, 50);
            para[1].Value = user.LastName;

            para[2] = new SqlParameter("@password", SqlDbType.VarChar, 50);
            para[2].Value = user.Password;
            
            para[3] = new SqlParameter("@userType", SqlDbType.Int);
            para[3].Value = user.UserType;

            para[4] = new SqlParameter("@phoneNumber", SqlDbType.VarChar, 50);
            para[4].Value = user.PhoneNumber;
            
            para[5] = new SqlParameter("@email", SqlDbType.VarChar, 50);
            para[5].Value = user.Email;

            DA.ExecuteCommand("user_register", para);
            DA.close();
        }
        public static UserType GetRule(int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@userid", SqlDbType.Int);
            para[0].Value = userId;
            DA.open();
            dataTable = DA.GetData("checkedUser", para);
            DA.close();
            return  (UserType)Convert.ToInt32(dataTable.Rows[0][0]);
        }
        public static string GetEmail(int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = userId;
            DA.open();
            dataTable = DA.GetData("sp_GetUserEmail", para);
            DA.close();
            return Convert.ToString(dataTable.Rows[0][0]);
        }
        public static DataTable GetUserByEmail(string email)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@email", SqlDbType.NVarChar,50);
            para[0].Value = email;
            DA.open();
            dataTable = DA.GetData("getUserByEmail", para);
            DA.close();
            return dataTable;
        }
        public static string GetFullNameByEmail(string email)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@email", SqlDbType.NVarChar,50);
            para[0].Value = email;
            DA.open();
            dataTable = DA.GetData("getFullName", para);
            DA.close();
            return Convert.ToString(dataTable.Rows[0][0]);
        }
    }
}
