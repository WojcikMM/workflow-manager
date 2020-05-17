FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/StatusesService/WorkflowManager.StatusesService.API/WorkflowManager.StatusesService.API.csproj", "src/Services/StatusesService/WorkflowManager.StatusesService.API/"]
COPY ["src/Services/StatusesService/WorkflowManager.StatusesService.Core/WorkflowManager.StatusesService.Core.csproj", "src/Services/StatusesService/WorkflowManager.StatusesService.Core/"]
COPY ["src/WorkflowManager.Common/WorkflowManager.Common.csproj", "src/WorkflowManager.Common/"]
COPY ["src/WorkflowManager.CQRS/WorkflowManager.CQRS.csproj", "src/WorkflowManager.CQRS/"]
COPY ["src/Services/StatusesService/WorkflowManager.StatusesService.ReadModel/WorkflowManager.StatusesService.ReadModel.csproj", "src/Services/StatusesService/WorkflowManager.StatusesService.ReadModel/"]
RUN dotnet restore "src/Services/StatusesService/WorkflowManager.StatusesService.API/WorkflowManager.StatusesService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/StatusesService/WorkflowManager.StatusesService.API"
RUN dotnet build --no-restore "WorkflowManager.StatusesService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.StatusesService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkflowManager.StatusesService.API.dll"]