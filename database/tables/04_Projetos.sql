create table dbo.Projetos
(
    Id          int identity
        primary key,
    NomeProjeto varchar(255) not null,
    Prazo       datetime     not null
)
go