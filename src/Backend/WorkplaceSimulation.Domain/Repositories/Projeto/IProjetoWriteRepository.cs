namespace WorkplaceSimulation.Domain.Repositories.Projeto;

public interface IProjetoWriteRepository
{
    Task CadastrarAsync(string NomeProjeto, DateTime Prazo);
}
