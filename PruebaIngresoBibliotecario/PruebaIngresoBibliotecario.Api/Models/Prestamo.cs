using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class Prestamo
    {      

        [Key]
        public int id_prestamo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public string isbn { get; set; }


        public string identificacionUsuario { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? FechaPrestamo { get; set; }

        
        [Column(TypeName = "datetime")]
        public DateTime? FechaMaximo { get; set; }
        

    }
}