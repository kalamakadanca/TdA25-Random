# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/TourDeApp.csproj", "src/"]
RUN dotnet restore "src/TourDeApp.csproj"
COPY src/ .
RUN dotnet build "src/TourDeApp.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "src/TourDeApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TourDeApp.dll"]