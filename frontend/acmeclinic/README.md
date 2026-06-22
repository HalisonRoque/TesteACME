# ACME Clinic - Frontend

Frontend da aplicação **ACME Clinic**, desenvolvido com **Angular 19**, responsável pela interface de gerenciamento de pacientes e atendimentos clínicos.

O sistema permite cadastrar, consultar, editar, ativar e inativar registros através de uma interface moderna e responsiva.

---

## Tecnologias utilizadas

### Frontend

* Angular 19
* TypeScript
* Angular Material
* Angular CDK
* Angular Router
* Angular Forms (Reactive Forms)

### Ferramentas de desenvolvimento

* Angular CLI

---

## Estrutura do projeto

```bash
src/
├── app/
│   ├── components/
│   ├── core/
│   │   ├── services/
│   │   └── interceptors/
│   ├── pages/
│   │   ├── home/
│   │   ├── pacientes/
│   │   └── atendimentos/
│   ├── shared/
│   │   ├── interfaces/
│   │   ├── models/
│   │   ├── modals/
│   │   └── notification/
│   └── app.routes.ts
│
├── environments/
└── styles.css
```

---

## Pré-requisitos

Antes de executar o projeto, instale:

* Node.js (versão 20+ recomendada)
* npm

Verifique:

```bash
node -v
npm -v
```

---

## Instalação

Entre na pasta:

```bash
cd acmeclinic
```

Instale as dependências:

```bash
npm install
```

---

## Executando o projeto

Iniciar ambiente local:

```bash
npm start
```

Acesse:

```bash
http://localhost:4200
```

---

## Funcionalidades

### Pacientes

* Cadastro de pacientes
* Edição de pacientes
* Listagem paginada
* Filtro por nome
* Ativação e inativação

### Atendimentos

* Cadastro de atendimento
* Edição de atendimento
* Busca por paciente
* Autocomplete de pacientes
* Filtro por período
* Ativação e inativação
* Paginação

---

## Interface

O projeto utiliza:

* Angular Material para componentes
* Bootstrap para apoio em layout

---

## Desenvolvido por

Projeto desenvolvido para o desafio técnico **ACME Clinic**.
