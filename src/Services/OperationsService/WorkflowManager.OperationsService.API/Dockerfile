FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/OperationsService/WorkflowManager.OperationsService.API/WorkflowManager.OperationsService.API.csproj", "src/Services/OperationsService/WorkflowManager.OperationsService.API/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
RUN dotnet restore "src/Services/OperationsService/WorkflowManager.OperationsService.API/WorkflowManager.OperationsService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/OperationsService/WorkflowManager.OperationsService.API"
RUN dotnet build "WorkflowManager.OperationsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.OperationsService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.OperationsService.API.dll"]