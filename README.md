# Northwind Database Project with .NET Core

This repository contains a .NET Core project that leverages the Northwind SQL database. The project is designed with a multi-layered architecture, following best practices and principles to ensure scalability, maintainability, and performance.

## Key Features

- **N-Layered Architecture**: The project is structured into multiple layers, each handling a specific aspect of the application. This architecture supports clean code practices, making the application easy to extend and maintain.
  
- **Dapper Integration**: Dapper, a lightweight and fast ORM, is used for database operations. It provides high-performance data access while maintaining simplicity and flexibility.

- **Entity Framework Option**: The project is flexible and can easily switch to Entity Framework by simply modifying the IoC (Inversion of Control) configuration. This allows developers to choose between Dapper and Entity Framework based on their specific needs and preferences.

- **Web APIs**: The application exposes a set of RESTful Web APIs to interact with the Northwind database, allowing for seamless integration with frontend applications or other services.

- **SOLID Principles**: The project adheres to SOLID principles, ensuring a robust, flexible, and easily maintainable codebase. This design approach allows for straightforward updates and modifications as requirements evolve.

- **AOP Techniques with Autofac**: The project utilizes Aspect-Oriented Programming (AOP) techniques via Autofac to handle cross-cutting concerns efficiently. Key AOP features include:
  - **JWT-Based Security**: Implements user-based authorization using JWT (JSON Web Tokens) for secure access control.
  - **Validation**: Ensures data integrity through systematic validation processes.
  - **Transaction Management**: Handles database transactions to ensure data consistency and reliability.
  - **Caching**: Improves performance by caching frequently accessed data.
  - **Logging**: Captures detailed logs for system monitoring and troubleshooting.
  - **Performance Monitoring**: Monitors the performance of various operations to maintain optimal efficiency.

- **Northwind SQL Database**: This project utilizes the Northwind database, a classic dataset for exploring data operations and queries within a real-world business context.

## Technologies Used

- **.NET Core 3.1**: A cross-platform framework for building high-performance, scalable applications.
- **Dapper**: A high-speed, micro ORM that provides a simple way to interact with SQL databases.
- **Entity Framework (Optional)**: The project supports easy switching to Entity Framework if needed, by adjusting the IoC configuration.
- **SQL Server**: The relational database management system used to manage the Northwind database.
- **Autofac**: An IoC container used to implement AOP techniques for handling concerns such as security, validation, and logging.

### Switching to Entity Framework

To switch from Dapper to Entity Framework, update the IoC (Inversion of Control) container configuration in the `Business\DependencyResolvers\Autofac\AutofacBusinessModule.cs` file. This allows you to switch ORMs with minimal code changes, giving you the flexibility to choose the best tool for your needs.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request or open an issue.
