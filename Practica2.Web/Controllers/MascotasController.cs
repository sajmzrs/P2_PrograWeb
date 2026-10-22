using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practica2.Web.Models;

namespace Practica2.Web.Controllers;

public class MascotasController : Controller
{
    private readonly HttpClient _http;

    public MascotasController(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("Api");
    }

    [HttpGet]
    public async Task<IActionResult> Registrar()
    {
        await CargarClientes();
        return View(new MascotaModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrar(MascotaModel model)
    {
        if (!ModelState.IsValid)
        {
            await CargarClientes();
            return View(model);
        }

        var respuesta = await _http.PostAsJsonAsync("api/Mascotas", model);
        var mensaje = await LeerMensaje(respuesta);

        if (!respuesta.IsSuccessStatusCode)
        {
            ViewBag.Error = mensaje ?? "No se pudo registrar la mascota.";
            await CargarClientes();
            return View(model);
        }

        TempData["Mensaje"] = mensaje;
        return RedirectToAction(nameof(Registrar));
    }

    [HttpGet]
    public async Task<IActionResult> Consultar()
    {
        var mascotas = await _http.GetFromJsonAsync<List<MascotaConsultaModel>>("api/Mascotas");
        return View(mascotas ?? new List<MascotaConsultaModel>());
    }

    private async Task CargarClientes()
    {
        var clientes = await _http.GetFromJsonAsync<List<ClienteModel>>("api/Clientes") ?? new List<ClienteModel>();
        ViewBag.Clientes = clientes
            .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = $"{c.Cedula} - {c.Nombre}" })
            .ToList();
    }

    private static async Task<string?> LeerMensaje(HttpResponseMessage respuesta)
    {
        try
        {
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaModel>();
            return string.IsNullOrWhiteSpace(resultado?.Mensaje) ? null : resultado.Mensaje;
        }
        catch
        {
            return null;
        }
    }
}
