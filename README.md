# WasteGlassAPI – .NET 8 REST API

## Prerequisites
- .NET 8 SDK  →  https://dotnet.microsoft.com/download/dotnet/8.0

## Local Development

```bash
cd WasteGlassAPI
dotnet restore
dotnet run
```

API starts at: **http://localhost:5000**  
Swagger UI:    **http://localhost:5000/swagger**

The SQLite database is auto-created at `Database/WasteGlass.db` on first run.  
6 test suppliers are seeded automatically.

## Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET  | /api/route?lat=&lng=   | Optimised stop sequence (Dijkstra) |
| GET  | /api/suppliers         | List all suppliers |
| POST | /api/collections       | Submit a single collection |
| POST | /api/collections/sync  | Batch sync all records |
| GET  | /api/report            | Trip summary report |
| PATCH| /api/suppliers/{id}/reset | Reset supplier status (testing) |

## Hosting on Railway (Free Tier)

1. Push code to GitHub
2. Go to https://railway.app → New Project → Deploy from GitHub
3. Select `WasteGlassAPI` as root directory
4. Railway detects .NET automatically
5. Copy the generated URL → paste into Flutter `constants.dart`

## Reset for Demo

To reset all supplier statuses back to Pending (start a fresh demo):
```
PATCH /api/suppliers/1/reset
PATCH /api/suppliers/2/reset
... (repeat for 3-6)
```
Or clear the `Collections` table and reset `Status` in the SQLite DB directly.
