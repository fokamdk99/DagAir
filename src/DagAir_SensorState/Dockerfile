FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY ["./DagAir_SensorState/DagAir.DataServices.SensorState/", "./DagAir_SensorState/DagAir.DataServices.SensorState/"]
COPY ["./DagAir_SensorState/DagAir.DataServices.SensorState.Contracts/", "./DagAir_SensorState/DagAir.DataServices.SensorState.Contracts/"]
COPY ["./DagAir_SensorState/DagAir.DataServices.SensorState.Infrastructure/", "./DagAir_SensorState/DagAir.DataServices.SensorState.Infrastructure/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/", "./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]
COPY ["./DagAir_Components/DagAir.Components.HttpClients/", "./DagAir_Components/DagAir.Components.HttpClients/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]


RUN dotnet restore ./DagAir_SensorState/DagAir.DataServices.SensorState/DagAir.DataServices.SensorState.csproj
RUN dotnet build ./DagAir_SensorState/DagAir.DataServices.SensorState/DagAir.DataServices.SensorState.csproj --no-restore
RUN dotnet publish ./DagAir_SensorState/DagAir.DataServices.SensorState/DagAir.DataServices.SensorState.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.DataServices.SensorState.dll"]
