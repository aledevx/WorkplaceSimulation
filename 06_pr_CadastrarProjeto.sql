CREATE PROCEDURE pr_CadastrarProjeto
    @NomeProjeto NVARCHAR(100),
    @Prazo DATETIME
AS
BEGIN
    -- 1. VALIDAÇÕES

    -- Evita projetos duplicados
    IF EXISTS (SELECT 1 FROM Projetos WHERE NomeProjeto = @NomeProjeto)
    BEGIN
        THROW 50006, N'Erro: Já existe um projeto com este nome.', 1;
    END

    -- Regra de Negócio: O prazo deve ser no futuro
    IF @Prazo <= GETDATE()
    BEGIN
        THROW 50007, N'Erro: A data de prazo do projeto deve ser superior à data atual.', 1;
    END

    -- 2. OPERAÇÃO
    BEGIN TRANSACTION
        BEGIN TRY
            INSERT INTO Projetos (NomeProjeto, Prazo)
            VALUES (@NomeProjeto, @Prazo);

            COMMIT;
            PRINT N'Projeto cadastrado com sucesso!';
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK;
            THROW;
        END CATCH
END;
go