namespace WorkplaceSimulation.Domain.Repositories.Departamento;

public interface IDepartamentoWriteRepository
{
    Task<bool> CadastrarAsync(string nomeDepartamento);
}
