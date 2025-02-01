This is a project for the course ASPNET Advanced Softuni
## ASP.NET Advanced 2024 October Project

### Setup Instructions:
**Connection String Configuration**:
- The project uses **Entity Framework Core** (EF Core) for database access and **SQL Server** as the database provider.
- To start the project, input your **DefaultConnection** string in the `appsettings.json` file under the **ConnectionStrings** section.
  
Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
  }
}
