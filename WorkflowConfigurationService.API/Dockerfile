FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WorkflowConfigurationService.API/WorkflowConfigurationService.API.csproj", "WorkflowConfigurationService.API/"]
RUN dotnet restore "WorkflowConfigurationService.API/WorkflowConfigurationService.API.csproj"
COPY . .
WORKDIR "/src/WorkflowConfigurationService.API"
RUN dotnet build "WorkflowConfigurationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowConfigurationService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowConfigurationService.API.dll"]