SET WORKSPACE=%CD%
cd %~dp0/../src
SET TAG=latest
SET SERVICE_NAME=%1
IF NOT [%2] == [] SET TAG=%2
docker build -t workflowmanager.azurecr.io/workflow-manager-%SERVICE_NAME%-service-api:%TAG% -f Services/%SERVICE_NAME%Service/WorkflowManager.%SERVICE_NAME%Service.API/Dockerfile .
docker push workflowmanager.azurecr.io/workflow-manager-%SERVICE_NAME%-service-api:%TAG%
cd %WORKSPACE%