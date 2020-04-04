FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ./WorkflowManager.CQRS ./WorkflowManager.CQRS
COPY ./WorkflowManager.Common/ ./WorkflowManager.Common
COPY ./Services/IdentityService/WorkflowManager.IdentityService.API ./Services/IdentityService/WorkflowManager.IdentityService.API
WORKDIR ./Services/IdentityService/WorkflowManager.IdentityService.API
RUN dotnet build "WorkflowManager.IdentityService.API.csproj" -c Release

FROM build AS publish
RUN dotnet publish "WorkflowManager.IdentityService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.IdentityService.API.dll"]