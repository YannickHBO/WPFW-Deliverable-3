# WPFWDesigns – Portfoliosite + Portfolio API

Deze repository bevat twee onderdelen:

| Map | Wat |
|---|---|
| `client/` | De portfoliosite als React-app (Vite + React Router) |
| `PortfolioApi/` | ASP.NET Core Web API (.NET 10) met CRUD voor projecten en blogposts |

Documentatie bij de opdracht:

- [ARCHITECTUUR.md](ARCHITECTUUR.md): onderbouwing van de architectuurkeuzes (max. één A4)
- [PortfolioApi/PortfolioApi.http](PortfolioApi/PortfolioApi.http): dezelfde testverzoeken, uit te voeren vanuit VS Code

## Snel starten (twee terminals)

```bash
# Terminal 1 – API
cd PortfolioApi
dotnet run --launch-profile http      # http://localhost:5080/swagger

# Terminal 2 – frontend
cd client
npm install                           # alleen de eerste keer
npm run dev                           # http://localhost:5173
```

## React-frontend (`client/`)

- Pagina's: Home, Whoami, Blog, Projects, Contact (`src/pages/`), met gedeelde layout,
  navigatiemenu en weer-afhankelijk thema in `src/components/`.
- **Blog** en **Projects** halen hun gegevens op uit de API (`src/api/portfolioApi.js`).
- Tijdens development stuurt Vite alle `/api`-requests door naar `http://localhost:5080`
  (zie `vite.config.js`). Browser en API zitten daardoor op één origin en er is geen
  CORS-configuratie nodig. Draait de API ergens anders, zet dan `VITE_API_URL`.
- Vereist Node.js 18 of hoger.

## Portfolio API

- **Stijl:** controllers (`[ApiController]`), gelaagd: Controllers → Services → Data (EF Core)
- **Database:** SQL Server (standaard `.\SQLEXPRESS`), via EF Core met eigen `PortfolioDbContext`
- **Documentatie / testen:** Swagger UI via `Swashbuckle.AspNetCore`

### Vereisten

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (Express, LocalDB of volledig). De connection string staat in
  `PortfolioApi/appsettings.json` onder `ConnectionStrings:PortfolioDb`:

  ```json
  "PortfolioDb": "Server=.\\SQLEXPRESS;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True"
  ```

  Gebruik je LocalDB, vervang `Server=.\\SQLEXPRESS` dan door `Server=(localdb)\\MSSQLLocalDB`.

### Starten

```bash
cd PortfolioApi
dotnet run --launch-profile http
```

Open daarna Swagger UI op <http://localhost:5080/swagger>.

### Database aanmaken: migraties

Ik gebruik **EF Core-migraties** (niet `EnsureCreated`). De migratie `InitialCreate` staat in
`PortfolioApi/Migrations/` en maakt de tabellen `Projects` en `BlogPosts` aan, inclusief
een paar seed-records (via `HasData`).

- Bij het opstarten roept `Program.cs` `db.Database.Migrate()` aan. De database wordt dus
  automatisch aangemaakt en bijgewerkt; je hoeft niets handmatig te doen.
- Handmatig kan ook:

  ```bash
  dotnet tool install --global dotnet-ef
  cd PortfolioApi
  dotnet ef database update            # migraties toepassen
  dotnet ef migrations add <Naam>      # nieuwe migratie na een modelwijziging
  ```

Waarom migraties: het schema is versiebeheerd, en latere wijzigingen (bijv. een nieuw veld)
kunnen worden doorgevoerd zonder de database met alle gegevens weg te gooien. Met
`EnsureCreated` kan dat niet.

### Endpoints

| Methode | Route | Body (DTO) | Response |
|---|---|---|---|
| GET | `/api/projects` | – | `200` lijst van `ProjectDto` |
| GET | `/api/projects/{id}` | – | `200` `ProjectDto` / `404` |
| POST | `/api/projects` | `CreateProjectDto` | `201` `ProjectDto` / `400` |
| PUT | `/api/projects/{id}` | `UpdateProjectDto` | `200` `ProjectDto` / `400` / `404` |
| DELETE | `/api/projects/{id}` | – | `204` / `404` |
| GET | `/api/blogposts` | – | `200` lijst van `BlogPostDto` |
| GET | `/api/blogposts/{id}` | – | `200` `BlogPostDto` / `404` |
| POST | `/api/blogposts` | `CreateBlogPostDto` | `201` `BlogPostDto` / `400` |
| PUT | `/api/blogposts/{id}` | `UpdateBlogPostDto` | `200` `BlogPostDto` / `400` / `404` |
| DELETE | `/api/blogposts/{id}` | – | `204` / `404` |

Voorbeeld-body voor `POST /api/projects`:

```json
{
  "title": "Portfolio API",
  "description": "ASP.NET Core Web API met EF Core en SQL Server",
  "category": "Backend",
  "gitHubUrl": "https://github.com/gebruiker/portfolio-api",
  "date": "2026-11-07"
}
```

Voorbeeld-body voor `POST /api/blogposts`:

```json
{
  "title": "Mijn eerste API",
  "content": "Vandaag heb ik een Web API gebouwd...",
  "publishedDate": "2026-11-07"
}
```

### Datamodel

- **Project:** `Id`, `Title`, `Description`, `Category`, `GitHubUrl` (optioneel), `Date`
- **BlogPost:** `Id`, `Title`, `Content`, `PublishedDate`

### Projectstructuur

```
PortfolioApi/
├── Controllers/   HTTP: routes, statuscodes (ProjectsController, BlogPostsController)
├── Services/      Applicatielogica (IProjectService/ProjectService, IBlogPostService/BlogPostService)
├── Mappings/      Entity <-> DTO conversie
├── Dtos/          Wat de API ontvangt en terugstuurt (incl. validatie)
├── Models/        EF Core-entiteiten (Project, BlogPost)
├── Data/          PortfolioDbContext
├── Migrations/    EF Core-migraties
└── Program.cs     DI-registratie (DbContext, services), Swagger, pipeline
```
