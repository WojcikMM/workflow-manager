SET WORKSPACE=%CD%
cd %~dp0/../src
docker build -t workflowmanager.azurecr.io/workflow-manager-identity-service-api -f Services/IdentityService/WorkflowManager.IdentityService.API/Dockerfile . && PAUSE
cd %WORKSPACE%