using SimpleJwt.DbContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Requests
{
    public class FormDto
    {
        public int FormId { get; set; }
        [Required(ErrorMessage = "El propietario es requerido")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "El nombre del formulario es requerido")]
        [ExistisInDb]
        public string Name { get; set; }
        [Required(ErrorMessage = "El título del formulario es requerido")]
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ExistisInDb : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (FormsManagmentDbContext)validationContext.GetService(typeof(FormsManagmentDbContext));

            var name = value.ToString();

            if (name.EndsWith("etvcreate"))
            {
                name.Replace("etvcreate", "");
                var form = dbContext.Forms.Where(f => f.Name.Equals(name)).FirstOrDefault();

                if (form != null) return new ValidationResult("Ya existe un formulario con este nombre, intenta con uno nuevo");

                return ValidationResult.Success;
            }
            else
            {
                return ValidationResult.Success;
            }

            
        }
    }
}
