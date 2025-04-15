using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.Entidades
{
    [Table("cuentas")]
    public class Cuentas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(16)]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Required]
        public int ClientesId { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        [ForeignKey("ClientesId")]
        public Clientes Cliente { get; set; } = null!;

        public ICollection<Transacciones> Transacciones { get; set; } = new List<Transacciones>();
    }
}
