using Microsoft.AspNetCore.Mvc;
using Practica2.API.Models;
using Practica2.API.Repositorios;

namespace Practica2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteRepositorio _repositorio;

    public ClientesController(ClienteRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> Consultar()
    {
        var clientes = await _repositorio.Consultar();
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(Cliente cliente)
    {
        cliente.Cedula = cliente.Cedula.Trim();
        cliente.Nombre = cliente.Nombre.Trim();
        cliente.Correo = cliente.Correo.Trim();

        var resultado = await _repositorio.Registrar(cliente);

        if (resultado == 0)
            return BadRequest(new Respuesta { Exito = false, Mensaje = "Ya existe un cliente registrado con esa cédula." });

        return Ok(new Respuesta { Exito = true, Mensaje = "Cliente registrado correctamente." });
    }
}
