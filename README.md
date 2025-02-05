# Promomash Trader

This project is a sample project that implements simple user registration wizard. It is built using a **Modular Monolith** architecture, with a combination of backend services, frontend app, and testing suite.

The name 'Trader' is used for illustrative purposes only and does not represent the actual name of the application or product.

## Project Structure

The project is organized into the following directories:

- **`/src`**: The main source code of the application.
  - **`/API`**: Backend services API that handles business logic and data access.
  - **`/Aspire`**: Aspire hosting configuration and setup for managing the application environment.
  - **`/Frontend`**: Frontend application built using Angular.
  - **`/Modules`**: Contains modules that describe the various features of the application, organized using **Modular Monolith** architecture. Each module implements **Clean Architecture**.
    - **`/UserAccess`**: Handles user registration, authentication (out of scope), and access management(out of scope).
  - **`/Tests`**: Unit and integration tests using **xUnit** and **TestContainers** for database isolation.

## Requirements

Before running the project, ensure you have the following installed:

- **.NET SDK**: Ensure you're using .NET 9 or later.
- **Node.js**: For managing the frontend.
- **Angular CLI**: Required to serve and build the Angular frontend.
- **Docker**: Required for running services like PostgreSQL and the tests using TestContainers.

## Getting Started

Follow the steps below to get the project up and running locally.

### Step 1: Clone the Repository

Clone the repository to your local machine.

```
git clone https://github.com/albatyr/promomash-trader.git
cd promomash-trader
```

### Step 2: Set Up Backend API and Database

The backend API is set up using **Aspire** and is connected to a PostgreSQL database. You can run the backend API locally with the following steps:

1. Navigate to the `/src/Aspire/Promomash.Trader.AppHost` directory.
2. Build and run the project with Aspire:

```
cd src/Aspire/Promomash.Trader.AppHost
dotnet run
```

Aspire will build the application, run the API, and start the necessary services, including PostgreSQL.

The Aspire dashboard will be available at `https://localhost:17241`.

There is also Scalar API documentation will be available at `http://localhost:5197/scalar/v1`

### Step 3: Set Up Frontend Application

The frontend is built using **Angular**. To run the frontend application:

1. Navigate to the `/src/Frontend/Promomash.Trader.App` directory.
2. Install the dependencies:

```
cd src/Frontend/Promomash.Trader.App
npm install
```

3. Ensure you have Angular CLI installed globally:

```
npm install -g @angular/cli
```

4. Run the frontend:

```
ng serve
```

The frontend will be available at `http://localhost:4200`.

### Step 4: Set Up and Run Tests

To run tests, use **TestContainers** to run PostgreSQL in isolation. Ensure that Docker is running on your machine.

1. Navigate to the `/src/Tests/Promomash.Trader.Tests` directory.
2. Run the tests with the following command:

```
cd src/Tests/Promomash.Trader.Tests
dotnet test
```

This will execute the tests using **xUnit** and TestContainers.

### Step 5: Database Migration

If you need to apply database migrations, just run the aspire host following Step 2.

This will apply any pending migrations for the database.

## Configuration

You can configure various settings related to the database, API, and other services in the **`appsettings.json`** file inside the `/src/API` directory.

The connection string for PostgreSQL is automatically managed by **Aspire**.

### Example `appsettings.json`:

```
{
  "ConnectionStrings": {
    "traderDb": "Host=localhost;Port=5432;Database=traderDb;Username=postgres;Password=postgres"
  }
}
```

## License

This project is not licensed.
