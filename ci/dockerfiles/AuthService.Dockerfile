FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/AuthService/ ./AuthService/
COPY /src/backend/AuthService.BLL/ ./AuthService.BLL/
COPY /src/backend/AuthService.DAL/ ./AuthService.DAL/
COPY /src/backend/AuthService.Consts/ ./AuthService.Consts/

# Восстанавливаем зависимости для проекта AuthService
RUN dotnet restore AuthService/AuthService.csproj

# Собираем проект
RUN dotnet build AuthService/AuthService.csproj -c Release -o /app/bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENTRYPOINT ["dotnet", "AuthService.dll"]