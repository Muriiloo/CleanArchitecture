# Clean Architecture com .NET

Projeto desenvolvido com o objetivo de estudar e aplicar os conceitos de **Clean Architecture**, **Domain-Driven Design (DDD)** e boas práticas de desenvolvimento backend utilizando **ASP.NET Core**.

> **Aviso:** Este é um projeto de estudos, desenvolvido para aprofundar conhecimentos em arquitetura de software. Novas funcionalidades e melhorias são adicionadas continuamente conforme o aprendizado evolui.

---

## Objetivos

* Aplicar os princípios da Clean Architecture
* Praticar conceitos de Domain-Driven Design (DDD)
* Desenvolver uma API desacoplada e de fácil manutenção
* Implementar autenticação utilizando JWT
* Utilizar padrões e boas práticas adotados no ecossistema .NET

---

## Tecnologias

* .NET
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* MediatR
* FluentValidation
* JWT Authentication
* xUnit
* Moq

---

## Estrutura do Projeto

```text
src
├── CleanArquitecture.Api
├── CleanArquitecture.Application
├── CleanArquitecture.Domain
└── CleanArquitecture.Infrastructure

tests
└── CleanArquitecture.Application.Tests
```

### Camadas

### Domain

Contém as regras de negócio da aplicação.

* Entidades
* Value Objects
* Interfaces
* Erros de domínio

### Application

Responsável pelos casos de uso.

* Commands
* Handlers
* Validators
* Behaviors (Pipeline)
* Interfaces de serviços

### Infrastructure

Implementações externas da aplicação.

* Entity Framework Core
* Repositórios
* Autenticação JWT
* Persistência
* Configurações

### API

Camada de apresentação.

* Controllers
* Middlewares
* Configuração de autenticação
* Injeção de dependências

---

## Conceitos aplicados

* Clean Architecture
* SOLID
* CQRS
* MediatR
* Domain-Driven Design (DDD)
* Value Objects
* Repository Pattern
* Unit of Work
* Result Pattern
* Dependency Injection
* Middleware para tratamento global de exceções
* Validação com FluentValidation
* Autenticação JWT

---

## Funcionalidades implementadas

* Cadastro de clientes
* Validação de regras de negócio utilizando Value Objects
* Pipeline de validação com MediatR
* Tratamento global de exceções
* Persistência com Entity Framework Core
* Autenticação via JWT
* Testes unitários

---

## Aprendizados

Este projeto está sendo utilizado para estudar temas como:

* Arquitetura Limpa
* Domain-Driven Design
* Inversão de Dependência
* Boas práticas de modelagem
* Organização de soluções em múltiplas camadas
* Desenvolvimento orientado a testes (TDD)
* Padrões de projeto utilizados no ecossistema .NET
