using E_CommerceAppUsingADO.NET.BL.Dtos;
using E_CommerceAppUsingADO.NET.BL.Methods;
using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Validations
{
    class OrderValidations
    {
        public static OrderDetails GetOrderDetails(int orderId)
        {
            Product product = new Product();
            product = ProductValidations.checkProductNameValidation();
            OrderDetails orderDetails = new OrderDetails();
            DataTable dataTable = OrderDetailsMethods.GetOrderDetailsOFProduct(orderId, product.Id);
            if (dataTable.Rows.Count == 0)
                BaseValidation.DisplayTextWithRedColor("You Not Orderd This Product");
            else
            {
                orderDetails.ProductId = Convert.ToInt32(dataTable.Rows[0][0]);
                orderDetails.OrderId = Convert.ToInt32(dataTable.Rows[0][1]);
                orderDetails.Quantity = Convert.ToInt32(dataTable.Rows[0][2]);
                orderDetails.TotalPrice = Convert.ToDecimal(dataTable.Rows[0][3]);
            }
            return orderDetails;
        }
        public static List<DIsplayAllOrdersDto> GetAllOrders()
        {
            DataTable dataTable = OrderMethods.getAllOrders();
            List<DIsplayAllOrdersDto> DIsplayAllOrdersDto = new List<DIsplayAllOrdersDto>();
            if (dataTable.Rows.Count == 0)
                BaseValidation.DisplayTextWithRedColor("No Orders yet....");
            else
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DIsplayAllOrdersDto displayOrdersDto = new DIsplayAllOrdersDto();
                    displayOrdersDto.FullName = Convert.ToString(dataTable.Rows[i][0]);
                    displayOrdersDto.Email = Convert.ToString(dataTable.Rows[i][1]);
                    displayOrdersDto.OrderDate = Convert.ToDateTime(dataTable.Rows[i][2]);
                    displayOrdersDto.FinalPrice = Convert.ToDecimal(dataTable.Rows[i][3]);
                    DIsplayAllOrdersDto.Add(displayOrdersDto);
                }
            }
            return DIsplayAllOrdersDto;
        }
        public static DisplayOrdersOFUserDto GetOrderOFUser(int userId,int orderId)
        {
            DisplayOrdersOFUserDto displayOrdersDto = new DisplayOrdersOFUserDto();
            DataTable dataTable = OrderMethods.getAllOrdersOFUser(userId,orderId);
            if (dataTable.Rows.Count == 0)
                BaseValidation.DisplayTextWithRedColor("No Orders");
            else
            {
                    displayOrdersDto.OrderId = Convert.ToInt32(dataTable.Rows[0][0]);
                    displayOrdersDto.OrderDate = Convert.ToDateTime(dataTable.Rows[0][1]);
                    displayOrdersDto.FinalPrice = Convert.ToDecimal(dataTable.Rows[0][2]);
            }
            return displayOrdersDto;
        }
        public static List<DisplayOrderDetailsDto> GetAllOrderDetailsOFUser(int userId)
        {
            List<DisplayOrderDetailsDto> displayOrderDetailsDtos = new List<DisplayOrderDetailsDto>();
            DataTable dataTable = OrderDetailsMethods.getOrderDetailsOFUser(userId);
            if (dataTable.Rows.Count == 0)
                BaseValidation.DisplayTextWithRedColor("No Orders");
            else
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DisplayOrderDetailsDto displayOrderDetailsDto = new DisplayOrderDetailsDto();
                    displayOrderDetailsDto.OrderId = Convert.ToInt32(dataTable.Rows[i][0]);
                    displayOrderDetailsDto.ProductName = Convert.ToString(dataTable.Rows[i][1]);
                    displayOrderDetailsDto.Quantity = Convert.ToInt32(dataTable.Rows[i][2]);
                    displayOrderDetailsDto.Price = Convert.ToDecimal(dataTable.Rows[i][3]);
                    displayOrderDetailsDto.TotalPrice = Convert.ToDecimal(dataTable.Rows[i][4]);
                    displayOrderDetailsDtos.Add(displayOrderDetailsDto);
                }
            }
            return displayOrderDetailsDtos;
        }
        public static bool ProductDetailsIsNull(OrderDetails orderDetails)
        {
            return orderDetails.OrderId == 0 && orderDetails.ProductId == 0 && orderDetails.Quantity == 0 && orderDetails.TotalPrice == 0;
        }
    }
}
