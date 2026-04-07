namespace WorkplaceSimulation.Domain.Repositories.Projeto;

public interface IProjetoReadRepository
{
    Task<IEnumerable<Entities.Projeto>> ListarRelatorioDetalhadoAsync();
}
