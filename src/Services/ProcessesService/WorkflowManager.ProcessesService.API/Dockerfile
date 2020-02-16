FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.API/WorkflowManager.ProcessesService.API.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.API/"]
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.Core/WorkflowManager.ProcessesService.Core.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.Core/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.ReadModel/WorkflowManager.ProcessesService.ReadModel.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.ReadModel/"]
RUN dotnet restore "src/Services/ProcessesService/WorkflowManager.ProcessesService.API/WorkflowManager.ProcessesService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ProcessesService/WorkflowManager.ProcessesService.API"
RUN dotnet build "WorkflowManager.ProcessesService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.ProcessesService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.ProcessesService.API.dll"]