using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Methods
{
    static class CategoryMethods
    {
        public static void Create(string categoryName)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            para[0].Value = categoryName.ToLower();
            DA.open();
            DA.ExecuteCommand("sp_CreateCategory", para);
            DA.close();
        }
        public static void Update(Category category, int categoryId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = categoryId;
            para[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            para[1].Value = category.Name.ToLower();
            DA.open();
            DA.ExecuteCommand("sp_UpdateCategory", para);
            DA.close();
        }
        public static bool Delete(int categoryId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = categoryId;
            DA.open();
            try
            {
                DA.ExecuteCommand("sp_DeleteCategory", para);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                DA.close();
            }
        }
        public static Category GetById(int categoryId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            Category category = new Category();
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = categoryId;
            DA.open();
            dataTable = DA.GetData("sp_GetCategoryById", para);
            DA.close();
            category.Id = Convert.ToInt32(dataTable.Rows[0][0]);
            category.Name = Convert.ToString(dataTable.Rows[0][1]);
            return category;
        }
        public static DataTable GetByName(string name)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@Name", SqlDbType.NVarChar,50);
            para[0].Value = name;
            DA.open();
            dataTable = DA.GetData("sp_GetCategoryByName", para);
            DA.close();
            return dataTable;
        }
        public static List<Category> GetAll()
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dataTable = new DataTable();
            List<Category> categories = new List<Category>();
            DA.open();
            dataTable = DA.GetData("sp_GetAllCategory", null);
            DA.close();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Category category = new Category();
                category.Id = Convert.ToInt32(dataTable.Rows[0][0]);
                category.Name = Convert.ToString(dataTable.Rows[0][1]);
                categories.Add(category);
            }
            return categories;
        }
        //public static int GetCountOFProductRelatedCategory(int categoryId)
        //{
        //    DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
        //    SqlParameter[] para = new SqlParameter[1];
        //    DataTable dataTable = new DataTable();
        //    para[0] = new SqlParameter("@id", SqlDbType.Int);
        //    para[0].Value = categoryId;
        //    DA.open();
        //    dataTable = DA.GetData("GetCountOFProductRelatedCategory", para);
        //    DA.close();
        //    return Convert.ToInt32(dataTable.Rows[0][0]);
        //}
    }
}
