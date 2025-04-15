using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.Entidades
{
    public class Transacciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string TipoTransaccion { get; set; } = string.Empty;
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public decimal SaldoAnterior { get; set; }
        [Required]
        public decimal SaldoNuevo { get; set; }
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Required]
        public int CuentasId { get; set; }
        [ForeignKey("CuentasId")]
        public Cuentas Cuenta { get; set; } = null!;
    }
}
