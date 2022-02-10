FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode/", "./DagAir_IngestionNode/DagAir.IngestionNode/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/", "./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Infrastructure/", "./DagAir_IngestionNode/DagAir.IngestionNode.Infrastructure/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Data/", "./DagAir_IngestionNode/DagAir.IngestionNode.Data/"]
COPY ["./DagAir_Sensors/DagAir.Sensors.Contracts/", "./DagAir_Sensors/DagAir.Sensors.Contracts/"]
COPY ["./DagAir_SensorState/DagAir.DataServices.SensorState.Contracts/", "./DagAir_SensorState/DagAir.DataServices.SensorState.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.HttpClients/", "./DagAir_Components/DagAir.Components.HttpClients/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]
COPY ["./DagAir_Components/DagAir.Components.Influx/", "./DagAir_Components/DagAir.Components.Influx/"]


RUN dotnet restore ./DagAir_IngestionNode/DagAir.IngestionNode/DagAir.IngestionNode.csproj
RUN dotnet build ./DagAir_IngestionNode/DagAir.IngestionNode/DagAir.IngestionNode.csproj --no-restore
RUN dotnet publish ./DagAir_IngestionNode/DagAir.IngestionNode/DagAir.IngestionNode.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.IngestionNode.dll"]