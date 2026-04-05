using Dapper;
using System.Data;
using WorkplaceSimulation.Domain.Entities;
using WorkplaceSimulation.Domain.Repositories.Departamento;

namespace WorkplaceSimulation.Infrastructure.DataAccess.Repositories;

public class DepartamentoRepository : IDepartamentoReadRepository, IDepartamentoWriteRepository
{
    private readonly DbSession _session;
    public DepartamentoRepository(DbSession session)
    {
        _session = session;
    }
    public async Task<bool> CadastrarAsync(string nomeDepartamento)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NomeDepartamento", nomeDepartamento);

        await _session.Connection.ExecuteAsync(
            "pr_CadastrarDepartamento", 
            parameters, 
            commandType: CommandType.StoredProcedure);

        return true;

    }

    public async Task<IEnumerable<Departamento>> ListarTodosAsync()
    {
        const string query = "SELECT Id, NomeDepartamento FROM Departamento ORDER BY NomeDepartamento";

        return await _session.Connection.QueryAsync<Departamento>(query);
    }
}
