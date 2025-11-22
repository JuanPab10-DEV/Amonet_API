using System.Data;

namespace Amonet.Infrastructure.Persistence;

public interface IFabricaConexionSql
{
    IDbConnection CrearConexion();
}

