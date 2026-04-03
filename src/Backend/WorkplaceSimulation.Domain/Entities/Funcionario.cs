namespace WorkplaceSimulation.Domain.Entities;
public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public int DepartamentoId { get; set; }

    // Propriedades de Navegação (Dapper Multi-Mapping)
    public Departamento? Departamento { get; set; }
    public DetalhesContrato? Contrato { get; set; }
    public List<Projeto> Projetos { get; set; } = new();
}

