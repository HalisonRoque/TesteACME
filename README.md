# ACME Clinic

Sistema Web para gerenciamento de **Pacientes** e **Atendimentos Clínicos**.

O projeto foi dividido em duas aplicações independentes:

* **Frontend** → Interface do usuário desenvolvida com Angular.
* **Backend** → API REST desenvolvida com .NET.

---

# Visão Geral

A aplicação permite:

✔ Cadastro de pacientes
✔ Consulta e edição de pacientes
✔ Ativação e inativação de registros
✔ Cadastro de atendimentos
✔ Associação de atendimento ao paciente
✔ Busca e filtros
✔ Paginação

---

# Arquitetura do Projeto

```plaintext
AcmeClinic
│
├── frontend/
│   └── Aplicação Angular
│
├── backend/
│   └── API .NET
│
└── README.md
```

---

# Tecnologias Utilizadas

## Frontend

* Angular 19
* TypeScript
* Angular Material
* Angular CDK
* Angular Router
* Reactive Forms

---

## Backend

* .NET 8
* ASP.NET Core Web API
* Dapper
* SQLite
* Swagger
* xUnit
* Moq
* Clean Architecture

---

# Pré-requisitos

Instalar:

### Node.js

Recomendado:

```plaintext
20+
```

Verificar:

```bash
node -v
npm -v
```

---

### .NET SDK

Recomendado:

```plaintext
.NET 8
```

Verificar:

```bash
dotnet --version
```

---

# Clonar Projeto

```bash
git clone https://github.com/HalisonRoque/TesteACME.git
```

Entrar:

```bash
cd AcmeClinic
```

---

# Executando Backend

Entrar na pasta:

```bash
cd backend
```

Restaurar dependências:

```bash
dotnet restore
```

Entrar na pasta:
```bash
cd AcmeClinic.Api
```


Compilar:

```bash
dotnet build
```

Executar:

```bash
dotnet run
```

API disponível:

```plaintext
https://localhost:5094
```

Swagger:

```plaintext
https://localhost:5094/swagger
```

---

# Executando Frontend

Abrir outro terminal.

Entrar na pasta:

```bash
cd frontend/acmeclinic
```

Instalar dependências:

```bash
npm install
```

Executar:

```bash
npm run start
```

Aplicação disponível:

```plaintext
http://localhost:4200
```

---

# Fluxo de Inicialização

Subir primeiro o backend:

```bash
cd backend
dotnet run
```

Depois subir frontend:

```bash
cd frontend
npm start
```

Acessar:

```plaintext
Frontend:
http://localhost:4200

Backend:
https://localhost:5094/swagger
```

---

# Estrutura do Frontend

```plaintext
frontend/
│
├── src/
│   ├── app/
│   │   ├── components
│   │   ├── core
│   │   ├── pages
│   │   ├── shared
│   │   └── app.routes.ts
│
├── environments
└── styles.css
```

---

# Estrutura do Backend

```plaintext
backend/
│
├── AcmeClinic.Api
├── AcmeClinic.Application
├── AcmeClinic.Domain
├── AcmeClinic.Infrastructure
└── AcmeClinic.Tests
```

---

# Funcionalidades

## Pacientes

* Criar
* Editar
* Consultar
* Buscar
* Filtrar
* Ativar
* Inativar
* Paginar

---

## Atendimentos

* Criar
* Editar
* Consultar
* Buscar
* Filtrar período
* Ativar
* Inativar
* Paginar

---

# Banco de Dados

Banco utilizado:

```plaintext
SQLite
```

Inicialização automática

---

# Executar Testes

Backend:

```bash
cd backend
dotnet test
```

---

# Melhorias Futuras

* Docker
* CI/CD
* JWT
* Logs estruturados
* Cache
* Deploy automatizado
* Cobertura de testes ampliada

---

# Desenvolvido por

Projeto desenvolvido para o desafio técnico **ACME Clinic**.
