using System.ComponentModel.DataAnnotations;

namespace Practica2.Web.Models;

public class MascotaModel
{
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Especie")]
    [Required(ErrorMessage = "La especie es obligatoria.")]
    [StringLength(100, ErrorMessage = "La especie no puede superar los 100 caracteres.")]
    public string Especie { get; set; } = string.Empty;

    [Display(Name = "Raza")]
    [Required(ErrorMessage = "La raza es obligatoria.")]
    [StringLength(100, ErrorMessage = "La raza no puede superar los 100 caracteres.")]
    public string Raza { get; set; } = string.Empty;

    [Display(Name = "Peso (kg)")]
    [Required(ErrorMessage = "El peso es obligatorio.")]
    [Range(0.01, 999999.99, ErrorMessage = "El peso debe ser mayor a 0.")]
    public decimal? Peso { get; set; }

    [Display(Name = "Cliente")]
    [Required(ErrorMessage = "Debe seleccionar un cliente.")]
    public long? IdCliente { get; set; }
}
