using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.DTO
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public ClienteDTO? Cliente { get; set; }
    }

    public class CuentasClienteDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }

    public class CuentasCreateDTO
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClientesId { get; set; }
        [Required(ErrorMessage = "El saldo inicial es obligatorio.")]
        [Range(150, double.MaxValue, ErrorMessage = "La cuenta debe aperturarse con un saldo inicial mínimo de 150 HNL.")]
        public decimal Saldo { get; set; }
    }

    public class CuentaSaldoDTO
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }

    public class SaldoByCuentaDTO()
    {
        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        public string NumeroCuenta { get; set; } = string.Empty;
    }

}
