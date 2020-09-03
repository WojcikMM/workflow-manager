FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY ["src/Services/IdentityService/WorkflowManager.IdentityService.API/WorkflowManager.IdentityService.API.csproj", "src/Services/IdentityService/WorkflowManager.IdentityService.API/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
RUN dotnet restore "src/Services/IdentityService/WorkflowManager.IdentityService.API/WorkflowManager.IdentityService.API.csproj"
COPY ["src/Services/IdentityService", "src/Services/IdentityService"]
COPY ["src/WorkflowManager.Common", "src/WorkflowManager.Common"]
COPY ["src/WorkflowManager.CQRS", "src/WorkflowManager.CQRS"]
WORKDIR "/src/Services/IdentityService/WorkflowManager.IdentityService.API"
RUN dotnet build --no-restore "WorkflowManager.IdentityService.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkflowManager.IdentityService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
ADD vsdbg-linux-x64.tar.gz /vsdbg
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.IdentityService.API.dll"]