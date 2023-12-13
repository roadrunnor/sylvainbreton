@echo off
REM Stop and remove all containers and networks managed by Docker Compose
docker-compose down

REM Start the services defined in docker-compose.yml in detached mode
docker-compose up -d --build

REM Wait for the services to start up
timeout /t 5

REM Open the browser to the frontend service
start http://localhost:3000

