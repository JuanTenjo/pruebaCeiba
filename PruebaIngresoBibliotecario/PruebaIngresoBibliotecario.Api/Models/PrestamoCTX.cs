using Microsoft.EntityFrameworkCore;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class PrestamoCTX:DbContext
    {
        public PrestamoCTX(DbContextOptions<PrestamoCTX> options): base (options)
        {

        }

        public DbSet<Prestamo> Prestamo { get; set; }


    }
}