namespace WorkplaceSimulation.Domain.Repositories.Departamento;

public interface IDepartamentoReadRepository
{
    Task<IEnumerable<Entities.Departamento>> ListarTodosAsync();
}
