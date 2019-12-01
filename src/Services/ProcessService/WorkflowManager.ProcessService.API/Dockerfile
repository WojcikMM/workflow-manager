FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WorkflowManager.ProductService.API/WorkflowManager.ProductService.API.csproj", "WorkflowManager.ProductService.API/"]
RUN dotnet restore "WorkflowManager.ProductService.API/WorkflowManager.ProductService.API.csproj"
COPY . .
WORKDIR "/src/WorkflowManager.ProductService.API"
RUN dotnet build "WorkflowManager.ProductService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.ProductService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManager.ProductService.API.dll"]