FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/WorkflowManager.APIGateway/", "WorkflowManager.APIGateway/"]
COPY ["src/WorkflowManager.Common/", "WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/", "WorkflowManager.CQRS/"]
RUN dotnet restore "WorkflowManager.APIGateway/WorkflowManager.APIGateway.csproj"
WORKDIR "WorkflowManager.APIGateway"
RUN dotnet build "WorkflowManager.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.APIGateway.dll"]