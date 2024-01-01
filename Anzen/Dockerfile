# Base image for ASP.NET Core 8.0 on Windows
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-20H2 AS base
WORKDIR /app

# Install SQLite (using PowerShell)
RUN powershell -Command Invoke-WebRequest -Uri https://github.com/nuget/NuGet.Client/releases/download/4.9.3/NuGet.exe -OutFile nuget.exe; \
    Start-Process .\nuget.exe install packages.sqlite -Source https://api.nuget.org/v3/index.json -OutputDirectory .\tools

# Copy project files and database
COPY . .

# Build the application
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Final image for running the application
FROM base AS final
WORKDIR /app
COPY --from=base /app/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]