# Use the official .NET core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7199

# Use the official .NET core SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ["_api/api_sylvainbreton.csproj", "./"]
RUN dotnet restore "api_sylvainbreton.csproj"
COPY _api/ ./
RUN dotnet build "api_sylvainbreton.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "api_sylvainbreton.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api_sylvainbreton.dll"]