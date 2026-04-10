CREATE VIEW vw_RelatorioProjetosDetalhado AS
SELECT
    P.Id AS Id, -- ID do Projeto (Raiz)
    P.NomeProjeto,
    P.Prazo,
    F.Id AS FuncId, -- Marcador de início do Funcionário
    F.Nome AS NomeFuncionario,
    C.FuncionarioId AS ContratoId, -- Marcador de início do Contrato
    C.Cargo,
    D.Id AS DeptoId, -- Marcador de início do Departamento
    D.NomeDepartamento AS Departamento
FROM Projetos P
INNER JOIN FuncionariosProjetos FP ON P.Id = FP.ProjetoId
INNER JOIN Funcionarios F ON FP.FuncionarioId = F.Id
INNER JOIN DetalhesContrato C ON F.Id = C.FuncionarioId
INNER JOIN Departamentos D ON F.DepartamentoId = D.Id
go