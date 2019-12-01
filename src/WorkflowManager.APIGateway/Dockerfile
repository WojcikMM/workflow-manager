FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WorkflowManagerGateway/WorkflowManagerGateway.csproj", "WorkflowManagerGateway/"]
RUN dotnet restore "WorkflowManagerGateway/WorkflowManagerGateway.csproj"
COPY . .
WORKDIR "/src/WorkflowManagerGateway"
RUN dotnet build "WorkflowManagerGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManagerGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowManagerGateway.dll"]