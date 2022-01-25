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

        public Guid isbn { get; set;}

        [Required(ErrorMessage = "La identificaion del usuario obligatoria")]
        [MaxLength(10, ErrorMessage = "La identificaion del usuario debe ser maximo de 10 caracteres")]
        public string identificacionUsuario { get; set; }


        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [Range(1,3)]
        public int tipoUsuario { get; set; }

        public DateTime fechaMaximaDevolucion { get; set; }


    }
}