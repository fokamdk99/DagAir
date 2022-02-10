FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY ["./Subscriber/", "./Subscriber/"]

RUN dotnet restore ./Subscriber/Subscriber.csproj
RUN dotnet build ./Subscriber/Subscriber.csproj --no-restore
RUN dotnet publish ./Subscriber/Subscriber.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Subscriber.dll", "host.docker.internal"]
