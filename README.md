# Dog Houses REST API



A simple REST API developed in C# that interacts with a Microsoft SQL database. This API allows you to perform various actions on a "dogs" table.

- Developed using ASP .NET Core Web API.
- Utilizes Entity Framework (EF Core) for database interactions.
- Makes extensive use of async-await patterns for improved performance.
- Adheres to SOLID principles for clean and maintainable code.
- Comprehensive unit tests ensure all logic in the application functions as expected

## Endpoints

### 1. Ping

** Description:  Check if the service is up and running.



### 2. Get Dogs

** Description:  Retrieve a list of dogs. Supports sorting by attribute and pagination.


** Query Parameters: 
- `attribute` (string, optional): Sort dogs by attribute (e.g., "weight").
- `order` (string, optional): Sorting order, "asc" or "desc" (e.g., "desc").
- `pageNumber` (int, optional): Page number.
- `pageSize` (int, optional): Number of items per page.


### 3. Create Dog
** Description: Create a new dog.



 ### 4. Rate Limiting
** Description:   The application handles too many incoming requests by setting a rate limit of 10 requests per second. If exceeded, it returns an HTTP 429 (Too Many Requests) status code.

## Key Principles
- Architecture: This project follows the principles of clean architecture to separate concerns and make the codebase maintainable and flexible.

- SOLID Principles:  The code adheres to SOLID principles, enhancing modularity and maintainability.
