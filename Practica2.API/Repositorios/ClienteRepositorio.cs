using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Practica2.API.Models;

namespace Practica2.API.Repositorios;

public class ClienteRepositorio
{
    private readonly string _conexion;

    public ClienteRepositorio(IConfiguration configuration)
    {
        _conexion = configuration.GetConnectionString("Practica2")!;
    }

    public async Task<int> Registrar(Cliente cliente)
    {
        using var db = new SqlConnection(_conexion);
        return await db.QueryFirstAsync<int>("RegistrarCliente",
            new { cliente.Cedula, cliente.Nombre, cliente.Correo },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Cliente>> Consultar()
    {
        using var db = new SqlConnection(_conexion);
        return await db.QueryAsync<Cliente>("ConsultarClientes",
            commandType: CommandType.StoredProcedure);
    }
}
