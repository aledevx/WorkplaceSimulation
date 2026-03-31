CREATE PROCEDURE pr_AtualizarSalario
    @NovoValorSalario DECIMAL(10,2), @FuncionarioId INT

AS BEGIN

    IF NOT EXISTS( SELECT 1 FROM Funcionarios WHERE Id = @FuncionarioId AND Ativo = 1)
    BEGIN
        THROW  50001, N'Error: O funcionário não existe ou não está ativo.', 1;
    end

    IF NOT EXISTS( SELECT 1 FROM DetalhesContrato WHERE FuncionarioId = @FuncionarioId)
    BEGIN
        THROW 50001, N'Error: O funcionário fornecido não possui contrato.', 1;
    end

    DECLARE @SalarioAtual DECIMAL(10,2);

    SELECT @SalarioAtual = Salario FROM DetalhesContrato WHERE FuncionarioId = @FuncionarioId;

    IF @SalarioAtual >= @NovoValorSalario
    BEGIN
        THROW 50003, N'Error: O novo valor infringe o principio de irredutibilidade salarial (Art. 7, VI, CF).', 3;
    end

    IF (@NovoValorSalario - @SalarioAtual) > (@SalarioAtual/2)
    BEGIN
        THROW 50004, N'Error: O aumento salarial não pode ultrapassar 50% do valor do salário atual.', 4;
    end

    BEGIN TRANSACTION
        BEGIN TRY
        UPDATE DetalhesContrato
        SET Salario = @NovoValorSalario
        WHERE FuncionarioId = @FuncionarioId;

        COMMIT;
        END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
end
go

