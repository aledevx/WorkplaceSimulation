CREATE VIEW vw_RelatorioProjetosDetalhado AS
SELECT
    P.NomeProjeto,
    P.Prazo,
    F.Nome AS NomeFuncionario,
    C.Cargo,
    D.NomeDepartamento AS Departamento
FROM Projetos P
INNER JOIN FuncionariosProjetos FP ON P.Id = FP.ProjetoId
INNER JOIN Funcionarios F ON FP.FuncionarioId = F.Id
INNER JOIN DetalhesContrato C ON F.Id = C.FuncionarioId
INNER JOIN Departamentos D ON F.DepartamentoId = D.Id
go