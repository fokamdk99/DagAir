FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_PolicyNode/DagAir.PolicyNode/", "./DagAir_PolicyNode/DagAir.PolicyNode/"]
COPY ["./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/", "./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/"]
COPY ["./DagAir_PolicyNode/DagAir.PolicyNode.Infrastructure/", "./DagAir_PolicyNode/DagAir.PolicyNode.Infrastructure/"]
COPY ["./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/", "./DagAir_IngestionNode/DagAir.IngestionNode.Contracts/"]
COPY ["./DagAir_Policies/DagAir.Policies.Contracts/", "./DagAir_Policies/DagAir.Policies.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.HttpClients/", "./DagAir_Components/DagAir.Components.HttpClients/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]

RUN dotnet restore ./DagAir_PolicyNode/DagAir.PolicyNode/DagAir.PolicyNode.csproj
RUN dotnet build ./DagAir_PolicyNode/DagAir.PolicyNode/DagAir.PolicyNode.csproj --no-restore
RUN dotnet publish ./DagAir_PolicyNode/DagAir.PolicyNode/DagAir.PolicyNode.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.PolicyNode.dll"]