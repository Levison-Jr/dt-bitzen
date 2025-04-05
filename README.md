# DESAFIO TÉCNICO BITZEN

## Objetivo
- Desenvolver uma API RESTFul em .Net 8/9 conectando com um banco de dados PostgreSQL.
- O projeto deve contemplar o desenvolvimento de uma Web Api para cadastro de agendamento de salas de reunião.
## Tecnologias
- .Net 
- PostgreSQL 
- Entity Framework 
- Swagger

## Requisitos Obrigatórios
- Gerenciar Usuários
-- Criar, editar e excluir usuários
-- Cada usuário deve ter nome, e-mail e senha para registro
- Autenticação de usuários
-- Realizar a autenticação dos usuários na api
-- Login com autenticação via JWT
- Gerenciar Salas
-- Criar, editar e excluir salas
-- Cada sala deve ter um nome e uma capacidade máxima de pessoas
- Gerenciar Reservas
-- Criar, listar e cancelar reservas de salas
-- Validar conflitos de horários (não permitir reservas sobrepostas para a mesma sala)
-- As reservas devem iniciar e finalizar no mesmo dia
- Listagem de Reservas
-- Buscar reservas por usuário e sala
-- Permitir filtros por data e status (ativa/cancelada)

## Execução do Projeto com Docker Compose
### Clonar e acessar pasta do repositório

    git clone git@github.com:Levison-Jr/dt-bitzen.git
    cd dt-bitzen
  
  ### Executar Docker Compose
  
    docker compose --project-name dtbitzen-group up -d

### Acessar Página do Swagger
http://localhost:4652/swagger/index.html
