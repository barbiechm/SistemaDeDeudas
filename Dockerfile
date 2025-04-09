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

# Exponer el puerto 5000 (o el que uses en tu aplicación)
EXPOSE 5000

# Variables de entorno para configurar el puerto
ENV ASPNETCORE_URLS=http://+:5000
ENV PORT=5000

ENTRYPOINT ["dotnet", "SistemaDeDeudas.dll"]