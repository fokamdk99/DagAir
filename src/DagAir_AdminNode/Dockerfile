FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_AdminNode/DagAir.AdminNode/", "./DagAir_AdminNode/DagAir.AdminNode/"]
COPY ["./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/", "./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/"]
COPY ["./DagAir_Facilities/DagAir.Facilities.Contracts/", "./DagAir_Facilities/DagAir.Facilities.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.HttpClients/", "./DagAir_Components/DagAir.Components.HttpClients/"]

RUN dotnet restore ./DagAir_AdminNode/DagAir.AdminNode/DagAir.AdminNode.csproj
RUN dotnet build ./DagAir_AdminNode/DagAir.AdminNode/DagAir.AdminNode.csproj --no-restore
RUN dotnet publish ./DagAir_AdminNode/DagAir.AdminNode/DagAir.AdminNode.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.AdminNode.dll"]