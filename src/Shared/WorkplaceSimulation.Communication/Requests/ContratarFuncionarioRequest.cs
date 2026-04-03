namespace WorkplaceSimulation.Communication.Requests;

public record ContratarFuncionarioRequest(
    string Nome, 
    string Email, 
    string Cargo, 
    decimal Salario, 
    string Departamento, 
    DateTime DataAdmissao);
