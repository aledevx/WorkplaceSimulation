#!/bin/bash

# 1. Inicia o SQL Server em background
/opt/mssql/bin/sqlservr &

# 2. Aguarda o motor do banco de dados estar pronto
echo "Aguardando o SQL Server iniciar (30s)..."
sleep 30s

# 3. Cria o Banco de Dados principal
echo "Criando o banco de dados 'WorkplaceDB'..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'WorkplaceDB') CREATE DATABASE WorkplaceDB;"

# 4. Função para executar os scripts modulares
execute_scripts() {
    local folder=$1
    echo "------------------------------------------"
    echo "📂 Executando scripts em: scripts/$folder"
    echo "------------------------------------------"
    
    # Percorre os arquivos .sql da pasta em ordem alfabética
    for f in $(ls /usr/src/app/scripts/$folder/*.sql | sort); do
        echo "🚀 Rodando: $f"
        # Executa o script especificando o banco -d WorkplaceDB
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d WorkplaceDB -i "$f"
    done
}

# 5. Execução na ordem lógica de dependência
execute_scripts "tables"
execute_scripts "views"
execute_scripts "procedures"
execute_scripts "triggers"

echo "------------------------------------------"
echo "✅ Ambiente Workplace configurado e pronto!"
echo "------------------------------------------"

# 6. Mantém o container ativo
tail -f /dev/null