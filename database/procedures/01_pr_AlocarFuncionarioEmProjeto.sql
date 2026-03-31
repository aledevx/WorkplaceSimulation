CREATE PROCEDURE pr_AlocarFuncionarioEmProjeto
    @FuncionarioId INT,
    @ProjetoId INT
AS
BEGIN
    -- 1. VALIDAÇÕES (Leitura - Sem abrir transação ainda)

    -- Verifica se o funcionário existe e está ativo
    IF NOT EXISTS (SELECT 1 FROM Funcionarios WHERE Id = @FuncionarioId AND Ativo = 1)
    BEGIN
        THROW 50001, 'Erro: Funcionário não encontrado ou está desativado.', 1;
    END

    -- Verifica se o projeto existe
    IF NOT EXISTS (SELECT 1 FROM Projetos WHERE Id = @ProjetoId)
    BEGIN
        THROW 50001, 'Erro: Projeto não encontrado.', 1;
    END

    -- Verifica se o vínculo já existe (Evita duplicidade)
    IF EXISTS (SELECT 1 FROM FuncionariosProjetos WHERE FuncionarioId = @FuncionarioId AND ProjetoId = @ProjetoId)
    BEGIN
        THROW 50001, 'Erro: O funcionário já está alocado neste projeto.', 1;
    END

    -- Verifica o prazo específico DO projeto informado
    IF EXISTS (SELECT 1 FROM Projetos WHERE Id = @ProjetoId AND Prazo < GETDATE())
    BEGIN
        THROW 50001, 'Erro: O prazo deste projeto já expirou.', 1;
    END

    -- 2. OPERAÇÃO (Escrita - Aqui abrimos a transação)

    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO FuncionariosProjetos (FuncionarioId, ProjetoId)
        VALUES (@FuncionarioId, @ProjetoId);

        COMMIT;
        PRINT 'Funcionário alocado com sucesso!';
    END TRY
    BEGIN CATCH
        -- Se algo falhar no INSERT, desfazemos apenas o que a transação abriu
        IF @@TRANCOUNT > 0 ROLLBACK;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
go

