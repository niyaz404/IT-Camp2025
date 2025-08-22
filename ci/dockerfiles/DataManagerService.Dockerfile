FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/DataManagerService/ ./DataManagerService/
COPY /src/backend/DataManagerService.BLL/ ./DataManagerService.BLL/
COPY /src/backend/DataManagerService.DAL/ ./DataManagerService.DAL/
COPY /src/backend/DataManagerService.Common/ ./DataManagerService.Common/
COPY /src/backend/Share/ ./Share/

RUN dotnet restore DataManagerService/DataManagerService.csproj

RUN dotnet build DataManagerService/DataManagerService.csproj -c Release -o /app/bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENTRYPOINT ["dotnet", "DataManagerService.dll"]