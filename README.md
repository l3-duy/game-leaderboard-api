# Game Leaderboard API

A lightweight, robust RESTful API built to manage player scores and leaderboards for games. 

## 🚀 Core Features

*   **Secure Submissions:** Implemented Custom Middleware to validate API Keys, ensuring only the game engine can submit scores.
*   **Architecture & Clean Code:** Built using the 3-Tier Architecture with the **Repository Pattern** and Dependency Injection for high testability and maintainability.
*   **Data Integrity:** Utilizes Data Transfer Objects (DTOs) to prevent overposting attacks and hide sensitive database structures.
*   **Global Exception Handling:** Integrated modern .NET `IExceptionHandler` and `ProblemDetails` (RFC 7807) to gracefully handle crashes and return standardized error responses.
*   **Optimized Retrieval:** Features offset pagination and EF Core's `AsNoTracking` for fast, lightweight leaderboard queries.

## 🛠️ Tech Stack

*   **Framework:** .NET (C#)
*   **Database & ORM:** SQLite, Entity Framework Core (Code-First Approach)
*   **Documentation:** Swagger UI / OpenAPI

## 🏃‍♂️ How to Run Locally

1. Clone the repository.
2. Add a secret key to `appsettings.json` under `GameSecrets:LeaderboardApiKey` (or use the existing test key).
3. Run `dotnet ef database update` to ensure the SQLite database is ready.
4. Execute `dotnet run` to start the server.
5. Access `http://localhost:5187/swagger` to test the endpoints.
