# Utiliza la imagen base oficial de .NET SDK para construir la aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del c�digo fuente
COPY . ./

# Publica la aplicaci�n en modo Release
RUN dotnet publish -c Release -o out

# Utiliza la imagen base oficial de ASP.NET Core Runtime para ejecutar la aplicaci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "SistemaDeDeudas.dll"]