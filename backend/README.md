# 🏥 ACME Clinic — Registro de Atendimento

Sistema Web desenvolvido para gerenciamento de pacientes e registro de atendimentos da clínica ACME.

Projeto desenvolvido utilizando **.NET 8 + Dapper + SQLite + Clean Architecture + xUnit**.

---

# Tecnologias

- .NET 8
- ASP.NET Core Web API
- Dapper
- SQLite
- Swagger
- xUnit
- Moq
- Clean Architecture

---

# Arquitetura

```plaintext
AcmeClinic
│
├── AcmeClinic.Api
│
├── AcmeClinic.Application
│   ├── DTOs
│   ├── Services
│   └── Exceptions
│
├── AcmeClinic.Domain
│   ├── Entities
│   ├── Interfaces
│   └── Constants
│
├── AcmeClinic.Infrastructure
│   ├── Context
│   ├── Repositories
│   └── DatabaseInitializer
│
└── AcmeClinic.Tests
```

---

# Funcionalidades

## Pacientes

### Cadastro

- Nome (obrigatório)
- Data nascimento (obrigatório)
- CPF único (obrigatório)
- Sexo (obrigatório)
- Endereço completo
- Status (Ativo/Inativo)

### Regras

✔ Não permite CPF duplicado  
✔ Não permite data nascimento futura

### Operações

- Criar
- Editar
- Consultar
- Buscar por Nome
- Buscar por CPF
- Filtrar Status
- Ativar
- Inativar
- Paginação

---

## Atendimentos

### Cadastro

- Paciente
- Data e hora
- Descrição
- Status

### Regras

✔ Não permite atendimento futuro  
✔ Apenas pacientes ativos

### Operações

- Criar
- Editar
- Consultar
- Filtrar período
- Filtrar paciente
- Filtrar status
- Ativar
- Inativar
- Paginação

---

# Configuração do Projeto

## Clonar

```bash
git clone URL_DO_REPOSITORIO
```

Entrar:

```bash
cd backend
```

---

## Restaurar dependências

```bash
dotnet restore
```

---

## Compilar

```bash
dotnet build
```

---

## Rodar API

```bash
dotnet run --project AcmeClinic.Api
```

Swagger:

```plaintext
https://localhost:xxxx/swagger
```

---

# Banco de Dados

Banco utilizado:

```plaintext
SQLite
```

Criação automática:

```plaintext
DatabaseInitializer.cs
```

Sem migrations.

---

# Executar Testes

Rodar todos:

```bash
dotnet test
```

Rodar detalhado:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Cobertura:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Paginação

Todas as listagens possuem paginação.

Exemplo:

```http
GET /api/pacientes?page=1&pageSize=10
```

Resposta:

```json
{
  "data": [],
  "total": 20,
  "page": 1,
  "pageSize": 10
}
```

---

# Tratamento Global de Erros

Implementado middleware global.

Exemplos:

## 400

```json
{
 "message":"Status inválido"
}
```

## 404

```json
{
 "message":"Paciente não encontrado"
}
```

## 409

```json
{
 "message":"CPF já cadastrado"
}
```

---

# Endpoints

## Pacientes

```http
POST /api/pacientes/create
GET /api/pacientes/all
GET /api/pacientes/{id}
PUT /api/pacientes/{id}
PATCH /api/pacientes/{id}/activate
PATCH /api/pacientes/{id}/inactivate
```

---

## Atendimentos

```http
POST /api/atendimentos/create
GET /api/atendimentos/all
GET /api/atendimentos/{id}
PUT /api/atendimentos/{id}
PATCH /api/atendimentos/{id}/activate
PATCH /api/atendimentos/{id}/inactivate
```

---

# Testes

Cobertura de:

- Services
- Regras de negócio
- Exceções
- Cenários positivos
- Cenários negativos

---

# Melhorias Futuras

- Frontend React
- Docker
- CI/CD
- FluentValidation
- Autenticação JWT
- Logs estruturados
- Cache
