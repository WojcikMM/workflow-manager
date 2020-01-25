FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/GatewayService/WorkflowManager.GatewayService.API/", "WorkflowManager.GatewayService.API/"]
COPY ["src/WorkflowManager.Common/", "WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/", "WorkflowManager.CQRS/"]
RUN dotnet restore "WorkflowManager.GatewayService.API/WorkflowManager.GatewayService.API.csproj"
WORKDIR "WorkflowManager.GatewayService.API"
RUN dotnet build "WorkflowManager.GatewayService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.GatewayService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.GatewayService.API.dll"]
