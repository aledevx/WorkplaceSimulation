create table dbo.Funcionarios
(
    Nome           varchar(100)                     not null,
    Email          varchar(150)                     not null
        constraint UQ_Email
            unique,
    DataCriacao    datetime
        constraint DF_DataCriacao default getdate() not null,
    Id             int identity
        primary key,
    DepartamentoId int                              not null
        constraint FK_Funcionarios_Departamentos
            references dbo.Departamentos,
    Ativo          bit
        constraint DF_Ativo default 1               not null
)
go