SET WORKSPACE=%CD%
cd %~dp0/../src
SET TAG=latest
IF NOT %1 == '' SET TAG=%1
docker build -t workflowmanager.azurecr.io/workflow-manager-identity-service-api:%TAG% -f Services/IdentityService/WorkflowManager.IdentityService.API/Dockerfile .
docker push workflowmanager.azurecr.io/workflow-manager-identity-service-api:%TAG%
cd %WORKSPACE%