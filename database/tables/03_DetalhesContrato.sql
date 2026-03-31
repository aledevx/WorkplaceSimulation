create table dbo.DetalhesContrato
(
    Id            int identity
        primary key,
    FuncionarioId int          not null
        unique
        constraint FK_DetalhesContrato_Funcionarios
            references dbo.Funcionarios,
    Salario       decimal(10, 2),
    Cargo         varchar(150) not null,
    DataAdmissao  datetime     not null
)
go