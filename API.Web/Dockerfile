# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore using solution file
RUN dotnet restore "SkypointSocial.sln"

# Build and publish Api.Web (assuming it's the main entry)
RUN dotnet publish "API.Web/API.Web.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.Web.dll"]
