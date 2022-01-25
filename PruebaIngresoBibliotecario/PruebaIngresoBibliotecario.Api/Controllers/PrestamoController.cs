using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaIngresoBibliotecario.Api.Models;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Api.Helpers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class prestamoController : ControllerBase
    {

        private readonly PrestamoCTX _context;


        public prestamoController(PrestamoCTX context)
        {
            _context = context;
        }


        // GET: api/<PrestamoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamo()
        {
            return await _context.Prestamo.ToListAsync();
        }


        // GET api/<PrestamoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(string id)
        {

            var prestamo = await _context.Prestamo.FindAsync(id);

            if(prestamo == null)
            {
                return NotFound(ErrorHelper.Response(404, $"El prestamo con id {id} no existe"));
            }

            return Ok(ErrorHelper.ResponseValiPrestamo(prestamo.isbn, prestamo.isbn, prestamo.identificaionUsuario, prestamo.tipoUsuario, prestamo.fechaMaximaDevolucion));

        }


        // POST api/<PrestamoController>
        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(400);
            }

            var UsuarioPrestamo = await _context.Prestamo.Where(x => x.identificaionUsuario == prestamo.identificaionUsuario).AnyAsync();

            if (UsuarioPrestamo && prestamo.tipoUsuario == 3)
            {
                   return BadRequest(ErrorHelper.Response(400, $"El usuario con identificacion {prestamo.identificaionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo"));
            }
            else
            {
                switch (prestamo.tipoUsuario)
                {
                    case 1: //Afiliado
                        prestamo.fechaMaximaDevolucion = CalcularFechaEntrega(DateTime.Now,10);
                        break;
                    case 2://Usuario o Empleado
                        prestamo.fechaMaximaDevolucion = CalcularFechaEntrega(DateTime.Now, 8);
                        break;
                    case 3://Invitado
                        prestamo.fechaMaximaDevolucion = CalcularFechaEntrega(DateTime.Now,7);
                        break;
                    default:
                        break;
                }




                var guid = Guid.NewGuid();


                prestamo.isbn = guid.ToString();

                _context.Prestamo.Add(prestamo);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetPrestamo", new { id = prestamo.id_prestamo }, prestamo);
                return Ok(ErrorHelper.ResponseRegis(prestamo.isbn,prestamo.fechaMaximaDevolucion));
            }
        }

        // PUT api/<PrestamoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPrestamo(string id_prestamo, Prestamo prestamo)
        {
            if(id_prestamo != prestamo.isbn)
            {
                return BadRequest();
            }

            _context.Entry(prestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if(!PrestamoExists(id_prestamo))
                {
                    return NotFound();
                }else
                {
                    throw;
                }

                
            }

            return NoContent();


        }


        // DELETE api/<PrestamoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prestamo>> DeletePrestamo(int id_prestamo)
        {
            var prestamo = await _context.Prestamo.FindAsync(id_prestamo);
            
            if(prestamo == null)
            {
                return NotFound();
            }

            _context.Prestamo.Remove(prestamo);
            await _context.SaveChangesAsync();

            return prestamo;

        }

        private bool PrestamoExists(string id_prestamo)
        {

            return _context.Prestamo.Any(e => e.isbn == id_prestamo);

        }

        static DateTime CalcularFechaEntrega(DateTime fechaPrestamo, int dias)
        {
            DateTime dt = fechaPrestamo;

            for (int x = 0; x < dias; x++)
            {
                
                while (dt.DayOfWeek == DayOfWeek.Saturday) dt = dt.AddDays(2);

                dt = dt.AddDays(1);
            }

            return dt;
        }


    }
}
