using PruebaTecnica_ArielFlores.Models.Entidades;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica_ArielFlores.Models.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public decimal Ingresos { get; set; }
    }

    public class ClienteCuentasDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public decimal Ingresos { get; set; }
        public List<CuentasClienteDTO> Cuentas { get; set; } = new();
    }

    public class ClientesCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La fecha es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El sexo es obligatorio.")]
        public string Sexo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ingreso es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto de ingresos debe ser mayor a 0.")]
        public decimal Ingresos { get; set; }
    }

    public class ClientesUpdateDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La fecha es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El sexo es obligatorio.")]
        public string Sexo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ingreso es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto de ingresos debe ser mayor a 0.")]
        public decimal Ingresos { get; set; }
    }

    public class ClienteByIdDTO()
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }
    }
}
