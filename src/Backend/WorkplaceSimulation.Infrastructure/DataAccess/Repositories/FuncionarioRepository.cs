using Dapper;
using WorkplaceSimulation.Domain.Entities;
using WorkplaceSimulation.Domain.Repositories.Funcionario;

namespace WorkplaceSimulation.Infrastructure.DataAccess.Repositories;

public class FuncionarioRepository : IFuncionarioReadRepository, IFuncionarioWriteRepository
{
    private readonly DbSession _session;
    public FuncionarioRepository(DbSession session)
    {
        _session = session;
    }

    public async Task AtualizarSalario(decimal NovoValorSalario, int FuncionarioId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NovoValorSalario", NovoValorSalario);
        parameters.Add("@FuncionarioId", FuncionarioId);

        await _session.Connection.ExecuteAsync(
            "pr_AtualizarSalario",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task ContratarAsync(string Nome, string Email, string Cargo, decimal Salario, string NomeDepartamento, DateTime DataAdmissao)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nome", Nome);
        parameters.Add("@Email", Email);
        parameters.Add("@Cargo", Cargo);
        parameters.Add("@Salario", Salario);
        parameters.Add("@NomeDepartamento", NomeDepartamento);
        parameters.Add("@DataAdmissao", DataAdmissao);

        await _session.Connection.ExecuteAsync(
            "pr_ContratarFuncionario",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);

    }

    public async Task DesativarAsync(int FuncionarioId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@FuncionarioId", FuncionarioId);

        await _session.Connection.ExecuteAsync(
            "pr_DesativarFuncionario",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task ReativarAsync(int FuncionarioId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@FuncionarioId", FuncionarioId);

        await _session.Connection.ExecuteAsync(
            "pr_ReativarFuncionario",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<Funcionario?> VisualizarAsync(int FuncionarioId)
    {
        const string query = @"
            SELECT F.Nome, F.Email, F.DataCriacao, D.NomeDepartamento
            FROM Funcionarios F INNER JOIN Departamentos D
            ON F.DepartamentoId = D.Id
            WHERE F.DepartamentoId = D.Id AND F.Id = @Id";

        var result = await _session.Connection.QueryAsync<Funcionario, Departamento, Funcionario>
            (
            query,
            (funcionario, departamento) =>
            {
                funcionario.Departamento = departamento;
                return funcionario;
            },
            new { Id = FuncionarioId },
            splitOn: "Id"
            );

        return result.FirstOrDefault();
    }
    public async Task<IEnumerable<Funcionario>> RelatorioDetalhadoAsync()
    {
        const string sql = "SELECT * FROM vw_RelatorioFuncionariosDetalhado";

        var funcionarioDict = new Dictionary<int, Funcionario>();

        await _session.Connection.QueryAsync<Funcionario, Departamento, DetalhesContrato, Projeto, Funcionario>(
            sql,
            (func, depto, contrato, projeto) =>
            {
                if (!funcionarioDict.TryGetValue(func.Id, out var funcionarioExistente))
                {
                    funcionarioExistente = func;
                    funcionarioExistente.Departamento = depto;
                    funcionarioExistente.Contrato = contrato;
                    funcionarioDict.Add(funcionarioExistente.Id, funcionarioExistente);
                }

                if (projeto != null)
                {
                    funcionarioExistente.Projetos.Add(projeto);
                }

                return funcionarioExistente;
            },
            // ATENÇÃO: Os nomes aqui devem ser IGUAIS aos aliases da View
            splitOn: "DeptoId, ContratoId, ProjetoId"
        );

        return funcionarioDict.Values;
    }
}
