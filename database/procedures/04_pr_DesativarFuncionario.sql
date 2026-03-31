CREATE PROCEDURE pr_DesativarFuncionario
    @FuncionarioId INT
AS
BEGIN
    IF NOT EXISTS(SELECT 1 FROM Funcionarios WHERE Id = @FuncionarioId AND Ativo = 1)
    BEGIN
        THROW 50001, N'Error: Funcionário não encontrado ou já está desativado.', 1;
    end

    BEGIN TRANSACTION
        BEGIN TRY
            UPDATE Funcionarios
            SET Ativo = 0
            WHERE Id = @FuncionarioId;

            COMMIT
            PRINT N'Funcionário desativado com sucesso!';
        END TRY
        BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
end
go

