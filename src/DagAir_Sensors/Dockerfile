FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_Sensors/DagAir.Sensors/", "./DagAir_Sensors/DagAir.Sensors/"]
COPY ["./DagAir_Sensors/DagAir.Sensors.Contracts/", "./DagAir_Sensors/DagAir.Sensors.Contracts/"]
COPY ["./DagAir_Sensors/DagAir.Sensors.Data/", "./DagAir_Sensors/DagAir.Sensors.Data/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]

RUN dotnet restore ./DagAir_Sensors/DagAir.Sensors/DagAir.Sensors.csproj
RUN dotnet build ./DagAir_Sensors/DagAir.Sensors/DagAir.Sensors.csproj --no-restore
RUN dotnet publish ./DagAir_Sensors/DagAir.Sensors/DagAir.Sensors.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.Sensors.dll"]