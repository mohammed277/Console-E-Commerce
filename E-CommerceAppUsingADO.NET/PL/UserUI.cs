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
     class UserUI
    {
        public static User UserRegiser(UserType userType)
        {
            User user = new User();
            user.FirstName = UserValidations.checkNameValidation("First Name");
            user.LastName = UserValidations.checkNameValidation("Last Name");
            user.Email = UserValidations.chcekEmailValidationForRegister();
            user.Password = UserValidations.checkPasswordValidationForRegister();
            user.PhoneNumber = UserValidations.checkPhoneNumberValidation();
            user.UserType = userType;
            UserMethods.Register(user);
            return user;
        }
        public static int UserLogin()
        {
          return UserValidations.CheckValidLogin();
        }
    }
}
