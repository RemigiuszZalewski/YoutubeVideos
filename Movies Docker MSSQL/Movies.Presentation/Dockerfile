FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 3000
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["Movies.Presentation/Movies.Presentation.csproj", "Movies.Presentation/"]
COPY ["Movies.Infrastructure/Movies.Infrastructure.csproj", "Movies.Infrastructure/"]
COPY ["Movies.Domain/Movies.Domain.csproj", "Movies.Domain/"]
COPY ["Movies.Application/Movies.Application.csproj", "Movies.Application/"]
COPY ["Movies.Contracts/Movies.Contracts.csproj", "Movies.Contracts/"]

RUN dotnet restore "Movies.Presentation/Movies.Presentation.csproj"

COPY . .

WORKDIR "/src/Movies.Presentation"
RUN dotnet build "Movies.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Movies.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Movies.Presentation.dll"]