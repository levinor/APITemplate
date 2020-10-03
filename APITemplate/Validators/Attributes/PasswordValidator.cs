using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidator : ValidationAttribute
    {
        public PasswordValidator() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            //Has numbers
            var hasNumber = new Regex(@"[0-9]+");
            //Has letters
            var hasChar = new Regex(@"[a-zA-Z_.-]*");
            //At least has 8 characters
            var hasMinimum8Chars = new Regex(@".{8,}");
            
            return hasNumber.IsMatch(password) && hasChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password)?
                 ValidationResult.Success :
                    new ValidationResult("Invalid new Password. It must contain at least a letter, a number and be at least 8 characters long");

        }
    }
}
