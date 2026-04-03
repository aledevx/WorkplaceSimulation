namespace WorkplaceSimulation.Domain.Entities;
public class Departamento
{
    public int Id { get; set; }
    public string NomeDepartamento { get; set; } = string.Empty;

    // Opcional: Lista de funcionários se precisar listar quem está no depto
    public List<Funcionario> Funcionarios { get; set; } = new();
}
