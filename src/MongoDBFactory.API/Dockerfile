FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MongoDBFactory.API/MongoDBFactory.API.csproj", "src/MongoDBFactory.API/"]
RUN dotnet restore "./src/MongoDBFactory.API/MongoDBFactory.API.csproj"
COPY . .
WORKDIR "/src/src/MongoDBFactory.API"
RUN dotnet build "./MongoDBFactory.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MongoDBFactory.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongoDBFactory.API.dll"]