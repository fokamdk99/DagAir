FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY ["./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory/", "./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory/"]
COPY ["./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Contracts/", "./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Contracts/"]
COPY ["./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Data/", "./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Data/"]
COPY ["./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Infrastructure/", "./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory.Infrastructure/"]
COPY ["./DagAir_Components/DagAir.Components.Influx/", "./DagAir_Components/DagAir.Components.Influx/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/", "./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]
COPY ["./DagAir_Components/DagAir.Components.HttpClients/", "./DagAir_Components/DagAir.Components.HttpClients/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]


RUN dotnet restore ./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory/DagAir.DataServices.SensorStateHistory.csproj
RUN dotnet build ./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory/DagAir.DataServices.SensorStateHistory.csproj --no-restore
RUN dotnet publish ./DagAir_SensorStateHistory/DagAir.DataServices.SensorStateHistory/DagAir.DataServices.SensorStateHistory.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.DataServices.SensorStateHistory.dll"]
