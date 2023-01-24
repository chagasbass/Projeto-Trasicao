using Microsoft.Extensions.Options;
using ProjetoTransicao.Shared.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoTransicao.Infra.Data.DataContexts;

public class ReadDataContext : IDisposable
{
    private readonly BaseConfigurationOptions _baseConfigurationOptions;
    private IDbConnection _DbConnection;

    public ReadDataContext(IOptions<BaseConfigurationOptions> options)
    {
        _baseConfigurationOptions = options.Value;
    }

    public IDbConnection AbrirConexao()
    {
        if (_DbConnection is null || _DbConnection.State != ConnectionState.Open)
        {
            _DbConnection = new SqlConnection(_baseConfigurationOptions.StringConexaoBancoDeDados);
            _DbConnection.Open();
        }

        return _DbConnection;

    }

    public void Dispose()
    {
        if (_DbConnection != null && _DbConnection.State == ConnectionState.Open)
            _DbConnection.Dispose();
    }
}
