using Microsoft.EntityFrameworkCore;
using PruebaTecnica_ArielFlores.Context;

namespace PruebaTecnica_ArielFlores.Utilidades
{
    public class GenerarAleatorio
    {
        private readonly AppDbContext _context;

        public GenerarAleatorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarNumeroCuentaUnico()
        {
            string numeroCuenta;
            var random = new Random();

            do
            {
                numeroCuenta = "";
                for (int i = 0; i < 16; i++)
                {
                    numeroCuenta += random.Next(0, 10).ToString();
                }
            }
            while (await _context.Cuentas.AnyAsync(c => c.NumeroCuenta == numeroCuenta));

            return numeroCuenta;
        }
    }
}
