
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Polling/Polling.API/Polling.API.csproj", "Services/Polling/Polling.API/"]
COPY ["Services/Polling/Polling.Infrastructure/Polling.Infrastructure.csproj", "Services/Polling/Polling.Infrastructure/"]
COPY ["Services/Polling/Polling.Domain/Polling.Domain.csproj", "Services/Polling/Polling.Domain/"]
COPY ["Services/Polling/Polling.Application/Polling.Application.csproj", "Services/Polling/Polling.Application/"]
RUN dotnet restore "Services/Polling/Polling.API/Polling.API.csproj"
COPY . .
WORKDIR "/src/Services/Polling/Polling.API"
RUN dotnet build "Polling.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Polling.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Polling.API.dll"]