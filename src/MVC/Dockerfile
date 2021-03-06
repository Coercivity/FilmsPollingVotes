#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LobbyMVC/LobbyMVC.csproj", "LobbyMVC/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "LobbyMVC/LobbyMVC.csproj"
COPY . .
WORKDIR "/src/LobbyMVC"
RUN dotnet build "LobbyMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LobbyMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LobbyMVC.dll"]