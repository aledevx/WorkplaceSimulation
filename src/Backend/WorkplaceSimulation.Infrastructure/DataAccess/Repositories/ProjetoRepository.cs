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
        const string sql = "SELECT * FROM vw_RelatorioProjetosDetalhado";

        var projetoDict = new Dictionary<int, Projeto>();

        // Ordem dos Tipos: <T1, T2, T3, T4, TReturn>
        await _session.Connection.QueryAsync<Projeto, Funcionario, DetalhesContrato, Departamento, Projeto>(
            sql,
            (proj, func, contrato, depto) =>
            {
                // 1. Gerencia o Agrupamento de Projetos
                if (!projetoDict.TryGetValue(proj.Id, out var projetoExistente))
                {
                    projetoExistente = proj;
                    projetoDict.Add(projetoExistente.Id, projetoExistente);
                }

                // 2. Monta a árvore hierárquica do Funcionário
                if (func != null)
                {
                    func.Contrato = contrato;
                    func.Departamento = depto;

                    // 3. Adiciona o funcionário completo à lista do projeto
                    projetoExistente.Funcionarios.Add(func);
                }

                return projetoExistente;
            },
            // Onde o Dapper deve "cortar" para cada classe
            splitOn: "FuncId, ContratoId, DeptoId"
        );

        return projetoDict.Values;
    }
}
