FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["./DagAir_Facilities/DagAir.Facilities/", "./DagAir_Facilities/DagAir.Facilities/"]
COPY ["./DagAir_Facilities/DagAir.Facilities.Data/", "./DagAir_Facilities/DagAir.Facilities.Data/"]
COPY ["./DagAir_Facilities/DagAir.Facilities.Contracts/", "./DagAir_Facilities/DagAir.Facilities.Contracts/"]
COPY ["./DagAir_Components/DagAir.Components.ApiModels/", "./DagAir_Components/DagAir.Components.ApiModels/"]
COPY ["./DagAir_Components/DagAir.Components.Healthchecks/", "./DagAir_Components/DagAir.Components.Healthchecks/"]
COPY ["./DagAir_Components/DagAir.Components.Logging/", "./DagAir_Components/DagAir.Components.Logging/"]

RUN dotnet restore ./DagAir_Facilities/DagAir.Facilities/DagAir.Facilities.csproj
RUN dotnet build ./DagAir_Facilities/DagAir.Facilities/DagAir.Facilities.csproj --no-restore
RUN dotnet publish ./DagAir_Facilities/DagAir.Facilities/DagAir.Facilities.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DagAir.Facilities.dll"]