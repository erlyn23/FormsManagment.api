using Microsoft.EntityFrameworkCore;
using SimpleJwt.DbContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Requests
{
    public class AccountRequest
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="Escribe tu nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Escribe tu apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Escribe tu correo")]
        [EmailAddress(ErrorMessage = "El dato enviado no es un correo electrónico")]
        [ExistsInDb]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(8, ErrorMessage = "Escribe al menos 8 letras")]
        [MaxLength(16, ErrorMessage = "La contraseña no debe contener más de 16 letras")]
        public string Password { get; set; }
        public string ProfilePhoto { get; set; }
    }
    public class ExistsInDb: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (FormsManagmentDbContext)validationContext.GetService(typeof(FormsManagmentDbContext));
            string passedData = value.ToString();

            var emailInDb = dbContext.Users.Where(u => u.Email.Equals(passedData)).FirstOrDefaultAsync();
            if (emailInDb.Result != null)
                return new ValidationResult("El usuario con este email ya existe, intenta con uno nuevo.");
            return ValidationResult.Success;
        }
    }
}
