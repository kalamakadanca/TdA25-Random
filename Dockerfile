FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TdA25-Random.csproj", "./"]
RUN dotnet restore "./TdA25-Random.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TdA25-Random.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TdA25-Random.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TdA25-Random.dll"]
