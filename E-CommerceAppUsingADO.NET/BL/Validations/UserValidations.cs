using E_CommerceAppUsingADO.NET.BL.Dtos;
using E_CommerceAppUsingADO.NET.BL.Enums;
using E_CommerceAppUsingADO.NET.BL.Methods;
using E_CommerceAppUsingADO.NET.BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_CommerceAppUsingADO.NET.BL.Validations
{
    class UserValidations:BaseValidation
    {
        //email
        public static bool isValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        public static bool FindEmail(string email)
        {
            DataTable dataTable = UserMethods.GetUserByEmail(email);
            return dataTable.Rows.Count != 0;
        }
        public static string chcekEmailValidationForRegister()
        {
            string email;
            Console.Write("Enter Email: ");
            do
            {
                email = Console.ReadLine();
                if (!isRequired(email))
                    DisplayTextWithRedColor("Email is Requied");
                else if (!isValidEmail(email))
                    DisplayTextWithRedColor("Enter Valid Email");
                else if (FindEmail(email))
                    DisplayTextWithRedColor("This Email is Already Exists");
            } while (!isRequired(email) || !isValidEmail(email) || FindEmail(email));
            return email;
        }
        public static string checkEmailValidationForLogin()
        {
            string email;
            Console.Write("Enter Email: ");
            do
            {
                email = Console.ReadLine();
                if(!isRequired(email))
                    DisplayTextWithRedColor("Email is Required");
            } while (!isRequired(email));
            return email;
        }

        //password
        public static string EncryptPassword(string password)
        {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
        }
        public static bool CheckPasswordMatch(string password, string confirmPassword) => password == confirmPassword;
        public static bool isValidPassword(string Password)
        {
            string regex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

            return Regex.IsMatch(Password, regex, RegexOptions.IgnoreCase);
        }
        public static string checkPasswordValidationForRegister()
        {
            string Password, ConfirmPassword;
            Console.Write("Enter Password: ");
            do
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Password = Console.ReadLine();
                if (!isRequired(Password))
                    DisplayTextWithRedColor("Password is Requied!");
                else if (!isValidPassword(Password))
                    DisplayTextWithRedColor("Enter Strong Password");
            } while (!isRequired(Password) || !isValidPassword(Password));
            Console.ResetColor();
            Console.Write("Enter Confirm Password: ");
            do
            {
                Console.ForegroundColor = ConsoleColor.Black;
                ConfirmPassword = Console.ReadLine();
                if (!isRequired(ConfirmPassword))
                    DisplayTextWithRedColor("Confirm Password is Requied!");
                else if (!CheckPasswordMatch(Password, ConfirmPassword))
                    DisplayTextWithRedColor("Password and Confirm Password Not Matched!!");
            } while (!isRequired(ConfirmPassword) || !CheckPasswordMatch(Password, ConfirmPassword));

            Password= EncryptPassword(Password);
            Console.ResetColor();
            return Password;
        }
        public static string checkPasswordValidationForLogin()
        {
            string Password;
            Console.Write("Enter Password: ");
            do
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Password = Console.ReadLine();
                if (!isRequired(Password))
                    DisplayTextWithRedColor("Password is Requied");
            } while (!isRequired(Password));
            Password = EncryptPassword(Password);
            Console.ResetColor();
            return Password;
        }

        //phone number
        public static bool isValidPhoneNumber(string input)
        {
            int phoneNumber;
            // egyption phone number;
            if (input.Length!=11) return false;
            return int.TryParse(input, out phoneNumber);
        }
        public static string checkPhoneNumberValidation()
        {
            bool valid = false;
            string phoneNumber;
            Console.WriteLine("Enter Phone Number");
            do
            {
                phoneNumber = Console.ReadLine();
                valid = isValidPhoneNumber(phoneNumber);
                if (!valid)
                    DisplayTextWithRedColor("Enter Valid Phone Number");
            } while (!valid);
            return phoneNumber;
        }
        //name
        public static bool isValidName(string name)
        {
            name = RemoveWhitespace(name);
            char[] chars = name.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!(chars[i] >= 'a' && chars[i] <= 'z') && !(chars[i] >= 'A' && chars[i] <= 'Z'))
                    return false;
            }
            return true;
        }
        //input for if firstname or lastname
        public static string checkNameValidation(string input)
        {
            string name;
            Console.Write($"Enter {input}: ");
            do
            {
                name = Console.ReadLine();
                if (!isRequired(name))
                    DisplayTextWithRedColor($"{input} Is Required!");
                else if (!isValidName(name))
                    DisplayTextWithRedColor($"Enter Valid {input}!");
            } while (!isRequired(name) || !isValidName(name));
            return name;
        }

        //Check Login 
        public static int CheckValidLogin()
        {
            DataTable dataTable = new DataTable();
            LoginDto login = new LoginDto();
            User user = new User();
            login.Email =checkEmailValidationForLogin();
            login.Password = checkPasswordValidationForLogin();
            dataTable = UserMethods.Login(login);
            if (dataTable.Rows.Count == 0)
            {
                DisplayTextWithRedColor("Email or Password Invalid");
                Console.ReadKey();
            }
            else
            {
                user.Id = Convert.ToInt32(dataTable.Rows[0][0]);
                user.FirstName = Convert.ToString(dataTable.Rows[0][1]);
                user.LastName = Convert.ToString(dataTable.Rows[0][2]);
                user.Password = Convert.ToString(dataTable.Rows[0][3]);
                user.UserType = (UserType)Convert.ToInt32(dataTable.Rows[0][4]);
                user.Email = Convert.ToString(dataTable.Rows[0][5]);
                user.PhoneNumber = Convert.ToString(dataTable.Rows[0][6]);
            }
            return user.Id;
        }
    }
}
