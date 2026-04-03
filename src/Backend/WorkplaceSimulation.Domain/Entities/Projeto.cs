namespace WorkplaceSimulation.Domain.Entities;

public class Projeto
{
    public int Id { get; set; }
    public string NomeProjeto { get; set; } = string.Empty;
    public DateTime Prazo { get; set; }

    // Relação N:N - O Dapper preencherá isso via JOIN na tabela FuncionariosProjetos
    public List<Funcionario> Funcionarios { get; set; } = new();
}
