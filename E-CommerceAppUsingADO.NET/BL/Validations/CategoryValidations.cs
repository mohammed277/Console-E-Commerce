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
    class CategoryValidations:BaseValidation
    {
        public static string validCategoryName()
        {
            bool isEmpty;
            string input;
            Console.Write("Enter Name OF Categoty : ");
            do
            {
                input = Console.ReadLine();
                isEmpty = isRequired(input);
                if (!isEmpty)
                    DisplayTextWithRedColor("Catgory Name is Required");
            } while (!isEmpty);
            return input;
        }
        public static Category FindCatgoryIdByName()
        {
            DataTable dataTable = new DataTable();
            Category category = new Category();
            do
            {
                string categoryName = validCategoryName();
                 dataTable = CategoryMethods.GetByName(categoryName);
                if (dataTable.Rows.Count != 0)
                {
                    category.Id = Convert.ToInt32(dataTable.Rows[0][0]);
                    category.Name = Convert.ToString(dataTable.Rows[0][1]);
                }
                else
                    DisplayTextWithRedColor("This Category Not Exist");
                
            } while (dataTable.Rows.Count == 0);

            return category;
        }
        public static List<Product> GetProductsOFCategory()
        {
            string categoryName = FindCatgoryIdByName().Name;
            DataTable dataTable = Productmethods.GetAllByCategoryName(categoryName);
            List<Product> products = new List<Product>();
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
        public static bool FindCategoryByName(string name)
        {
           return CategoryMethods.GetByName(name).Rows.Count != 0;
        }

    }
}
