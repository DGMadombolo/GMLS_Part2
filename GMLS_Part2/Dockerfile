# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["GMLS_Part2/GMLS_Part2.csproj", "GMLS_Part2/"]

RUN dotnet restore "GMLS_Part2/GMLS_Part2.csproj"

COPY . .

WORKDIR "/src/GMLS_Part2"

RUN dotnet build "GMLS_Part2.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish

RUN dotnet publish "GMLS_Part2.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GMLS_Part2.dll"]