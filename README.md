## 🚧PROJETO EM DESENVOLVIMENTO🚧
# 🏢 WorkplaceSimulation

O **WorkplaceSimulation** é um projeto que simula o ecossistema de um ambiente de trabalho real, gerenciando o ciclo de vida de colaboradores, departamentos, contratos e alocações em projetos.

### 🎯 Objetivo do Projeto
O foco principal deste projeto é explorar a abordagem **Database-First** com lógica de negócios centralizada no banco de dados. Diferente de arquiteturas tradicionais onde a lógica reside inteiramente na aplicação (API), aqui o Banco de Dados (SQL Server) detém as regras de integridade, cálculos e restrições legais.

---

### 🏗️ Arquitetura e Fluxo de Dados
A aplicação segue um modelo de camadas onde a inteligência está na base:

1.  **Database (The Brain):** Centraliza toda a inteligência. Utiliza **Stored Procedures** para processos complexos, **Triggers** para auditoria e automação de limpeza, e **Views** para abstração de consultas complexas (JOINs).
2.  **API (The Bridge):** Atua como uma camada fina (*Thin Layer*). Sua responsabilidade é validar os dados de entrada, gerenciar a autenticação e servir como uma ponte de comunicação, disparando as Procedures e consumindo as Views.



---

### 🛠️ Funcionalidades Implementadas (SQL Server)

* **Gestão de Colaboradores:** Procedures seguras para contratação, desativação (Soft Delete) e reativação de funcionários.
* **Controle Salarial:** Validação rigorosa de regras de negócio, incluindo a trava constitucional de irredutibilidade salarial (Art. 7º, VI da CF) e limites percentuais de aumento.
* **Gestão de Projetos (N:N):** Alocação dinâmica de funcionários em múltiplos projetos com validações de prazo e duplicidade.
* **Auditoria Automática:** Triggers de log para rastreabilidade de alterações salariais e limpeza automática de vínculos em projetos após desligamentos.
* **Relatórios Consolidados:** Views otimizadas para leitura rápida de dados relacionais complexos.

---

### 💻 Tecnologias Utilizadas
* **RDBMS:** SQL Server (T-SQL)
* **Backend:** .NET / C# (Consumidor das Procedures/Views)
* **Database Management:** DataGrip 2026.1

---

### 💡 Por que esta abordagem?
Este projeto demonstra como centralizar a lógica no banco de dados pode garantir a **integridade dos dados** independentemente da aplicação que o consome, além de proporcionar ganhos de performance em operações complexas e facilitar a manutenção de regras de negócio sem a necessidade de novos deploys da API.


---


### 🐳 Como Rodar com Docker
Com o Docker instalado, execute o comando abaixo na raiz do projeto:
```bash
docker-compose up --build
```
O ambiente será configurado automaticamente:
1. O SQL Server 2022 será iniciado.
2. O banco `WorkplaceDB` será criado.
3. Scripts de Tabelas, Views, Procedures e Triggers serão aplicados na ordem correta de dependência.
