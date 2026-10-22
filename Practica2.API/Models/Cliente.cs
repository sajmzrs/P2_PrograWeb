using System.ComponentModel.DataAnnotations;

namespace Practica2.API.Models;

public class Cliente
{
    public long IdCliente { get; set; }

    [Required]
    [StringLength(50)]
    public string Cedula { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Correo { get; set; } = string.Empty;

    public bool Estado { get; set; }
}
