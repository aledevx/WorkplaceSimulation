using Dapper;
using WorkplaceSimulation.Domain.Entities;
using WorkplaceSimulation.Domain.Repositories.Projeto;

namespace WorkplaceSimulation.Infrastructure.DataAccess.Repositories;

public class ProjetoRepository : IProjetoWriteRepository, IProjetoReadRepository
{
    private readonly DbSession _session;
    public ProjetoRepository(DbSession session)
    {
        _session = session;
    }
    public async Task CadastrarAsync(string NomeProjeto, DateTime Prazo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NomeProjeto", NomeProjeto);
        parameters.Add("@Prazo", Prazo);

        await _session.Connection.ExecuteAsync(
            "pr_CadastrarProjeto",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Projeto>> ListarRelatorioDetalhadoAsync()
    {
        // Importante: Adicionei os IDs no SELECT para o Dapper conseguir mapear
        const string sql = @"
        SELECT 
            P.Id, P.NomeProjeto, P.Prazo,                 -- Objeto Projeto
            F.Id, F.Nome,                                 -- Objeto Funcionario
            D.Id, D.NomeDepartamento,                     -- Objeto Departamento
            C.FuncionarioId, C.Cargo                      -- Objeto DetalhesContrato
        FROM Projetos P
        INNER JOIN FuncionariosProjetos FP ON P.Id = FP.ProjetoId
        INNER JOIN Funcionarios F ON FP.FuncionarioId = F.Id
        INNER JOIN DetalhesContrato C ON F.Id = C.FuncionarioId
        INNER JOIN Departamentos D ON F.DepartamentoId = D.Id";

        var projetoDict = new Dictionary<int, Projeto>();

        await _session.Connection.QueryAsync<Projeto, Funcionario, Departamento, DetalhesContrato, Projeto>(
            sql,
            (projeto, funcionario, departamento, contrato) =>
            {
                // Verifica se o projeto já foi criado no dicionário
                if (!projetoDict.TryGetValue(projeto.Id, out var projetoExistente))
                {
                    projetoExistente = projeto;
                    projetoDict.Add(projetoExistente.Id, projetoExistente);
                }

                // Monta o funcionário com seu departamento e contrato sem listar os projetos
                funcionario.Departamento = departamento;
                funcionario.Contrato = contrato;

                // Adiciona o funcionário à lista do projeto
                projetoExistente.Funcionarios.Add(funcionario);

                return projetoExistente;
            },
            splitOn: "Id, Id, FuncionarioId" // Onde a "tesoura" corta para cada objeto
        );

        return projetoDict.Values;
    }
}
