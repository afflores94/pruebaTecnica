using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica_ArielFlores.Context;
using PruebaTecnica_ArielFlores.Models.DTO;
using PruebaTecnica_ArielFlores.Models.Entidades;
using PruebaTecnica_ArielFlores.Utilidades;

namespace PruebaTecnica_ArielFlores.Controllers
{
    [ApiController]
    [Route("api/prueba")]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GenerarAleatorio _generarAleatorio;

        public HomeController(AppDbContext context, GenerarAleatorio generarAleatorio)
        {
            _context = context;
            _generarAleatorio = generarAleatorio;
        }

        // Obtener clientes
        [HttpGet("clientes/getClientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var cliente = await _context.Clientes
                    .Select(c => new ClienteDTO
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        FechaNacimiento = c.FechaNacimiento,
                        Sexo = c.Sexo,
                        Ingresos = c.Ingresos
                    })
                    .ToListAsync();

                if (!cliente.Any())
                {
                    return Ok(ApiResponseHelper.NotFound<object>("No hay registros."));
                }

                return Ok(ApiResponseHelper.Success(cliente));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Crear cliente
        [HttpPost("clientes/createCliente")]
        public async Task<IActionResult> CreateCliente([FromBody] ClientesCreateDTO req)
        {
            try
            {
                var nuevoCliente = new Clientes
                {
                    Nombre = req.Nombre,
                    FechaNacimiento = req.FechaNacimiento,
                    Sexo = req.Sexo,
                    Ingresos = req.Ingresos
                };

                _context.Clientes.Add(nuevoCliente);
                await _context.SaveChangesAsync();

                var cliente = new ClienteDTO
                {
                    Id = nuevoCliente.Id,
                    Nombre = nuevoCliente.Nombre,
                    FechaNacimiento = nuevoCliente.FechaNacimiento,
                    Sexo = nuevoCliente.Sexo,
                    Ingresos = nuevoCliente.Ingresos
                };

                return Ok(ApiResponseHelper.Success(cliente));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Obtener cuentas
        [HttpGet("cuentas/getCuentas")]
        public async Task<IActionResult> GetCuentas()
        {
            try
            {
                var cuenta = await _context.Cuentas
                    .Include(c => c.Cliente) // Incluye la relación
                    .Select(c => new CuentaDTO
                    {
                        Id = c.Id,
                        NumeroCuenta = c.NumeroCuenta,
                        Saldo = c.Saldo,
                        Cliente = new ClienteDTO
                        {
                            Id = c.Cliente.Id,
                            Nombre = c.Cliente.Nombre,
                            FechaNacimiento = c.Cliente.FechaNacimiento,
                            Sexo = c.Cliente.Sexo,
                            Ingresos = c.Cliente.Ingresos
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

        // Crear cuenta
        [HttpPost("cuentas/createCuenta")]
        public async Task<IActionResult> CreateCuenta([FromBody] CuentasCreateDTO req)
        {
            try
            {
                if (req.Saldo < 150)
                {
                    return Ok(ApiResponseHelper.BadRequest<object>("La cuenta debe aperturarse con un saldo inicial mínimo de 150 HNL."));
                }

                var cuenta = new Cuentas
                {
                    ClientesId = req.ClientesId,
                    Saldo = req.Saldo,
                    NumeroCuenta = await _generarAleatorio.GenerarNumeroCuentaUnico()
                };

                _context.Cuentas.Add(cuenta);
                await _context.SaveChangesAsync();

                var cuentaConCliente = await _context.Cuentas
                    .Include(c => c.Cliente)
                    .FirstOrDefaultAsync(c => c.Id == cuenta.Id);

                if (cuentaConCliente == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Error al obtener la cuenta creada."));
                }

                var response = new CuentaDTO
                {
                    Id = cuentaConCliente.Id,
                    NumeroCuenta = cuentaConCliente.NumeroCuenta,
                    Saldo = cuentaConCliente.Saldo,
                    Cliente = new ClienteDTO
                    {
                        Id = cuentaConCliente.Cliente.Id,
                        Nombre = cuentaConCliente.Cliente.Nombre,
                        FechaNacimiento = cuentaConCliente.Cliente.FechaNacimiento,
                        Sexo = cuentaConCliente.Cliente.Sexo,
                        Ingresos = cuentaConCliente.Cliente.Ingresos
                    }
                };

                return Ok(ApiResponseHelper.Success(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Obtener saldo por número de cuenta
        [HttpPost("cuentas/getSaldoByCuenta")]
        public async Task<IActionResult> GetSaldoByCuenta([FromBody] SaldoByCuentaDTO req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.NumeroCuenta))
                {
                    return Ok(ApiResponseHelper.BadRequest<object>("El número de cuenta es requerido."));
                }

                var data = await _context.Cuentas
                    .Where(c => c.NumeroCuenta == req.NumeroCuenta)
                    .Select(c => new CuentaSaldoDTO
                    {
                        NumeroCuenta = c.NumeroCuenta,
                        Saldo = c.Saldo
                    })
                    .FirstOrDefaultAsync();

                if (data == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Registro no encontrado."));
                }

                return Ok(ApiResponseHelper.Success(data));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Crear transacción
        [HttpPost("transacciones/createTransaccion")]
        public async Task<IActionResult> CreateTransaccion([FromBody] TransaccionesCreateDTO req)
        {
            try
            {
                var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.Id == req.CuentasId);
                if (cuenta == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Cuenta no encontrada."));
                }

                if (req.Monto <= 0)
                {
                    return Ok(ApiResponseHelper.BadRequest<object>("El monto debe ser mayor a 0."));
                }

                decimal saldoAnterior = cuenta.Saldo;
                decimal saldoNuevo = saldoAnterior;

                if (req.TipoTransaccion.ToLower() == "deposito")
                {
                    saldoNuevo += req.Monto;
                }
                else if (req.TipoTransaccion.ToLower() == "retiro")
                {
                    if (req.Monto > saldoAnterior)
                    {
                        return Ok(ApiResponseHelper.BadRequest<object>("Saldo insuficiente en la cuenta."));
                    }
                    saldoNuevo -= req.Monto;
                }
                else
                {
                    return Ok(ApiResponseHelper.BadRequest<object>("Tipo de transacción inválido."));
                }

                // Crear y guardar la transacción
                var transaccion = new Transacciones
                {
                    TipoTransaccion = req.TipoTransaccion,
                    Monto = req.Monto,
                    SaldoAnterior = saldoAnterior,
                    SaldoNuevo = saldoNuevo,
                    Fecha = DateTime.Now,
                    CuentasId = cuenta.Id
                };

                // Actualizar saldo
                cuenta.Saldo = saldoNuevo;

                _context.Transacciones.Add(transaccion);
                await _context.SaveChangesAsync();

                return Ok(ApiResponseHelper.Success(new
                {
                    Id = transaccion.Id,
                    cuenta.NumeroCuenta,
                    transaccion.TipoTransaccion,
                    transaccion.Monto,
                    transaccion.SaldoAnterior,
                    transaccion.SaldoNuevo,
                    transaccion.Fecha
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        // Obtener historial transacciones por número de cuenta
        [HttpPost("transacciones/getResumenTransaccionesByCuenta")]
        public async Task<IActionResult> GetResumenTransaccionesByCuenta([FromBody] SaldoByCuentaDTO req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.NumeroCuenta))
                {
                    return Ok(ApiResponseHelper.BadRequest<object>("El número de cuenta es requerido."));
                }

                var cuenta = await _context.Cuentas
                    .Include(c => c.Transacciones)
                    .Include(c => c.Cliente)
                    .FirstOrDefaultAsync(c => c.NumeroCuenta == req.NumeroCuenta);

                if (cuenta == null)
                {
                    return Ok(ApiResponseHelper.NotFound<object>("Cuenta no encontrada."));
                }

                var resumen = new
                {
                    cuenta.NumeroCuenta,
                    cuenta.Saldo,
                    Cliente = new
                    {
                        cuenta.Cliente.Nombre,
                        cuenta.Cliente.FechaNacimiento,
                        cuenta.Cliente.Sexo,
                        cuenta.Cliente.Ingresos
                    },
                    Transacciones = cuenta.Transacciones
                    .OrderByDescending(t => t.Fecha)
                    .Select(t => new
                    {
                        t.Id,
                        t.TipoTransaccion,
                        t.Monto,
                        t.SaldoAnterior,
                        t.SaldoNuevo,
                        Fecha = t.Fecha.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                };

                return Ok(ApiResponseHelper.Success(resumen));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Error<object>(new { error = ex.Message }));
            }
        }

        
    }
}
