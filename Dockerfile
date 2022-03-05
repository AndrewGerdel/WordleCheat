FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /source

COPY WordleCheat/*.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "WordleCheat.dll"]
