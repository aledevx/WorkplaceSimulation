CREATE VIEW vw_RelatorioFuncionariosDetalhado AS
SELECT
    F.Nome,
    C.Cargo,
    C.Salario,
    D.NomeDepartamento AS Departamento,
    P.NomeProjeto,
    P.Prazo
FROM Projetos P
INNER JOIN FuncionariosProjetos FP ON P.Id = FP.ProjetoId
INNER JOIN Funcionarios F ON FP.FuncionarioId = F.Id
INNER JOIN DetalhesContrato C ON F.Id = C.FuncionarioId
INNER JOIN Departamentos D ON F.DepartamentoId = D.Id
go