namespace WorkplaceSimulation.Domain.Repositories.Funcionario;

public interface IFuncionarioReadRepository
{
    Task<Entities.Funcionario?> VisualizarAsync(int FuncionarioId);
    Task<IEnumerable<Entities.Funcionario>> RelatorioDetalhadoAsync();
}
