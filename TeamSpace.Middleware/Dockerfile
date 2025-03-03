FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TeamSpace.Middleware/TeamSpace.Middleware.csproj", "TeamSpace.Middleware/"]
COPY ["TeamSpace.Application/TeamSpace.Application.csproj", "TeamSpace.Application/"]
COPY ["TeamSpace.Infraestructure/TeamSpace.Infraestructure.csproj", "TeamSpace.Infraestructure/"]
COPY ["TeamSpace.Domain/TeamSpace.Domain.csproj", "TeamSpace.Domain/"]
COPY ["TeamSpace.Configuration/TeamSpace.Configuration.csproj", "TeamSpace.Configuration/"]
RUN dotnet restore "TeamSpace.Middleware/TeamSpace.Middleware.csproj"

COPY . .
WORKDIR "/src/TeamSpace.Middleware"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamSpace.Middleware.dll"]
