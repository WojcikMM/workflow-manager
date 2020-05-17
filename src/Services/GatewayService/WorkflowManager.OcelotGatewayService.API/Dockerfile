FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .
RUN dotnet build "WorkflowManager.OcelotGatewayService.API.csproj" -c Release

FROM build AS publish
RUN dotnet publish "WorkflowManager.OcelotGatewayService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.OcelotGatewayService.API.dll"]