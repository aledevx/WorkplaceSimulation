namespace WorkplaceSimulation.Domain.Entities;

public class DetalhesContrato
{
    public int FuncionarioId { get; set; }
    public decimal Salario { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public DateTime DataAdmissao { get; set; }

    // Referência de volta ao funcionário, se necessário
    public Funcionario? Funcionario { get; set; }
}
