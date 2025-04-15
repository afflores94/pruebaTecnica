using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.Entidades
{
    [Table("clientes")]
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(25)]
        public string Sexo { get; set; } = string.Empty;

        [Required]
        public decimal Ingresos { get; set; }

        public List<Cuentas> Cuentas { get; set; } = new();
    }
}
