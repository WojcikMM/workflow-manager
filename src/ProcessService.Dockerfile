FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Services/ProcessService/WorkflowManager.ProcessService.API"
#RUN dotnet build "WorkflowManager.ProcessService.API.csproj" -c Release -o /app/build
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.ProcessService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.ProcessService.API.dll"]
