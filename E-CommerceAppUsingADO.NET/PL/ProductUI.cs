using ConsoleTables;
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
     class ProductUI
    {
        public static void CRUDProduct()
        {
            Console.WriteLine("1-Add Product");
            Console.WriteLine("2-Update Product");
            Console.WriteLine("3-Delete Product");
            Console.WriteLine("4-Search Product");
            Console.WriteLine("5-Get All Product");
            int ch;
            do
            {
                ch = Console.ReadKey().KeyChar;
                CRUD prodcutOperation = (CRUD)ch;
                switch (prodcutOperation)
                {
                    case CRUD.Create:
                        Add();
                        break;
                    case CRUD.GetById:
                        SearchProduct();
                        break;
                    case CRUD.Update:
                        Update();
                        break;
                    case CRUD.Delete:
                        Delete();
                        break;
                }

            } while (ch >= 49 && ch <= 52);
        }
        public static void PrintProducts(List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var table = new ConsoleTable( " Name ", " Price ", " Quantity ", "Description" , "Category");
            foreach (Product product in products)
            {
                    table.AddRow(product.Name, product.Price.ToString("C2"),product.Quantity==0?"Out OF Stock":product.Quantity, product.Description, CategoryMethods.GetById(product.CategoryId).Name);
            }
            table.Write();
            Console.ResetColor();
        }
        public static void SearchProduct()
        {
            Product product = ProductValidations.checkProductNameValidation();
            PrintProducts(new List<Product>() { product });
            Console.ReadKey();
        }
        public static void Add()
        {
            Productmethods.Create(AddOrUpdate());
            BaseValidation.DisplayTextWithGreenColor("Product Added Successfully......");
        }
        public static void Update()
        {
            Product product = new Product();
            int productId;
            productId = ProductValidations.checkProductNameValidation().Id;
            product = AddOrUpdate();
            Productmethods.Update(product, productId);
            BaseValidation.DisplayTextWithGreenColor("Updated Successfully......");
        }
        public static void Delete()
        {
            int productId = ProductValidations.checkProductNameValidation().Id;
            Productmethods.Delete(productId);
            BaseValidation.DisplayTextWithGreenColor("Product Deleted .....");
        }
        public static Product AddOrUpdate()
        {
            Product product = new Product();
            product.Name= ProductValidations.validProductName();
            product.Price = ProductValidations.validProductPrice();
            product.Quantity = ProductValidations.validProductQuantity();
            product.Description = ProductValidations.validProductDescription();
            product.CategoryId = CategoryValidations.FindCatgoryIdByName().Id;
            return product;
        }
    }
}
