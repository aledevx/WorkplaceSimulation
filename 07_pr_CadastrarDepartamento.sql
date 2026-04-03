CREATE PROCEDURE pr_CadastrarDepartamento
    @NomeDepartamento NVARCHAR(100)
AS
BEGIN
    -- 1. VALIDAÇÃO: Evita departamentos com nomes idênticos
    IF EXISTS (SELECT 1 FROM Departamentos WHERE NomeDepartamento = @NomeDepartamento)
    BEGIN
        THROW 50005, N'Erro: Já existe um departamento cadastrado com este nome.', 1;
    END

    -- 2. OPERAÇÃO
    BEGIN TRANSACTION
        BEGIN TRY
            INSERT INTO Departamentos (NomeDepartamento)
            VALUES (@NomeDepartamento);

            COMMIT;
            PRINT N'Departamento cadastrado com sucesso!';
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK;
            THROW;
        END CATCH
END;
go