namespace WorkplaceSimulation.Domain.Repositories.Funcionario;

public interface IFuncionarioWriteRepository
{
    Task ContratarAsync(string Nome,
        string Email,
        string Cargo, 
        decimal Salario, 
        string NomeDepartamento, 
        DateTime DataAdmissao);
    Task DesativarAsync(int FuncionarioId);
    Task ReativarAsync(int FuncionarioId);
    Task AtualizarSalario(decimal NovoValorSalario, int FuncionarioId);

}
