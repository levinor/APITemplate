using Levinor.APITemplate.Models.User;
using Levinor.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserCreationValidator : ValidationAttribute
    {
        public UserCreationValidator() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var newUser = value as UserModel;
            var service = (IUserService)validationContext.GetService(typeof(IUserService));

            return  ?
                 ValidationResult.Success :
                    new ValidationResult("Invalid new Password. It must contain at least a letter, a number and be at least 8 characters long");

        }
    }
}
