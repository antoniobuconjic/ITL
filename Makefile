# Makefile

# Variables
DOCKER_COMPOSE_FILE=docker-compose.yml
SQL_CONTAINER_NAME=sqlserver
SQL_IMAGE=mcr.microsoft.com/mssql/server:2019-latest
SQL_SA_PASSWORD=YourStrong!Passw0rd
SQL_PORT=1433

run-backend:
	dotnet run --project Backend/src/ITL.API/ITL.API.csproj

docker-sql-setup:
	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$(SQL_SA_PASSWORD)" \
		-p $(SQL_PORT):1433 --name $(SQL_CONTAINER_NAME) \
		-d $(SQL_IMAGE)

docker-sql-down:
	docker stop $(SQL_CONTAINER_NAME) && docker rm $(SQL_CONTAINER_NAME)

migrate:
	dotnet ef database update --project Backend/src/ITL.Infrastructure --startup-project Backend/src/ITL.API

add-migration:
	dotnet ef migrations add $(name) --project Backend/src/ITL.Infrastructure --startup-project Backend/src/ITL.API

remove-migration:
	dotnet ef migrations remove --project Backend/src/ITL.Infrastructure/ITL.Infrastructure.csproj

clean:
	dotnet clean

restore:
	dotnet restore

build:
	dotnet build

test:
	dotnet test

.PHONY: run-backend docker-sql-setup docker-sql-down migrate add-migration remove-migration clean restore build test
