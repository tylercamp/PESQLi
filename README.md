# PESQLi (Parametric Endpoint SQL Injection)

This is an intentionally-vulnerable web app, built on ASP.NET Core 3.1 and EF Core, with a SQLite backend.

## Building

Note: .NET Core SDK 3.1 or later is required.

### Standalone executable

This will build a larger package that can be ran later without needing .NET Core.

```bash
git clone https://github.com/tylercamp/PESQLi
cd PESQLi
dotnet publish -r RUNTIME
```

Replace `RUNTIME` with `osx-x64`, `linux-x64`, or `win-x64` depending on your platform.

Output files can be found in `bin/Debug/netcoreapp3.1/RUNTIME/publish`. This will contain `PESQLi.exe` on Windows, and `PESQLi` on other platforms. Run this executable to start the app.

### Runtime-dependent executable

This will build a smaller package that requires the .NET Core runtime to run.

```bash
git clone https://github.com/tylercamp/PESQLi
cd PESQLi
dotnet publish
```

Output files can be found in `bin/Debug/netcoreapp3.1`. This will contain `PESQLi.dll`. Run the command `dotnet PESQLi.dll` in the output folder to start the app.

## Usage

Start PESQLi after building, which runs the web server locally on port 5000.

Default settings store the SQLite database in a file named `model.db`, which can be customized by modifying `appsettings.json`.

The app maintains 2 model types internally, `Users` and `CmsEntries`. `Users` have no functional use, the type and table exists so that SQLi penetration tools can try detecting its presence. `CmsEntries` contain a `tag` and `content`, where the content of an entry can be accessed via URL based on its tag.

The app exposes 3 endpoints, all accessed via `GET`:

1. `/cms/{*tag}` - Uses a raw interpolated (unsanitized) SQL query to search for and return a CMS entry with the given tag.
2. `/database/` - Returns counts of `CmsEntry` and `User` record types in the database, used for confirming that the database exists and is initialized.
3. `/database/reset` - Uses `EnsureDeleted`, `EnsureCreated`, and a `Seed.Apply` to recreate the database from scratch and fill it with seed data.

