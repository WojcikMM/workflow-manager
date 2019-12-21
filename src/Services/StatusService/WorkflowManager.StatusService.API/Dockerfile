FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/StatusService/WorkflowManager.StatusService.API/WorkflowManager.StatusService.API.csproj", "src/Services/StatusService/WorkflowManager.StatusService.API/"]
COPY ["src/Services/StatusService/WorkflowManager.StatusService.Core/WorkflowManager.StatusService.Core.csproj", "src/Services/StatusService/WorkflowManager.StatusService.Core/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
COPY ["src/Services/StatusService/WorkflowManager.StatusService.ReadModel/WorkflowManager.StatusService.ReadModel.csproj", "src/Services/StatusService/WorkflowManager.StatusService.ReadModel/"]
RUN dotnet restore "src/Services/StatusService/WorkflowManager.StatusService.API/WorkflowManager.StatusService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/StatusService/WorkflowManager.StatusService.API"
RUN dotnet build "WorkflowManager.StatusService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.StatusService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.StatusService.API.dll"]