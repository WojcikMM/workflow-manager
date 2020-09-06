FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.API/WorkflowManager.ProcessesService.API.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.API/"]
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.Core/WorkflowManager.ProcessesService.Core.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.Core/"]
COPY ["src/Services/ProcessesService/WorkflowManager.ProcessesService.ReadModel/WorkflowManager.ProcessesService.ReadModel.csproj", "src/Services/ProcessesService/WorkflowManager.ProcessesService.ReadModel/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
RUN dotnet restore "src/Services/ProcessesService/WorkflowManager.ProcessesService.API/WorkflowManager.ProcessesService.API.csproj"
COPY ["src/Services/ProcessesService", "src/Services/ProcessesService"]
COPY ["src/WorkflowManager.Common", "src/WorkflowManager.Common"]
COPY ["src/WorkflowManager.CQRS", "src/WorkflowManager.CQRS"]
WORKDIR "/src/Services/ProcessesService/WorkflowManager.ProcessesService.API"
RUN dotnet build --no-restore "WorkflowManager.ProcessesService.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkflowManager.ProcessesService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
ADD vsdbg-linux-x64.tar.gz /vsdbg
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.ProcessesService.API.dll"]