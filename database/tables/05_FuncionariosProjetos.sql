create table dbo.FuncionariosProjetos
(
    FuncionarioId int not null
        constraint FK_FuncionariosProjetos_Funcionario
            references dbo.Funcionarios,
    ProjetoId     int not null
        constraint FK_FuncionariosProjetos_Projeto
            references dbo.Projetos
)
go