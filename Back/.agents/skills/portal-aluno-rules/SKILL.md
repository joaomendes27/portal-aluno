---
name: portal-aluno-rules
description: Regras de qualidade de código, convenções de nomenclatura, arquitetura e padrões do projeto Portal Aluno. Esta skill deve ser consultada SEMPRE que for criar ou modificar código neste projeto.
---

# Portal Aluno - Regras do Projeto

Esta skill define as regras e convenções obrigatórias do projeto **Portal Aluno**. Consulte sempre antes de criar ou modificar código.

## Quando Usar Esta Skill

Use esta skill **SEMPRE** que:

- For criar novos arquivos (classes, controllers, handlers, etc.)
- For refatorar código existente
- For adicionar novas funcionalidades
- For escrever testes
- Tiver dúvida sobre nomenclatura ou padrões

---

## Arquitetura

O projeto segue **Clean Architecture** com os seguintes layers:

| Layer | Projeto | Responsabilidade |
|-------|---------|-----------------|
| Domain | `Portal-Aluno.Domain` | Entidades, interfaces de repositório |
| Application | `Portal-Aluno.Application` | Commands, Queries (CQRS via MediatR), Handlers, DTOs |
| Infrastructure | `Portal-Aluno.Infrastructure` | EF Core, repositórios, serviços externos |
| Presentation | `Portal-Aluno` | Controllers, middleware, configuração |
| Tests | `Portal-Aluno.Tests` | Testes unitários xUnit |

### Stack Tecnológica

- **Backend:** .NET 8, Entity Framework Core 8, MediatR (CQRS)
- **Banco de Dados:** PostgreSQL
- **Autenticação:** JWT Bearer Tokens
- **Testes:** xUnit + Moq + FluentAssertions

---

## Padrões de Qualidade de Código

### 1. Código Limpo e Sem Comentários

- O código deve ser **autoexplicativo**
- Nomes de variáveis, métodos e classes devem comunicar a intenção **sem necessidade de comentários**
- Não use comentários explicativos — se o código precisa de comentário, ele precisa ser reescrito

### 2. Nomenclatura em Português

- **Todas** as classes, propriedades, DTOs, Commands, Queries e nomes de tabelas/colunas devem estar em **português**
- **Banco de dados:** usar `snake_case` para tabelas e colunas (ex: `carga_horaria`, `data_criacao`)
- **C#:** usar `PascalCase` para classes e métodos, `camelCase` para variáveis locais

#### Exemplos de Nomenclatura

```csharp
// ✅ CORRETO
public class CadastrarCursoCommand : IRequest<int> { }
public class GetAllSalasQuery : IRequest<List<SalaResponse>> { }
public class Aluno { public string Nome { get; set; } }

// ❌ ERRADO
public class CreateCourseCommand : IRequest<int> { }
public class GetAllRoomsQuery : IRequest<List<RoomResponse>> { }
public class Student { public string Name { get; set; } }
```

### 3. Organização de Pastas (Application Layer)

Seguir o padrão por funcionalidade:

```
Portal-Aluno.Application/
├── Features/
│   ├── CursoFeature/
│   │   ├── Commands/
│   │   │   ├── CadastrarCurso/
│   │   │   │   ├── CadastrarCursoCommand.cs
│   │   │   │   └── CadastrarCursoCommandHandler.cs
│   │   │   └── AtualizarCurso/
│   │   │       ├── AtualizarCursoCommand.cs
│   │   │       └── AtualizarCursoCommandHandler.cs
│   │   ├── Queries/
│   │   └── DTOs/
│   ├── SalaFeature/
│   ├── TurmaFeature/
│   ├── DisciplinaFeature/
│   ├── UsuarioFeature/
│   ├── MatriculaFeature/
│   └── AgendaFeature/
```

### 4. Testes Contínuos

- **Toda** nova funcionalidade **deve** ter testes unitários
- Framework: **xUnit** + **Moq** + **FluentAssertions**
- Cobrir **cenários de sucesso e falha**
- Nomenclatura de testes: `MetodoTestado_Cenario_ResultadoEsperado`

```csharp
// ✅ Exemplo de teste
[Fact]
public async Task Handle_QuandoCursoValido_DeveCadastrarComSucesso()
{
    // Arrange
    var dto = new CadastrarCursoRequest("Engenharia", "Bacharelado", 3600);
    var comando = new CadastrarCursoCommand(dto);
    _cursoRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Curso>()))
        .Returns(Task.CompletedTask);

    // Act
    var resultado = await _handler.Handle(comando, CancellationToken.None);

    // Assert
    _cursoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Curso>()), Times.Once);
    _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
}
```

### 5. Boas Práticas

- Seguir **SOLID**, **Clean Architecture** e **DRY**
- Métodos **pequenos** e com **responsabilidade única**
- Usar **injeção de dependência** sempre
- Usar **async/await** para operações I/O
- Validações em **Commands** usando validações manuais ou FluentValidation
- Retornar exceções tipadas para tratamento de erros

### 6. Entity Framework (Banco de Dados)

- Configurações de entidade em classes separadas (`IEntityTypeConfiguration<T>`)
- Usar **snake_case** no mapeamento de tabelas e colunas
- Migrations com nomes descritivos em português

```csharp
// ✅ Exemplo de mapeamento
public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("cursos");
        builder.Property(c => c.Nome).HasColumnName("nome").IsRequired();
        builder.Property(c => c.CargaHoraria).HasColumnName("carga_horaria");
    }
}
```

---

## Checklist Antes de Submeter Código

- [ ] Nomes de classes/métodos/propriedades em português?
- [ ] Sem comentários desnecessários?
- [ ] Testes unitários criados para cenários de sucesso e falha?
- [ ] Mapeamento de banco de dados usando snake_case?
- [ ] Código segue Clean Architecture (Domain não depende de Infrastructure)?
- [ ] Métodos pequenos com responsabilidade única?
