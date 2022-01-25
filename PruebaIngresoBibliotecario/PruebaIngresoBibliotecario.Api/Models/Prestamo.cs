using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Prestamo
    {

        [Key]
        public Guid id { get; set; }


        [Required(ErrorMessage = "El codigo isbn es obligatorio")]
        [isValidGuidAttribute]
        public string isbn { get; set; }

            


        
        [Required(ErrorMessage = "La identificaion del usuario obligatoria")]
        [MaxLength(10, ErrorMessage = "La identificaion del usuario debe ser maximo de 10 caracteres")]
        public string identificacionUsuario { get; set; }


        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [Range(1,3)]
        public int tipoUsuario { get; set; }

        public DateTime fechaMaximaDevolucion { get; set; }


    }

    public class isValidGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            Guid x;
            bool isValid = Guid.TryParse(value.ToString(), out x);

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("El codigo isbn no es valido");
            }
  
        }

    }

}