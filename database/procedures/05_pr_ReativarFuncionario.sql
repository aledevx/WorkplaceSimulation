CREATE PROCEDURE pr_ReativarFuncionario
    @FuncionarioId INT
AS
BEGIN
    -- 1. VALIDAÇÕES

    -- Verifica se o ID existe na base
    IF NOT EXISTS(SELECT 1 FROM Funcionarios WHERE Id = @FuncionarioId)
    BEGIN
        THROW 50001, N'Erro: O ID do funcionário informado não existe.', 1;
    END

    -- Verifica se o funcionário já está ativo (evita updates desnecessários)
    IF EXISTS(SELECT 1 FROM Funcionarios WHERE Id = @FuncionarioId AND Ativo = 1)
    BEGIN
        THROW 50002, N'Erro: Este funcionário já se encontra ativo no sistema.', 1;
    END

    -- 2. OPERAÇÃO
    BEGIN TRANSACTION
        BEGIN TRY
            UPDATE Funcionarios
            SET Ativo = 1
            WHERE Id = @FuncionarioId;

            COMMIT;
            PRINT N'Funcionário reativado com sucesso!';
        END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;
go

