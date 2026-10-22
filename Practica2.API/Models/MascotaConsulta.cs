namespace Practica2.API.Models;

public class MascotaConsulta
{
    public string CedulaCliente { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public decimal Peso { get; set; }
}
