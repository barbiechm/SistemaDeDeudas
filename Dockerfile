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
ENTRYPOINT ["dotnet", "SistemaDeDeudas.dll"]