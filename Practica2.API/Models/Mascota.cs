using System.ComponentModel.DataAnnotations;

namespace Practica2.API.Models;

public class Mascota
{
    public long IdMascota { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Especie { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Raza { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 999999.99)]
    public decimal? Peso { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public long? IdCliente { get; set; }
}
