# Architectuurkeuzes – Portfolio API

## 1. API-stijl: controllers

Ik heb gekozen voor **controllers** met `[ApiController]`.

- **Waarom:** een portfoliosite heeft twee duidelijke resources (projecten en blogposts) met
  allemaal dezelfde CRUD-acties. Een controller per resource groepeert die vijf acties
  vanzelf. Attributen als `[Route]`, `[HttpGet]` en `[ProducesResponseType]` maken in één
  oogopslag duidelijk welke route welke statuscode teruggeeft. `[ApiController]` zorgt
  bovendien voor automatische modelvalidatie: een ongeldige DTO geeft direct een
  `400 Bad Request` met foutmeldingen, zonder dat ik daar zelf code voor schrijf. Het sluit
  ook aan op Les 5, waardoor de code herkenbaar is.
- **Het alternatief, Minimal API:** routes worden met lambda's in `Program.cs` (of in
  extensiemethodes per feature, met `MapGroup`) gekoppeld, bijvoorbeeld
  `app.MapGet("/api/projects", (IProjectService s) => s.GetAllAsync())`. Afhankelijkheden
  komen binnen via **parameterinjectie** in plaats van constructorinjectie. Minimal API heeft
  minder ceremonie en iets minder overhead, en past goed bij kleine services of een
  verticale opbouw per feature. Validatie, statuscodes en groepering moet je daarbij wel
  explicieter zelf inrichten. Voor deze omvang hadden beide gewerkt. Ik koos controllers
  vanwege de vaste structuur en de ingebouwde validatie.

## 2. Code-organisatie: lagen

| Laag | Map | Verantwoordelijkheid |
|---|---|---|
| HTTP | `Controllers/` | Route, request-body ontvangen, juiste statuscode teruggeven (`200/201/204/400/404`). Geen databasecode. |
| Applicatielogica | `Services/` + `Mappings/` | Use cases (ophalen, aanmaken, wijzigen, verwijderen), sortering, omzetten tussen entiteit en DTO. |
| Data | `Data/` + `Models/` + `Migrations/` | `PortfolioDbContext`, entiteiten, tabelconfiguratie (maxlengtes, seed-data) en schema-migraties. |
| Contract | `Dtos/` | Wat de buitenwereld ziet en mag sturen. |

**Waarom lagen en geen verticale features:** de app heeft maar twee features, die ook nog
precies hetzelfde patroon volgen. In een gelaagde structuur vind je per soort
verantwoordelijkheid alles op één plek. Een verticale opbouw (bijvoorbeeld
`Features/Projects/CreateProject.cs` met endpoint, DTO en handler bij elkaar) wordt pas
voordelig als er veel features komen die verschillende logica hebben.

De controller kent alleen de **interface** van de service (`IProjectService`). De service
gebruikt de `DbContext`. Daardoor kan de logica los van HTTP getest of vervangen worden.

## 3. DTO's in plaats van entiteiten

Geen enkel endpoint ontvangt of verstuurt een EF Core-entiteit. Per resource zijn er drie DTO's:

- `ProjectDto` – **uitgaand**, bevat het `Id`.
- `CreateProjectDto` – **inkomend bij POST**, zonder `Id`, want de database genereert dat.
  Zo kan een client geen id forceren (*overposting*).
- `UpdateProjectDto` – **inkomend bij PUT**, het `Id` komt uit de route.

Voordelen: het databaseschema kan veranderen zonder dat het API-contract breekt, de
validatieregels (`[Required]`, `[StringLength]`, `[Url]`) staan op het contract waar ze
horen, en er lekken nooit velden naar buiten die niet bedoeld zijn om te tonen. De
mapping staat in eigen extensiemethodes (`ToDto`, `ToEntity`, `ApplyTo`) in `Mappings/`,
zodat je die makkelijk terugvindt.

## 4. Dependency injection

In `Program.cs` worden geregistreerd:

```csharp
builder.Services.AddDbContext<PortfolioDbContext>(o => o.UseSqlServer(...)); // scoped
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
```

Controllers krijgen de service via **constructorinjectie**, en services krijgen de
`DbContext` ook via de constructor. Nergens wordt met `new` een `DbContext` gemaakt. De
container maakt er één per request aan en ruimt die daarna op. **Scoped** is de juiste
levensduur, omdat een `DbContext` niet thread-safe is en per unit-of-work hoort te leven.

## 5. Opslag: EF Core + SQL Server met migraties

SQL Server zorgt ervoor dat gegevens een herstart overleven, wat met de InMemory-provider
niet zou lukken. Het schema en de seed data worden opgebouwd met **migraties**, te beginnen bij
`InitialCreate`, die bij het opstarten met `Database.Migrate()` worden toegepast. Anders dan bij `EnsureCreated` kan het
schema later stap voor stap meegroeien met de site (bijvoorbeeld tags of een
`ImageUrl` op een project) zonder dat bestaande projecten en blogposts verloren gaan.

## 6. Waarom dit past bij een portfoliosite

Een portfoliosite is klein, maar groeit wel: er komen projecten en blogposts bij, en
misschien later een beheerpagina of authenticatie. De gekozen opzet houdt de API
voorspelbaar (REST-routes, standaard statuscodes, gedocumenteerd in Swagger). Uitbreiden
blijft ook eenvoudig: een nieuw veld kost een DTO-veld, een mappingregel en een migratie,
zonder dat de controllers veranderen.
