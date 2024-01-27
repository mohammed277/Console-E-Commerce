
using ConsoleTables;
using E_CommerceAppUsingADO.NET.BL.Enums;
using E_CommerceAppUsingADO.NET.BL.Methods;
using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.PL
{
     class Home
    {
        public static int userId;
        public static UserType userType;
        public static void Header(int userId=0)
        {
            ConsoleTable table;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (userId == 0)
            {
                table = new ConsoleTable(" E-Commerce  ", "1- Home ", "2- Products", "3- Categories", "4- Orders", "5- Register", "6- Login");
            }
            else
            {
                userType = UserMethods.GetRule(userId);
                string FullName =UserMethods.GetFullNameByEmail((UserMethods.GetEmail(userId)));
                table = new ConsoleTable($" E-Commerce  ", "1- Home ", "2- Products", "3- Categories", "4- Orders", FullName, "6- Logout");
            }
            table.Write();
            Console.ResetColor();
        }
        public static void HomePage()
        {
            int choice;
                if (userType!=0)
                    Header(userId);
                else
                    Header();
            ProductUI.PrintProducts(Productmethods.GetAll());
            do
                {
                    choice = Console.ReadKey().KeyChar;
                    if (!(choice >= 49 && choice <= 54))
                        Console.WriteLine("\nchoose Valid Choice");
                } while (!(choice >= 49 && choice <= 54));
                HeaderComponent userOperations = (HeaderComponent)choice;
                switch (userOperations)
                {
                    case HeaderComponent.Home:
                        {
                        if (userType == 0)
                            Header();
                        else
                            Header(userId);
                        ProductUI.PrintProducts(Productmethods.GetAll());
                        break;
                    }
                    case HeaderComponent.Products:
                        {
                        Console.Clear();
                        if (userType == UserType.Admin)
                        {
                            Header(userId);
                            ProductUI.PrintProducts(Productmethods.GetAll());
                            ProductUI.CRUDProduct();
                        }
                        else
                        {
                            Header(userId);
                            ProductUI.PrintProducts(Productmethods.GetAll());
                            ProductUI.SearchProduct();
                        }
                        break;
                    }
                    case HeaderComponent.Categories:
                        {
                            Console.Clear();
                            if (userType==UserType.Admin)
                            {
                                Header(userId);
                            ProductUI.PrintProducts(Productmethods.GetAll());
                            CategoryUI.CRUDCategory();
                            }
                            else
                            {
                                Header(userId);
                            ProductUI.PrintProducts(Productmethods.GetAll());
                            CategoryUI.DisplayAllProductOFOneCategory();
                            }
                        break;
                    }
                    case HeaderComponent.Orders:
                        {
                        Console.Clear();
                        if (userType == UserType.Admin)
                        {
                            Header(userId);
                            OrderUI.DisplayAllOrder();
                        }
                        else if (userType == UserType.Customer)
                        {
                            Header(userId);
                            ProductUI.PrintProducts(Productmethods.GetAll());
                            OrderUI.CRUDOrder(userId);
                        }
                        else
                        {
                            Header();
                            ProductUI.PrintProducts(Productmethods.GetAll());
                        }
                        break;
                    }
                    case HeaderComponent.Register:
                        {
                        if (userType == 0)
                        {
                            Console.Clear();
                            Header();
                            string email = UserUI.UserRegiser(UserType.Customer).Email;
                            userId = Convert.ToInt32(UserMethods.GetUserByEmail(email).Rows[0][0]);
                            if (userId != 0)
                            {
                                Console.Clear();
                                Header(userId);
                                ProductUI.PrintProducts(Productmethods.GetAll());
                            }
                        }
                        else if (userType ==UserType.Admin)
                        {
                            Console.Clear();
                            Header(userId);
                            UserUI.UserRegiser(UserType.Admin);
                        }
                        break;
                    }
                    case HeaderComponent.LoginOrLogOut:
                        {
                        if (userType ==0)
                        {
                            Console.Clear();
                            Header();
                            userId = UserUI.UserLogin();
                            if (userId != 0)
                            {
                                Console.Clear();
                                Header(userId);
                                ProductUI.PrintProducts(Productmethods.GetAll());
                            }
                        }
                        else
                        {
                            userType = 0;
                            userId = 0;
                            Header();
                            ProductUI.PrintProducts(Productmethods.GetAll());
                        }
                            break;
                        }
                }
        }
    }
}
