using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Practica2.API.Models;

namespace Practica2.API.Repositorios;

public class MascotaRepositorio
{
    private readonly string _conexion;

    public MascotaRepositorio(IConfiguration configuration)
    {
        _conexion = configuration.GetConnectionString("Practica2")!;
    }

    public async Task<int> Registrar(Mascota mascota)
    {
        using var db = new SqlConnection(_conexion);
        return await db.QueryFirstAsync<int>("RegistrarMascota",
            new { mascota.Nombre, mascota.Especie, mascota.Raza, mascota.Peso, mascota.IdCliente },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MascotaConsulta>> Consultar()
    {
        using var db = new SqlConnection(_conexion);
        return await db.QueryAsync<MascotaConsulta>("ConsultarMascotas",
            commandType: CommandType.StoredProcedure);
    }
}
