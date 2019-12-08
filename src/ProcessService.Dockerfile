FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Services/ProcessService/WorkflowManager.ProcessService.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowManager.ProcessService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
ARG rabbit_port
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
COPY --from=publish /src/scripts/wait-for-it.sh ./wait-for-it.sh
RUN echo $rabbit_port
ENTRYPOINT ./wait-for-it.sh rabbitmq:5672 -- dotnet  WorkflowManager.ProcessService.API.dll