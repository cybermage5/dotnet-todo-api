
# Todo API

This project is a simple **Todo API** built with **ASP.NET Core**, featuring **JWT Authentication**, **Entity Framework Core**, and **PostgreSQL**.

## Features

- **CRUD operations** for managing Todo items
- **JWT Authentication** to secure the API
- **Entity Framework Core** for data access
- **PostgreSQL** as the database provider
- **ASP.NET Core Identity** for user authentication

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/todo-api.git
cd todo-api
```

### 2. Install Dependencies

Make sure you have the necessary NuGet packages installed:

```bash
dotnet restore
```

### 3. Set up the Database

Create a PostgreSQL database and configure the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=your_database_name;Username=your_username;Password=your_password"
  }
}
```

### 4. Migrate the Database

Run the following commands to create the database tables:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Set up Environment Variables for JWT

Set the following environment variables for JWT authentication:

```bash
export JWT_KEY="YourSecureKey123456!"
export JWT_ISSUER="YourIssuer"
export JWT_AUDIENCE="YourAudience"
```

### 6. Run the Application

```bash
dotnet run
```

The application will be running on `http://localhost:5000` (or a different port if configured).

## API Endpoints

| Method | Endpoint          | Description                       |
|--------|-------------------|-----------------------------------|
| GET    | /api/Todo          | Get all Todo items                |
| GET    | /api/Todo/{id}     | Get a Todo item by ID             |
| POST   | /api/Todo          | Create a new Todo item            |
| PUT    | /api/Todo/{id}     | Update a Todo item                |
| DELETE | /api/Todo/{id}     | Delete a Todo item                |

## Authentication

The API uses **JWT Authentication**. To access the protected routes, a valid JWT token must be provided in the `Authorization` header:

```http
Authorization: Bearer <your_jwt_token>
```

### 7. JWT Token Generation

To generate a JWT token, implement an authentication endpoint (e.g., `/api/auth/login`) in your API, or use a pre-existing user management system with **ASP.NET Core Identity**.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License.
