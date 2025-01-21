# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivo de proyecto y restaurar dependencias
COPY ["SL_Backend/SL_Backend.csproj", "SL_Backend/"]
RUN dotnet restore "./SL_Backend/SL_Backend.csproj"

# Copiar el resto de los archivos y construir
COPY . .
WORKDIR "/src/SL_Backend"
RUN dotnet build "./SL_Backend.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "./SL_Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SL_Backend.dll"]