# Fase de construcción - Usa imagen SDK con Ubuntu Chiseled para mejor rendimiento
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build-env
WORKDIR /src

# 1. Copia solo los archivos necesarios para restaurar dependencias
COPY *.sln .
COPY *.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

# 2. Copia el resto del código y construye
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Fase final - Usa imagen distroless para reducir tamaño y mejorar seguridad
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled
WORKDIR /app

# Configura el usuario no-root (mejor práctica de seguridad)
USER app
ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

COPY --from=build-env --chown=app:app /app .
ENTRYPOINT ["dotnet", "SistemaDeDeudas.dll"]