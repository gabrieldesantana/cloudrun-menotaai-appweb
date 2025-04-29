FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MeNotaAi.Web/MeNotaAi.Web.csproj", "MeNotaAi.Web/"]
COPY ["MeNotaAi.Application/MeNotaAi.Application.csproj", "MeNotaAi.Application/"]
COPY ["MeNotaAi.Domain/MeNotaAi.Domain.csproj", "MeNotaAi.Domain/"]
COPY ["MeNotaAi.Infrastructure/MeNotaAi.Infrastructure.csproj", "MeNotaAi.Infrastructure/"]
RUN dotnet restore "./MeNotaAi.Web/MeNotaAi.Web.csproj"
COPY . .
WORKDIR "/src/MeNotaAi.Web"
RUN dotnet build "MeNotaAi.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeNotaAi.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeNotaAi.Web.dll"]