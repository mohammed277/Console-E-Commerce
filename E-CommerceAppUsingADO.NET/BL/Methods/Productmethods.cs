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
    static class Productmethods
    {
        public static int GetAvaliableQuantityOFProduct(int productId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = productId;
            DA.open();
            DataTable dt = new DataTable();
            dt = DA.GetData("sp_GetProductQuantity", para);
            DA.close();
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public static void Create(Product product)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@Name", SqlDbType.NVarChar,50);
            para[0].Value = product.Name;

            para[1] = new SqlParameter("@Quantity", SqlDbType.Int);
            para[1].Value = product.Quantity;

            para[2] = new SqlParameter("@Price", SqlDbType.Decimal);
            para[2].Value = product.Price;

            para[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
            para[3].Value = product.Description;

            para[4] = new SqlParameter("@CategoryId", SqlDbType.Int);
            para[4].Value = product.CategoryId;
            DA.open();
            DA.ExecuteCommand("sp_CreateProduct", para);
            DA.close();
        }
        public static void Update(Product product,int productId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@Id", SqlDbType.Int);
            para[0].Value = productId;
            
            para[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            para[1].Value = product.Name;
            
            para[2] = new SqlParameter("@Quantity", SqlDbType.Int);
            para[2].Value = product.Quantity;
            
            para[3] = new SqlParameter("@Price", SqlDbType.Decimal);
            para[3].Value = product.Price;

            para[4] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
            para[4].Value = product.Description;
            
            para[5] = new SqlParameter("@categoryId", SqlDbType.Int);
            para[5].Value = product.CategoryId;
            DA.open();
            DA.ExecuteCommand("sp_UpdateProduct", para);
            DA.close();
        }
        public static void Delete(int productId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@ProductID", SqlDbType.Int);
            para[0].Value = productId;
            DA.open();
            DA.ExecuteCommand("DeleteProduct", para);
            DA.close();
        }
        public static Product GetById(int productId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            Product product = new Product();
            para[0] = new SqlParameter("@ProductID", SqlDbType.Int);
            para[0].Value = productId;
            DA.open();
            dataTable =  DA.GetData("GetProductById",para);
            DA.close();
            product.Id = productId;
            product.Name = Convert.ToString(dataTable.Rows[0][1]);
            product.Quantity = Convert.ToInt32(dataTable.Rows[0][2]);
            product.Price = Convert.ToDecimal(dataTable.Rows[0][3]);
            product.Description = Convert.ToString(dataTable.Rows[0][4]);
            product.CategoryId = Convert.ToInt32(dataTable.Rows[0][5]);
            return product;
        }
        public static DataTable GetByName(string productName)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            DataTable dataTable = new DataTable();
            Product product = new Product();
            para[0] = new SqlParameter("@ProductName", SqlDbType.VarChar,50);
            para[0].Value = productName;
            DA.open();
            dataTable = DA.GetData("GetProductByName", para);
            DA.close();
            return dataTable;
        }
        public static List<Product> GetAll()
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dataTable = new DataTable();
            List<Product> products = new List<Product>();
            DA.open();
            dataTable = DA.GetData("GetAllProducts", null);
            DA.close();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataTable.Rows[i][0]);
                product.Name = Convert.ToString(dataTable.Rows[i][1]);
                product.Quantity = Convert.ToInt32(dataTable.Rows[i][2]);
                product.Price = Convert.ToDecimal(dataTable.Rows[i][3]);
                product.Description = Convert.ToString(dataTable.Rows[i][4]);
                product.CategoryId = Convert.ToInt32(dataTable.Rows[i][5]);
                products.Add(product);
            }
            return products;
        }
        public static DataTable GetAllByCategoryName(string categoryName)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            DataTable dataTable = new DataTable();
            List<Product> products = new List<Product>();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar,50);
            para[0].Value = categoryName;
            DA.open();
            dataTable = DA.GetData("GetProductsByCategoryName", para);
            DA.close();
            return dataTable;
        }
    }
}
