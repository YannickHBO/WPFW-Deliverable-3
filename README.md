# WPFWDesigns – Portfoliosite + Portfolio API

Deze repository bevat twee onderdelen:

| Map | Wat |
|---|---|
| `client/` | De portfoliosite als React-app (Vite + React Router) |
| `PortfolioApi/` | ASP.NET Core Web API (.NET 10) met CRUD voor projecten en blogposts |

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
