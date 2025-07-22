# Sistema de Reserva de Salas

Projeto didático desenvolvido ao longo das aulas de **Técnicas de Programação II** (Fatec Atibaia).  
Objetivo: substituir a planilha manual usada pelos coordenadores para reservar salas de aula, integrando conceitos de **ASP.NET Core MVC**, **Entity Framework Core**, **GoF** e boas práticas de camadas.

---

## ✨ Tecnologias & Ferramentas

| Camada               | Stack                                    | Observações                                  |
|----------------------|------------------------------------------|-----------------------------------------------|
| **Back‑end**         | .NET 7 · ASP.NET Core MVC               | Hot‑reload com `dotnet watch`                 |
| **Persistência**     | SQL Server · Entity Framework Core      | Migrations automáticas para DDL               |
| **Front‑end**        | Razor Views · Bootstrap 5               | Tag Helpers, partial views e componentes      |
| **Tabelas dinâmicas**| DataTables + Buttons                    | Busca, paginação e exportação CSV/Excel/PDF   |
| **Build & CLI**      | `dotnet CLI`                            | `dotnet build` / `dotnet run`                 |

---

## 🎯 Funcionalidades Implementadas

| Código  | Descrição           |
|---------|---------------------|
| **RF01** | Cadastrar Usuário     |
| **RF02** | Listar Usuários       |
| **RF03** | Editar Usuário        |
| **RF04** | Excluir Usuário       |
| **RF05** | Cadastrar Sala        |
| **RF06** | Listar Salas          |
| **RF07** | Editar Sala           |
| **RF08** | Excluir Sala          |
| **RF09** | Reservar Sala         |
| **RF10** | Listar Reservas       |
| **RF11** | Editar Reserva        |
| **RF12** | Excluir Reserva       |

### Regras de Negócio

| Código  | Regra                                                                 |
|---------|----------------------------------------------------------------------|
| **RN01** | **Evitar conflitos de horário** – não permite reservas sobrepostas na mesma sala e data |
| **RN02** | **Bloquear datas/horas passadas** – impede agendamentos retroativos                |
| **RN03** | **Verificar capacidade da sala** – rejeita reservas que excedem a lotação            |

---

## 🏛️ Padrões de Projeto (GoF) Utilizados

| Categoria      | Padrão        | Onde aparece                             | Papel                                                       |
|----------------|---------------|------------------------------------------|-------------------------------------------------------------|
| **Criacional** | Singleton     | `Startup`/`Program.cs` (injeção de serviços) | Garante instância única de serviços (`AddScoped` e `AddDbContext`) |
| **Estrutural** | Facade        | `ReservasFacade`                          | Interface simplificada para orquestrar serviços de Usuário, Sala e Reserva |
| **Comportamental** | Strategy  | `ValidadorDeReservaHorario`, `ValidadorDeReservaCapacidade` | Permite trocar algoritmos de validação em tempo de execução           |

---

## 🚀 Como executar localmente

1. **Pré‑requisitos**
   - .NET 7 SDK ou superior
   - SQL Server (Express ou completo)
   - Visual Studio 2022 / VS Code (Opcional)

2. **Clone o repositório**
   ```bash
   git clone https://github.com/SEU_USUARIO/reserva-de-salas.git
   cd reserva-de-salas
   ```

3. **Configurar a string de conexão**
   - Abra `appsettings.json` e atualize `"DefaultConnection"` com seu servidor SQL e credenciais:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=ReservaSalas;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
     }
     ```

4. **Gerar e aplicar migrações**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Executar a aplicação**
   ```bash
   dotnet run
   ```
   - Acesse no navegador: `https://localhost:5001` (ou a porta exibida no console).

---

**Bom estudo e bons códigos!**
