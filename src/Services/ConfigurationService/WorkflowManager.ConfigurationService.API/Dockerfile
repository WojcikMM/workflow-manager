FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY ["src/Services/ConfigurationService/WorkflowManager.ConfigurationService.API/WorkflowManager.ConfigurationService.API.csproj", "src/Services/ConfigurationService/WorkflowManager.ConfigurationService.API/"]
COPY ["src/Services/ConfigurationService/WorkflowManager.ConfigurationService.Core/WorkflowManager.ConfigurationService.Core.csproj", "src/Services/ConfigurationService/WorkflowManager.ConfigurationService.Core/"]
COPY ["src/Services/ConfigurationService/WorkflowManager.ConfigurationService.ReadModel/WorkflowManager.ConfigurationService.ReadModel.csproj", "src/Services/ConfigurationService/WorkflowManager.ConfigurationService.ReadModel/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
RUN dotnet restore "src/Services/ConfigurationService/WorkflowManager.ConfigurationService.API/WorkflowManager.ConfigurationService.API.csproj"
COPY ["src/Services/ConfigurationService", "src/Services/ConfigurationService"]
COPY ["src/WorkflowManager.Common", "src/WorkflowManager.Common"]
COPY ["src/WorkflowManager.CQRS", "src/WorkflowManager.CQRS"]
WORKDIR "/src/Services/ConfigurationService/WorkflowManager.ConfigurationService.API"
RUN dotnet build --no-restore "WorkflowManager.ConfigurationService.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkflowManager.ConfigurationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
ADD vsdbg-linux-x64.tar.gz /vsdbg
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.ConfigurationService.API.dll"]