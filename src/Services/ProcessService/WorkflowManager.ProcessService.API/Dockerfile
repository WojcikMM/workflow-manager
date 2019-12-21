FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/ProcessService/WorkflowManager.ProcessService.API/WorkflowManager.ProcessService.API.csproj", "src/Services/ProcessService/WorkflowManager.ProcessService.API/"]
COPY ["src/Services/ProcessService/WorkflowManager.ProcessService.Core/WorkflowManager.ProcessService.Core.csproj", "src/Services/ProcessService/WorkflowManager.ProcessService.Core/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
COPY ["src/Services/ProcessService/WorkflowManager.ProcessService.ReadModel/WorkflowManager.ProcessService.ReadModel.csproj", "src/Services/ProcessService/WorkflowManager.ProcessService.ReadModel/"]
RUN dotnet restore "src/Services/ProcessService/WorkflowManager.ProcessService.API/WorkflowManager.ProcessService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ProcessService/WorkflowManager.ProcessService.API"
RUN dotnet build "WorkflowManager.ProcessService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.ProcessService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.ProcessService.API.dll"]