SET WORKSPACE=%CD%
cd %~dp0
docker-compose build && PAUSE
cd %WORKSPACE%