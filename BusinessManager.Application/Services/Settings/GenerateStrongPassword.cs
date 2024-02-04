using BusinessManager.Application.Interfaces.Settings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Services.Settings
{
    public class GenerateStrongPassword : IGenerateStrongPassword
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GenerateStrongPassword(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_+=<>?";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            // Ensure at least one digit
            if (options.RequireDigit)
            {
                string digits = "0123456789";
                password.Append(digits[random.Next(digits.Length)]);
            }

            // Ensure at least one lowercase letter
            if (options.RequireLowercase)
            {
                string lowercase = "abcdefghijklmnopqrstuvwxyz";
                password.Append(lowercase[random.Next(lowercase.Length)]);
            }

            // Ensure at least one uppercase letter
            if (options.RequireUppercase)
            {
                string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                password.Append(uppercase[random.Next(uppercase.Length)]);
            }

            // Ensure at least one non-alphanumeric character
            if (options.RequireNonAlphanumeric)
            {
                string nonAlphanumeric = "!@#$%^&*()-_+=<>?";
                password.Append(nonAlphanumeric[random.Next(nonAlphanumeric.Length)]);
            }

            // Fill the rest of the password length with random characters from validChars
            while (password.Length < options.RequiredLength)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }
    }
}
