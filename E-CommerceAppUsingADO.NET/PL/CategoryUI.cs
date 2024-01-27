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
    class CategoryUI
    {
        public static void CRUDCategory()
        {
            Console.WriteLine("1-Add Category");
            Console.WriteLine("2-Update Category");
            Console.WriteLine("3-Delete Delete");
            Console.WriteLine("4-Search Category");
            Console.WriteLine("5-Get All Categories");
            int ch;
            do
            {
                ch = Console.ReadKey().KeyChar;
                CRUD categoryOpearion = (CRUD)ch;
                switch (categoryOpearion)
                {
                    case CRUD.Create:
                        Add();
                        break;
                    case CRUD.GetById:
                        DisplayAllProductOFOneCategory();
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
        public static void DisplayAllProductOFOneCategory()
        {
            ProductUI.PrintProducts(CategoryValidations.GetProductsOFCategory());
            Console.ReadKey();
        }
        public static void Add()
        {
           string categoryName = AddOrUpdate();
            CategoryMethods.Create(categoryName);
            BaseValidation.DisplayTextWithGreenColor("Category Added Successfully");
        }
        public static void Update()
        {
            Category category = new Category();
            category = CategoryValidations.FindCatgoryIdByName();
            category.Name=AddOrUpdate();
            CategoryMethods.Update(category, category.Id);
           BaseValidation.DisplayTextWithGreenColor("Category Updated Successfully");
        }
        public static void Delete()
        {
            int categoryId=0;
           categoryId= CategoryValidations.FindCatgoryIdByName().Id;
                bool canDelete = CategoryMethods.Delete(categoryId);
                if (!canDelete)
                    BaseValidation.DisplayTextWithRedColor("There are Products Assign To This Category");
                else
                BaseValidation.DisplayTextWithGreenColor("Category Deleted Successfully.......");
        }
        public static string AddOrUpdate()
        {
            string categoryName;
            do
            {
                categoryName = CategoryValidations.validCategoryName();
                if (CategoryValidations.FindCategoryByName(categoryName))
                    BaseValidation.DisplayTextWithRedColor("This Category is Exist");
            } while (CategoryValidations.FindCategoryByName(categoryName));
            return categoryName;
        }
    }
}
