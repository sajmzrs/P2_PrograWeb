using System.ComponentModel.DataAnnotations;

namespace Practica2.Web.Models;

public class ClienteModel
{
    public long IdCliente { get; set; }

    [Display(Name = "Cédula")]
    [Required(ErrorMessage = "La cédula es obligatoria.")]
    [StringLength(50, ErrorMessage = "La cédula no puede superar los 50 caracteres.")]
    public string Cedula { get; set; } = string.Empty;

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Correo electrónico")]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    public string Correo { get; set; } = string.Empty;
}
