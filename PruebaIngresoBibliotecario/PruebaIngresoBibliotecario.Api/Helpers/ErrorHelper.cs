using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Helpers
{
    public class ErrorHelper
    {
        public static ResponseObject Response(int StatusCode, string Message)
        {
            return new ResponseObject()
            {
                //Type = "C",
                //StatusCode = StatusCode,
                mensaje = Message
            };
        }

        public static ResponseObjectRegis ResponseRegis(string Id,DateTime FechaMaximaDevolucion)
        {
            return new ResponseObjectRegis()
            {
                //Type = "C",
                //StatusCode = StatusCode,
                id = Id,
                fechaMaximaDevolucion = FechaMaximaDevolucion.ToString("dd/MM/yyyy")

            };
        }

        public static ResponseValiPrestamo ResponseValiPrestamo(string Id, string Isbn,string IdentificaciónUsuario, int TipoUsuario,DateTime FechaMaximaDevolucion)
        {
            return new ResponseValiPrestamo()
            {
                //Type = "C",
                //StatusCode = StatusCode,
                id = Id,
                isbn = Isbn,
                identificaciónUsuario = IdentificaciónUsuario,
                tipoUsuario = TipoUsuario,
                fechaMaximaDevolucion = FechaMaximaDevolucion.ToString("dd/MM/yyyy")

            };
        }

        public static List<ModelErrors> GetModelStateErrors(ModelStateDictionary Model)
        {
            return Model.Select(x => new ModelErrors() { Type = "M", Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).ToList() }).ToList();
        }

    }

    public class ResponseObject
    {
        //public string Type { get; set; }
        //public int StatusCode { get; set; }
        public string mensaje { get; set; }

    }

    public class ResponseObjectRegis
    {
        public string id { get; set; }
        public string fechaMaximaDevolucion { get; set; }

    }
    public class ResponseValiPrestamo
    {
        public string id { get; set; }
        public string isbn { get; set; }
        public string identificaciónUsuario { get; set; }
        public int tipoUsuario { get; set; }
        public string fechaMaximaDevolucion { get; set; }

    }

    

    public class ModelErrors
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public List<string> Messages { get; set; }

    }

}
