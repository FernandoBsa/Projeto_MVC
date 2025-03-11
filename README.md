
# 📚 Projeto de Empréstimo de Livros

Este é um sistema de gerenciamento de empréstimo de livros desenvolvido em **ASP.NET Core MVC (.NET 8)**, utilizando **Entity Framework Core** com banco de dados **PostgreSQL**. O objetivo do projeto é facilitar o controle de empréstimos, devoluções e organização do acervo de livros.

---

## 🏗 Estrutura do Projeto

A estrutura principal do projeto segue o padrão MVC (Model-View-Controller), com as seguintes pastas:

- **Controllers**: Responsável pelos controladores que recebem as requisições HTTP.
- **Data/Context**: Contém a classe de contexto do EF Core para comunicação com o banco de dados.
- **Migrations**: Histórico das migrações geradas pelo EF Core.
- **Models**: Representações das entidades da aplicação.
- **Services**: Contém a lógica de negócio da aplicação (sem separação entre Service e Repository Layer).
- **Utils**: Métodos utilitários e auxiliares.
- **Views**: Arquivos .cshtml responsáveis pela interface do usuário.
- **wwwroot**: Arquivos estáticos como CSS, JS e imagens.

---

## 🛠 Tecnologias Utilizadas

- **.NET 8**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **PostgreSQL**
- **Code First Migration**

---

## 🔐 Gerenciamento de Sessão (Login e Logout)

O projeto conta com uma funcionalidade simples de autenticação de usuário via **sessão HTTP**, implementada na classe `SessaoService`. Ela permite armazenar temporariamente os dados do usuário autenticado, evitando a necessidade de realizar login a cada nova requisição.

### Funcionalidades da sessão:
- **Login persistente na sessão** do navegador durante a navegação.
- **Armazenamento dos dados do usuário logado** na sessão com `SetString`.
- **Validação automática do usuário logado** em outras áreas do sistema.
- **Logout simples** com remoção da sessão (`RemoveSessao`).

Essa funcionalidade garante uma experiência mais fluida e segura para o usuário durante sua utilização do sistema.

---

## ⚙ Configuração do Ambiente

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/EmprestimoLivros.git
   cd EmprestimoLivros
   ```

2. **Configurar o banco de dados:**

   Atualize as strings de conexão no arquivo `appsettings.json` ou `appsettings.Development.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=sua_porta;Database=seu_banco;Username=postgres;Password=sua_senha"
   }
   ```
   
3. **Aplicar migrações e criar o banco:**
   ```bash
   dotnet ef database update
   ```

4. **Executar a aplicação:**
   ```bash
   dotnet run
   ```

---

## 📸 Estrutura do Projeto (Exemplo Visual)
```
EmprestimoLivros/
│
├── Controllers/
├── Data/Context/
├── Migrations/
├── Models/
├── Properties/
├── Services/
├── Utils/
├── Views/
├── wwwroot/
│
├── EmprestimoLivros.csproj
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── EmprestimoLivros.sln
```

---

## 💡 Observações

- O projeto foi desenvolvido com uma arquitetura simples e direta, sem uma separação em camadas específicas de Repository e Service Layer. Toda a lógica de negócio foi implementada diretamente nos serviços.
- Futuramente, o projeto pode ser refatorado para seguir uma arquitetura mais robusta com os princípios **SOLID** e **Design Patterns** como Repository Pattern e Unit of Work.

---

## 📄 Licença

Este projeto é open-source e está sob a licença MIT. Sinta-se livre para usá-lo e contribuir!

---

## ✉ Contato

Caso tenha dúvidas ou sugestões, entre em contato:
- 📧 fernandobarrosdesak@gmail.com
- 💻 [LinkedIn](https://www.linkedin.com/in/fernandobarrosdev/)
