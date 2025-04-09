# Utiliza la imagen base oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
# Copia el archivo .csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore
# Copia el resto del código fuente
COPY . ./
# Publica la aplicación en modo Release
RUN dotnet publish -c Release -o out

# Utiliza la imagen base oficial de ASP.NET Core Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

# Exponer el puerto 8080
EXPOSE 8080

# Variables de entorno para configurar el puerto
ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080

ENTRYPOINT ["dotnet", "SistemaDeDeudas.dll"]