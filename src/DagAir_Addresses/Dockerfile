FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_Addresses/DagAir.Addresses/", "./DagAir_Addresses/DagAir.Addresses/"]
COPY ["./DagAir_Addresses/DagAir.Addresses.Data/", "./DagAir_Addresses/DagAir.Addresses.Data/"]
COPY ["./DagAir_Addresses/DagAir.Addresses.Contracts/", "./DagAir_Addresses/DagAir.Addresses.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]

RUN dotnet restore ./DagAir_Addresses/DagAir.Addresses/DagAir.Addresses.csproj
RUN dotnet build ./DagAir_Addresses/DagAir.Addresses/DagAir.Addresses.csproj --no-restore
RUN dotnet publish ./DagAir_Addresses/DagAir.Addresses/DagAir.Addresses.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.Addresses.dll"]