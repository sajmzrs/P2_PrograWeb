using Microsoft.AspNetCore.Mvc;
using Practica2.API.Models;
using Practica2.API.Repositorios;

namespace Practica2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MascotasController : ControllerBase
{
    private readonly MascotaRepositorio _repositorio;

    public MascotasController(MascotaRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> Consultar()
    {
        var mascotas = await _repositorio.Consultar();
        return Ok(mascotas);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(Mascota mascota)
    {
        mascota.Nombre = mascota.Nombre.Trim();
        mascota.Especie = mascota.Especie.Trim();
        mascota.Raza = mascota.Raza.Trim();

        var resultado = await _repositorio.Registrar(mascota);

        if (resultado == -1)
            return BadRequest(new Respuesta { Exito = false, Mensaje = "El cliente seleccionado no existe." });

        if (resultado == 0)
            return BadRequest(new Respuesta { Exito = false, Mensaje = $"No se pueden registrar más de 2 mascotas de la especie {mascota.Especie}." });

        return Ok(new Respuesta { Exito = true, Mensaje = "Mascota registrada correctamente." });
    }
}
