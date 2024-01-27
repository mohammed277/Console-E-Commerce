using ConsoleTables;
using E_CommerceAppUsingADO.NET.BL.Dtos;
using E_CommerceAppUsingADO.NET.BL.Enums;
using E_CommerceAppUsingADO.NET.BL.Methods;
using E_CommerceAppUsingADO.NET.BL.Models;
using E_CommerceAppUsingADO.NET.BL.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.PL
{
     class OrderUI
    {
        public static int orderId;
        public static void CRUDOrder(int userId)
        {
            Console.WriteLine("1-Add Order");
            Console.WriteLine("2-Update Order");
            Console.WriteLine("3-Delete Order");
            Console.WriteLine("4-Get All Your Orders");
            int ch;
            do
            {
                ch = Console.ReadKey().KeyChar;
                CRUD orderOperaions = (CRUD)ch;
                switch (orderOperaions)
                {
                    case CRUD.Create:
                        CreateOrderDetails(userId);
                        break;
                    case CRUD.Update:
                        UpdateOrderDetails(orderId);
                        break;
                    case CRUD.GetById:
                        DisplayAllOrderDetailsOFUser(userId);
                        break;
                    case CRUD.Delete:
                        DeleteOrderDetails(orderId);
                        break;
                }

            } while (ch >= 49 && ch <= 52);
        }
        public static void CreateOrder(int userId)
        {
            orderId = OrderMethods.Create(userId);
        }
        public static void CreateOrderDetails(int userId)
        {
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            char c;
            decimal finalPrice = 0;
            do
            {
                Product product = new Product();
                product = ProductValidations.checkProductNameValidation();
                int quantity = ProductValidations.validQuantity(product.Id);
                if (quantity == 0)
                {
                    Console.ReadKey();
                    return;
                }
                else
                {
                    if (orderId == 0)
                        CreateOrder(userId);
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.OrderId = orderId;
                    orderDetails.ProductId = product.Id;
                    orderDetails.Quantity = quantity;
                    orderDetails.TotalPrice = product.Price * quantity;
                    finalPrice += orderDetails.TotalPrice;
                    OrderDetailsMethods.CreateOrderDetails(orderDetails);
                    BaseValidation.DisplayTextWithGreenColor("Order Created Successfully......");
                    orderDetailsList.Add(orderDetails);
                    DisplayOrderDetails(orderDetailsList);
                    Console.WriteLine("are you want to continue ? y/n");
                }
                c = Console.ReadKey().KeyChar;
            } while (c != 'n');
            DisplayFinalPrice(finalPrice);
            Console.ReadKey();
        }
        public static void UpdateOrderDetails(int orderId)
        {
            OrderDetails orderDetails = new OrderDetails();
            orderDetails = OrderValidations.GetOrderDetails(orderId);
            if (!OrderValidations.ProductDetailsIsNull(orderDetails))
            {
                OrderDetailsMethods.DeleteOrderDetails(orderDetails);
                int quantity = ProductValidations.validQuantity(orderDetails.ProductId);
                orderDetails.OrderId = orderId;
                orderDetails.ProductId = orderDetails.ProductId;
                orderDetails.Quantity = quantity;
                orderDetails.TotalPrice =Productmethods.GetById(orderDetails.ProductId).Price * quantity;
                OrderDetailsMethods.CreateOrderDetails(orderDetails);
                BaseValidation.DisplayTextWithGreenColor("Order Updated Successfully......");
                DisplayOrderDetails(new List<OrderDetails>() { orderDetails });
            }
            Console.ReadKey();
        }
        public static void DeleteOrderDetails(int orderId)
        {
            OrderDetails orderDetails = new OrderDetails();
            orderDetails = OrderValidations.GetOrderDetails(orderId);
            char c;
            if (!OrderValidations.ProductDetailsIsNull(orderDetails))
            {
                do
                {
                    Console.WriteLine("Are you sure you want to delete this order ? (y/n) : ");
                    c = Console.ReadKey().KeyChar;
                } while (c != 'y' && c != 'n');
                if (c == 'y')
                {
                    {
                        OrderDetailsMethods.DeleteOrderDetails(orderDetails);
                        BaseValidation.DisplayTextWithGreenColor("Order Deleted Successfully......");
                    }
                }
            }
            Console.ReadKey();
        }
        public static void DisplayFinalPrice(decimal FinalPrice)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            var table = new ConsoleTable("Final Price");
                table.AddRow(FinalPrice.ToString("C2"));
            table.Write();
            Console.ResetColor();
        }
        public static void DisplayOrderDetails(List<OrderDetails> orderDetails)
        {
            var table = new ConsoleTable("Product Name", "Quantity", "Product Price", "Total Price");
            foreach (var orderdetails in orderDetails)
            {
                table.AddRow(Productmethods.GetById(orderdetails.ProductId).Name, orderdetails.Quantity,Productmethods.GetById(orderdetails.ProductId).Price.ToString("C2"), orderdetails.TotalPrice.ToString("C2"));
            }
            table.Write();
        }
        public static void DisplayAllOrder()
        {
            List<DIsplayAllOrdersDto> dIsplayAllOrdersDtos = new List<DIsplayAllOrdersDto>();
            dIsplayAllOrdersDtos = OrderValidations.GetAllOrders();
            if (dIsplayAllOrdersDtos.Count > 0)
            {
                var table = new ConsoleTable("Full Name", "Email", "Order Date", "Final Price");
                foreach (var order in dIsplayAllOrdersDtos)
                {
                    table.AddRow(order.FullName, order.Email, order.OrderDate, order.FinalPrice.ToString("C2"));

                }
                table.Write();
            }
            Console.ReadKey();
        }
        public static void DisplayOrdersOFUser(int userId,int orderId)
        {
            DisplayOrdersOFUserDto displayOrdersOFUserDtos = new DisplayOrdersOFUserDto();
            displayOrdersOFUserDtos = OrderValidations.GetOrderOFUser(userId,orderId);
            if(displayOrdersOFUserDtos.FinalPrice!=0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                var table = new ConsoleTable("Order Date", "Final Price");
                   table.AddRow(displayOrdersOFUserDtos.OrderDate, displayOrdersOFUserDtos.FinalPrice.ToString("C2"));
                
                table.Write();
                Console.ResetColor();
            }
        }
        public static void DisplayAllOrderDetailsOFUser(int userId)
        {
            List<DisplayOrderDetailsDto> displayOrderDetailsDtos = OrderValidations.GetAllOrderDetailsOFUser(userId);
            if (displayOrderDetailsDtos.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                var table = new ConsoleTable("Product Name", "Quantity", "Price", "Total Price");
                int orderId = displayOrderDetailsDtos.First().OrderId;
                foreach (var orderDetails in displayOrderDetailsDtos)
                {
                    if (orderId != orderDetails.OrderId)
                    {
                        table.Write();
                        DisplayOrdersOFUser(userId, orderId);
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        table = new ConsoleTable("Product Name", "Quantity", "Price", "Total Price");
                        orderId = orderDetails.OrderId;
                        table.AddRow(orderDetails.ProductName, orderDetails.Quantity, orderDetails.Price.ToString("C2"), orderDetails.TotalPrice.ToString("C2"));
                    }
                    else
                    {
                        table.AddRow(orderDetails.ProductName, orderDetails.Quantity, orderDetails.Price.ToString("C2"), orderDetails.TotalPrice.ToString("C2"));
                    }
                }
                table.Write();
                DisplayOrdersOFUser(userId, orderId);
                Console.ResetColor();
            }
        }
    }
}
