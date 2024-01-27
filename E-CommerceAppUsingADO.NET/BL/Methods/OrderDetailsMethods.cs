using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAppUsingADO.NET.BL.Validations;
using System.Runtime.Intrinsics.Arm;

namespace E_CommerceAppUsingADO.NET.BL.Methods
{
    static class OrderDetailsMethods
    {
        public static OrderDetails CreateOrderDetails(OrderDetails orderDetails)
        {
                DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
                SqlParameter[] para = new SqlParameter[4];

                para[0] = new SqlParameter("@orderId", SqlDbType.Int);
                para[0].Value =orderDetails.OrderId;

                para[1] = new SqlParameter("@productId", SqlDbType.Int);
                para[1].Value = orderDetails.ProductId;

                para[2] = new SqlParameter("@quantity", SqlDbType.Int);
                para[2].Value = orderDetails.Quantity;

                para[3] = new SqlParameter("@totalPrice", SqlDbType.Decimal);
                para[3].Value =orderDetails.TotalPrice;
                DA.open();
                DA.ExecuteCommand("createOrderDetails", para);
                DA.close();
            return orderDetails;
        }
        public static DataTable getOrderDetailsOFUser(int userId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@userId", SqlDbType.Int);
            para[0].Value = userId;
            DataTable dataTable = new DataTable();
            DA.open();
            dataTable = DA.GetData("getOrderDetailsOFUser", para);
            DA.close();
            return dataTable;
        }
        public static void DeleteOrderDetails(OrderDetails orderDetails)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();

            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@orderId", SqlDbType.Int);
            para[0].Value = orderDetails.OrderId;
            para[1] = new SqlParameter("@productId", SqlDbType.Int);
            para[1].Value = orderDetails.ProductId;
            para[2] = new SqlParameter("@quantity", SqlDbType.Int);
            para[2].Value = orderDetails.Quantity;
            para[3] = new SqlParameter("@totalPrice", SqlDbType.Decimal);
            para[3].Value = orderDetails.TotalPrice;
            DA.open();
            DA.ExecuteCommand("deleteOrderDetails", para);
            DA.close();
        }
        public static DataTable GetOrderDetailsOFProduct(int orderId, int productId)
        {
            DAL.DataAccessLayer DA = new DAL.DataAccessLayer();
            SqlParameter[] para = new SqlParameter[2];
            DataTable dataTable = new DataTable();
            para[0] = new SqlParameter("@orderId", SqlDbType.Int);
            para[0].Value = orderId;
            para[1] = new SqlParameter("@productId", SqlDbType.Int);
            para[1].Value = productId;
            DA.open();
            dataTable = DA.GetData("getOrderDetails", para);
            DA.close();
            return dataTable;
        }
    }
}
