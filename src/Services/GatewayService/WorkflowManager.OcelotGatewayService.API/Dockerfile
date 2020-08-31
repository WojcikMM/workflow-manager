FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY "WorkflowManager.OcelotGatewayService.API.csproj" "WorkflowManager.OcelotGatewayService.API.csproj"
RUN dotnet restore WorkflowManager.OcelotGatewayService.API.csproj
COPY . .
RUN dotnet build "WorkflowManager.OcelotGatewayService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.OcelotGatewayService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.OcelotGatewayService.API.dll"]