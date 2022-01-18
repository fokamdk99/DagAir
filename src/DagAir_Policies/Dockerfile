FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_Policies/DagAir.Policies/", "./DagAir_Policies/DagAir.Policies/"]
COPY ["./DagAir_Policies/DagAir.Policies.Contracts/", "./DagAir_Policies/DagAir.Policies.Contracts/"]
COPY ["./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Contracts/", "./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Contracts/"]
COPY ["./DagAir_Policies/DagAir.Policies.Data/", "./DagAir_Policies/DagAir.Policies.Data/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/", "./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/"]

RUN dotnet restore ./DagAir_Policies/DagAir.Policies/DagAir.Policies.csproj
RUN dotnet build ./DagAir_Policies/DagAir.Policies/DagAir.Policies.csproj --no-restore
RUN dotnet publish ./DagAir_Policies/DagAir.Policies/DagAir.Policies.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.Policies.dll"]