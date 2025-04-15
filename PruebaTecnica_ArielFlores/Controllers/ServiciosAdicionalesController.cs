using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica_ArielFlores.Context;
using PruebaTecnica_ArielFlores.Models.DTO;
using PruebaTecnica_ArielFlores.Models.Entidades;
using PruebaTecnica_ArielFlores.Utilidades;

namespace PruebaTecnica_ArielFlores.Controllers
{
    [ApiController]
    [Route("api/prueba/adicionales")]
    public class ServiciosAdicionalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciosAdicionalesController(AppDbContext context)
        {
            _context = context;
        }

        // Obtener cliente por Id
        [HttpPost("clientes/getClienteById")]
        public async Task<IActionResult> GetClienteById([FromBody] ClientesUpdateDTO req)
        {
            try
            {
                var cliente = await _context.Clientes
                    .Where(c => c.Id == req.Id)
                    .Select(c => new ClienteDTO
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        FechaNacimiento = c.FechaNacimiento,
                        Sexo = c.Sexo,
                        Ingresos = c.Ingresos
                    })
                    .FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Registro no encontrado."));
                }

                return Ok(ApiResponseHelper.Success(cliente));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Actualizar cliente
        [HttpPut("clientes/updateCliente")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClientesUpdateDTO req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponseHelper.BadRequest<object>("Datos inválidos."));
            }

            try
            {
                var data = await _context.Clientes.FindAsync(req.Id);
                if (data == null)
                {
                    return NotFound(ApiResponseHelper.NotFound<object>("Registro no encontrado."));
                }

                data.Nombre = req.Nombre;
                data.FechaNacimiento = req.FechaNacimiento;
                data.Sexo = req.Sexo;
                data.Ingresos = req.Ingresos;

                _context.Clientes.Update(data);
                await _context.SaveChangesAsync();

                return Ok(ApiResponseHelper.Success(new
                {
                    data.Id,
                    data.Nombre,
                    data.FechaNacimiento,
                    data.Sexo,
                    data.Ingresos
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Obtener las cuentas del cliente
        [HttpPost("clientes/getCuentasCliente")]
        public async Task<IActionResult> GetCuentasCliente([FromBody] ClienteByIdDTO req)
        {
            try
            {
                var clienteConCuentas = await _context.Clientes
                    .Where(c => c.Id == req.Id) 
                    .Include(c => c.Cuentas)
                    .FirstOrDefaultAsync();

                if (clienteConCuentas == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Cliente no encontrado."));
                }

                var clienteDTO = new ClienteCuentasDTO
                {
                    Id = clienteConCuentas.Id,
                    Nombre = clienteConCuentas.Nombre,
                    Cuentas = clienteConCuentas.Cuentas.Select(c => new CuentasClienteDTO
                    {
                        Id = c.Id,
                        NumeroCuenta = c.NumeroCuenta,
                        Saldo = c.Saldo
                    }).ToList()
                };

                return Ok(ApiResponseHelper.Success(clienteDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Obtener transacciones
        [HttpGet("transacciones/getTransacciones")]
        public async Task<IActionResult> GetTransacciones()
        {
            try
            {
                var cuenta = await _context.Transacciones
                    .Include(c => c.Cuenta)
                    .Select(c => new TransaccionesDTO
                    {
                        Id = c.Id,
                        TipoTransaccion = c.TipoTransaccion,
                        Monto = c.Monto,
                        SaldoAnterior = c.SaldoAnterior,
                        SaldoNuevo = c.SaldoNuevo,
                        Fecha = c.Fecha,
                        Cuenta = new CuentasClienteDTO
                        {
                            Id = c.Cuenta.Id,
                            NumeroCuenta = c.Cuenta.NumeroCuenta,
                            Saldo = c.Cuenta.Saldo,
                        }
                    })
                    .ToListAsync();

                if (!cuenta.Any())
                {
                    return Ok(ApiResponseHelper.NotFound<object>("No hay registros."));
                }

                return Ok(ApiResponseHelper.Success(cuenta));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

    }
}