using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Amonet.Infrastructure.Persistence;

public class FabricaConexionSql : IFabricaConexionSql
{
    private readonly string _connectionString;

    public FabricaConexionSql(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en la configuración.");
    }

    public IDbConnection CrearConexion()
    {
        return new SqlConnection(_connectionString);
    }
}

