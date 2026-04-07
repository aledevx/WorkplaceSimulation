namespace WorkplaceSimulation.Domain.Repositories.Funcionario;

public interface IFuncionarioReadRepository
{
    Task<Entities.Funcionario?> VisualizarAsync(int FuncionarioId);
}
