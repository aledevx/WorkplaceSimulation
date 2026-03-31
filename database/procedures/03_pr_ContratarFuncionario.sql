CREATE PROCEDURE pr_ContratarFuncionario
    @Nome VARCHAR(100), @Email VARCHAR(150), @Cargo VARCHAR(150), @Salario DECIMAL(10,2), @NomeDepartamento VARCHAR(150), @DataAdmissao DATETIME
AS
BEGIN
    BEGIN TRANSACTION;

		DECLARE @DepartamentoId INT;

		SELECT @DepartamentoId = Id FROM Departamentos WHERE Departamentos.NomeDepartamento = @NomeDepartamento;

		IF EXISTS (SELECT 1 FROM Funcionarios WHERE Email = @Email)
		BEGIN
			THROW 50002, 'Erro: E-mail já cadastrado.', 1;
		END

	    IF @DepartamentoId IS NULL
		BEGIN
			THROW 50002, 'Erro: Departamento não encontrado.', 1;
		END


    BEGIN TRY
        INSERT INTO Funcionarios (Nome, Email, DepartamentoId) VALUES (@Nome, @Email, @DepartamentoId);
        DECLARE @NovoId INT = SCOPE_IDENTITY();

        INSERT INTO DetalhesContrato (FuncionarioId, Cargo, Salario, DataAdmissao)
        VALUES (@NovoId, @Cargo, @Salario, @DataAdmissao);

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END;
go

