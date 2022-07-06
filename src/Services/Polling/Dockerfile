#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Polling/Polling.API/Polling.API.csproj", "Polling/Polling.API/"]
COPY ["Polling/Polling.Infrastructure/Polling.Infrastructure.csproj", "Polling/Polling.Infrastructure/"]
COPY ["Polling/Polling.Domain/Polling.Domain.csproj", "Polling/Polling.Domain/"]
COPY ["Polling/Polling.Application/Polling.Application.csproj", "Polling/Polling.Application/"]
RUN dotnet restore "Polling/Polling.API/Polling.API.csproj"
COPY . .
WORKDIR "/src/Polling/Polling.API"
RUN dotnet build "Polling.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Polling.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Polling.API.dll"]