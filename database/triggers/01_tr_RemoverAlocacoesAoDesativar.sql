CREATE TRIGGER tr_RemoverAlocacoesAoDesativar
ON Funcionarios
AFTER UPDATE
AS
BEGIN

    IF UPDATE(Ativo)
    BEGIN

        DELETE FROM FuncionariosProjetos
        WHERE FuncionarioId IN (
            SELECT i.Id
            FROM inserted i
            INNER JOIN deleted d ON i.Id = d.Id
            WHERE i.Ativo = 0 AND d.Ativo = 1
        );
    END
END;
go

