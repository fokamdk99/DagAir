FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_ClientNode/DagAir.ClientNode/", "./DagAir_ClientNode/DagAir.ClientNode/"]
COPY ["./DagAir_ClientNode/DagAir.ClientNode.Infrastructure/", "./DagAir_ClientNode/DagAir.ClientNode.Infrastructure/"]
COPY ["./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/", "./DagAir_PolicyNode/DagAir.PolicyNode.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/", "./DagAir_Components/DagAir.Components.MassTransit.RabbitMq/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]

RUN dotnet restore ./DagAir_ClientNode/DagAir.ClientNode/DagAir.ClientNode.csproj
RUN dotnet build ./DagAir_ClientNode/DagAir.ClientNode/DagAir.ClientNode.csproj --no-restore
RUN dotnet publish ./DagAir_ClientNode/DagAir.ClientNode/DagAir.ClientNode.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.ClientNode.dll"]