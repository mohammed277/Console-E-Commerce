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
    class ProductValidations : BaseValidation
    {
        public static Product checkProductNameValidation()
        {
            string productName;
            bool validProductName;
            Product product = new Product();
            DataTable dataTable = new DataTable();
            Console.WriteLine("Enter Product Name");
            do
            {
                productName = Console.ReadLine();
                validProductName = isRequired(productName);
                if (validProductName)
                {
                    dataTable = Productmethods.GetByName(productName);
                    if (dataTable.Rows.Count==0)
                        DisplayTextWithRedColor("This Product Not Exist!");
                }
                else
                {
                    DisplayTextWithRedColor("Product Name is Required!");
                }
            } while (!validProductName || dataTable.Rows.Count == 0);

            product.Id = Convert.ToInt32(dataTable.Rows[0][0]);
            product.Name = Convert.ToString(dataTable.Rows[0][1]);
            product.Quantity = Convert.ToInt32(dataTable.Rows[0][2]);
            product.Price = Convert.ToDecimal(dataTable.Rows[0][3]);
            product.Description = Convert.ToString(dataTable.Rows[0][4]);
            product.CategoryId = Convert.ToInt32(dataTable.Rows[0][5]);

            return product;
        }
        public static int validQuantity(int productId)
        {
            int quantity, availableQuantity;
            bool checkValidQuantity;
            Console.WriteLine("Enter Quantity OF Product");
            do
            {
                checkValidQuantity = int.TryParse(Console.ReadLine(), out quantity);
                availableQuantity = Productmethods.GetAvaliableQuantityOFProduct(productId);
                if (!checkValidQuantity || quantity <= 0)
                    DisplayTextWithRedColor("Enter Valid Quantity");
                else if (availableQuantity <= 0)
                {
                    DisplayTextWithRedColor("This Product Out OF Stock");
                    return 0;
                }
                else if (availableQuantity < quantity)
                {
                    Console.Write($"Sorry, This Quantity Not available. Maximum Number OF Quantity You Can Orderd Is");
                    DisplayTextWithRedColor($"{availableQuantity}");
                }
            } while (!checkValidQuantity || availableQuantity < quantity || quantity<=0);
            return quantity;
        }
        public static string validProductName()
            {
            bool isEmpty;
            string input;
            Console.Write("Enter Name OF Product : ");
            do
            {
                input = Console.ReadLine();
                isEmpty = isRequired(input);
                if (!isEmpty)
                    DisplayTextWithRedColor("Product Name is Required");
                else if (FindProductByName(input))
                    DisplayTextWithRedColor("This Product is Exist!");
            } while (!isEmpty||FindProductByName(input));
            return input;
        }
        public static int validProductQuantity()
        {
            int quantity;
            bool isValid;
            Console.Write("Enter Product Quantity: ");
            do
            {
                isValid = int.TryParse(Console.ReadLine(), out quantity);
                if (!isValid || quantity <= 0)
                    DisplayTextWithRedColor("Enter Valid Quantity");
            } while ((!isValid || quantity <= 0));
            return quantity;
        }
        public static decimal validProductPrice()
        {
            decimal price;
            bool isValid;
            Console.Write("Enter Product Price: ");
            do
            {
                isValid = Decimal.TryParse(Console.ReadLine(), out price);
                if (!isValid || price <= 0)
                    DisplayTextWithRedColor("Enter Valid Price");
            } while ((!isValid || price <= 0));
            return price;
        }
        public static string validProductDescription()
        {
            bool isEmpty;
            string input;
            Console.Write("Enter Name OF Product Description : ");
            do
            {
                input = Console.ReadLine();
                isEmpty = isRequired(input);
                if (!isEmpty)
                    DisplayTextWithRedColor("Product Description is Required");
            } while (!isEmpty);
            return input;
        }
        public static bool FindProductByName(string name)
        {
            DataTable dataTable = Productmethods.GetByName(name);
            return dataTable.Rows.Count != 0;
        }
    }
}
