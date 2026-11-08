## Techniek

- **.NET-versie:** .NET 10 (`net10.0`, getest met SDK 10.0.403)
- **Packages:** Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.SqlServer`, `.Design`), Swashbuckle.AspNetCore 10 (Swagger UI)
- **Database:** SQL Server via EF Core met een eigen `PortfolioDbContext`

## API-stijl en codestructuur

De API gebruikt **controllers** (`[ApiController]`) en is gelaagd opgebouwd: Controllers → Services → Data.
De motivatie achter deze keuzes staat in [ARCHITECTUUR.md](ARCHITECTUUR.md).

```
PortfolioApi/
├── Controllers/   ProjectsController, BlogPostsController (HTTP)
├── Services/      IProjectService/ProjectService, IBlogPostService/BlogPostService (logica)
├── Mappings/      Entity <-> DTO conversie (ToDto, ToEntity, ApplyTo)
├── Dtos/          Invoer- en uitvoer-DTO's met validatie
├── Models/        EF Core-entiteiten: Project, BlogPost, BlogPostImage
├── Data/          PortfolioDbContext (incl. seed data)
├── Migrations/    EF Core-migraties
└── Program.cs     Dependency injection (DbContext, services), Swagger, pipeline
```

## SQL Server configureren

De connectionstring staat in `PortfolioApi/appsettings.json` onder `ConnectionStrings:PortfolioDb`:

```json
"PortfolioDb": "Server=.\\SQLEXPRESS;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True"
```

- Standaard wordt een lokale **SQL Server Express**-instantie (`.\SQLEXPRESS`) gebruikt met Windows-authenticatie.
- Gebruik je **LocalDB**, vervang dan `Server=.\\SQLEXPRESS` door `Server=(localdb)\\MSSQLLocalDB`.
- Gebruik je een andere server of SQL-login, pas dan `Server` aan en vervang `Trusted_Connection=True`
  door `User Id=...;Password=...`.

## Database aanmaken

De database wordt aangemaakt met **EF Core-migraties** (niet met `EnsureCreated`).

- **Automatisch:** bij het starten roept `Program.cs` `db.Database.Migrate()` aan. De database
  `PortfolioDb`, de tabellen en de seed data (projecten en blogposts) worden dan aangemaakt of bijgewerkt.
- **Handmatig** (optioneel):

  ```bash
  dotnet tool install --global dotnet-ef --version 10.0.14
  cd PortfolioApi
  dotnet ef database update
  ```

Omdat de gegevens in SQL Server staan, blijven ze bestaan na een herstart van de applicatie.

## Applicatie starten

```bash
cd PortfolioApi
dotnet run --launch-profile http
```

Open daarna **Swagger UI** op http://localhost:5080/swagger om alle endpoints te testen:
`GET`, `GET {id}`, `POST`, `PUT {id}` en `DELETE {id}` voor zowel `/api/projects` als `/api/blogposts`.
## Frontend starten

```bash
cd client
npm install
npm run dev
```