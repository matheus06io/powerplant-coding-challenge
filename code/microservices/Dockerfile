ARG OFFICIAL_NUGET_SOURCE_REPOSITORY=https://api.nuget.org/v3/index.json

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
ARG OFFICIAL_NUGET_SOURCE_REPOSITORY

WORKDIR /src
COPY ["microservice.powercalculation/src/Microservice.PowerCalculation/Microservice.PowerCalculation.csproj", "microservice.powercalculation/src/Microservice.PowerCalculation/"]
COPY ["platform/src/Platform/Platform.csproj", "platform/src/Platform/Platform.csproj"]
RUN dotnet restore "microservice.powercalculation/src/Microservice.PowerCalculation/Microservice.PowerCalculation.csproj"
COPY . .
WORKDIR "/src/microservice.powercalculation/src/Microservice.PowerCalculation"
RUN dotnet build "Microservice.PowerCalculation.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "Microservice.PowerCalculation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.PowerCalculation.dll"]