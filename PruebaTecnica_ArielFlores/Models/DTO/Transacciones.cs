using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.DTO
{
    public class TransaccionesDTO
    {
        public int Id { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty; // "Deposito" o "Retiro"
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }
        public DateTime Fecha { get; set; }
        public CuentasClienteDTO? Cuenta { get; set; }
    }

    public class TransaccionesCreateDTO
    {
        [Required(ErrorMessage = "El ID de cuenta es obligatorio.")]
        public int CuentasId { get; set; }
        [Required(ErrorMessage = "El tipo de transacción es obligatorio.")]
        public string TipoTransaccion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }
    }
}
