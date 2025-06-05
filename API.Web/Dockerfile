FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API.Web/API.Web.csproj", "API.Web/"]
COPY ["Api/API.csproj", "Api/"]
COPY ["DAL/DAL.csproj", "DAL/"]
RUN dotnet restore "API.Web/API.Web.csproj"
COPY . .
WORKDIR "/src/API.Web"
RUN dotnet build "API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Web.dll"]