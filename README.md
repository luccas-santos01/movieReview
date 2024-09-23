# Movie Review System

## Introdução

Esta é uma API desenvolvida com intuito de gerenciar um sistema básico de cadastro de filmes e suas resenhas, onde usuários podem se cadastrar, fazer um login, cadastrar um filme e sua respectiva resenha. Conta com sistema de autenticação e gerenciamento de permissões para acesso de determinadas rotas.

## Docker

A aplicação pode ser executada em um contêiner usando **Docker**. O arquivo `docker-compose.yml` configura os serviços necessários para rodar a API e o banco de dados.

### Comandos Úteis

**Construir a imagem**:

```bash
docker-compose build -d --build
```

## Formatos Suportados

A API aceita e retorna dados exclusivamente no formato JSON.

### Exemplo de Corpo de Requisição

Aqui está um exemplo de como deve ser o corpo de uma requisição para adicionar um filme:

```json
{
  "title": "Inception",
  "releaseYear": 2010,
  "image": "https://m.media-amazon.com/images/I/71uKM+LdgFL._AC_SY879_.jpg",
  "genre": "Sci-Fi"
}
```

## Autenticação

A autenticação é realizada por meio de tokens JWT (JSON Web Tokens), que são gerados após a validação das credenciais do usuário.

### Geração de Token

- O método Generate na classe TokenGenerator cria um token JWT, utilizando um segredo armazenado em uma variável de ambiente (JWT_SECRET).

- O token é configurado para expirar após 4 horas.

### Claims

- O token inclui informações (claims) sobre o usuário, como Email, Name e Role (administrador ou não). Esta informação é útil para validar acesso e gerenciar o conteúdo personalizado para cada usuário.

- Se alguma informação essencial estiver faltando, uma exceção é lançada.

### Verificação de Permissões

As permissões são verificadas usando controles de acesso definidos no program.cs, onde o acesso a rotas específicas é autorizado com base nos claims do token.

### Uso do Token

O token gerado deve ser incluído no cabeçalho de autorização (Authorization) em requisições que requerem autenticação.

## Endpoints

### MovieController

- **GET /movie**

  - Lista todos os filmes.

- **POST /movie**

  - Adiciona um filme. Retorna o criado.

- **PUT /movie/update/{movieId}**

  - Atualiza um filme pelo ID. Requer "Admin Only".

- **DELETE /movie/delete/{movieId}**
  - Remove um filme pelo ID. Requer "Admin Only".

### ReviewController

- **GET /review**

  - Lista todas as avaliações.

- **POST /review**

  - Adiciona uma avaliação. Retorna a criada.

- **PUT /review/update/{reviewId}**

  - Atualiza uma avaliação pelo ID. Permite atualização por autor ou administrador.

- **DELETE /review/delete/{reviewId}**
  - Remove uma avaliação pelo ID.

### UserController

- **GET /user**

  - Lista todos os usuários. Requer "Admin Only".

- **POST /user/signup**

  - Cria um usuário e retorna um token JWT.

- **POST /user/login**

  - Autentica um usuário e retorna um token JWT.

- **PUT /user/update**

  - Atualiza informações de um usuário. Requer "Admin Only".

- **DELETE /user/delete/{userId}**

  - Remove um usuário pelo ID. Requer "Admin Only".

  ## Considerações Finais

Nesta API, utilizei as seguintes ferramentas e pacotes:

- **.NET 6**

- **Microsoft.AspNetCore.Authentication**: Para gerenciar autenticação em aplicações ASP.NET Core.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Para suportar autenticação via tokens JWT.
- **Microsoft.EntityFrameworkCore**: ORM para interagir com bancos de dados usando C#.
- **Pomelo.EntityFrameworkCore.MySql**: Suporte para o MySQL no Entity Framework Core.
- **Swashbuckle.AspNetCore**: Para gerar documentação Swagger da API.

Em caso de dúvidas, sugestões ou melhorias, por favor, fico à inteira disposição.
