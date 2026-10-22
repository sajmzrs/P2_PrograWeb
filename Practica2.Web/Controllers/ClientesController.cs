using Microsoft.AspNetCore.Mvc;
using Practica2.Web.Models;

namespace Practica2.Web.Controllers;

public class ClientesController : Controller
{
    private readonly HttpClient _http;

    public ClientesController(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("Api");
    }

    [HttpGet]
    public IActionResult Registrar()
    {
        return View(new ClienteModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrar(ClienteModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var respuesta = await _http.PostAsJsonAsync("api/Clientes", model);
        var mensaje = await LeerMensaje(respuesta);

        if (!respuesta.IsSuccessStatusCode)
        {
            ViewBag.Error = mensaje ?? "No se pudo registrar el cliente.";
            return View(model);
        }

        TempData["Mensaje"] = mensaje;
        return RedirectToAction(nameof(Registrar));
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
