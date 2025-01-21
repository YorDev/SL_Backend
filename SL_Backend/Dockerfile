# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Configura el usuario como root
USER root

# Copiar archivos de solución y restaurar dependencias
COPY *.sln .
COPY SL_Backend/*.csproj ./SL_Backend/
RUN dotnet restore ./SL_Backend/SL_Backend.csproj

# Copiar el resto de los archivos y construir
COPY . .
WORKDIR /app/SL_Backend
RUN dotnet publish -c Release -o /out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "SL_Backend.dll"]
