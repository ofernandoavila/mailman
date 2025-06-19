<h1 align="center">✉️ Mailman </h1>
<div align="center">
 
<a href="">![.NET Core](https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)</a>
<a href="">![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white)</a>
<a href="">![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)</a>

</div>

<div align="center">

<h4>Made with ❤️ by <a href="https://linkedin.com/in/ofernandoavila">Fernando Avila</a> </h4>
 
</div>

<br>

<h2 align="center">📌 Project Overview</h2>

<p align="center">
<b>Mailman</b> is a platform designed to manage and send e-mails efficiently. You can send e-mail to the queue and send through <b>Twilio SendGrid</b>. Read this doc to get information about build, deploy and usage. <br>All information you may need, you can find here.
</p>

<br>

## 🚀 Technologies Used

- **Backend**: .NET Core
- **Database**: PostgreSQL

---

## 🔥 Features

✔️ Generate API Key and create custom configuration to an application <br>
✔️ Send through <b>Twilio SendGrid</b>

---

## 🛠️ First Build

Clone the repository:
```sh
git clone https://github.com/ofernandoavila/mailman mailman
cd mailman
```

First we need to create a container for the Postgres database, run the following command in your terminal:

> If you want a different password for your database, change the GUID in "POSTGRES_PASSWORD" for your password and change in `appsettings.debug.json`.

```sh
docker run -d \
  --name MailmanDB \
  -p 5432:5432 \
  -e POSTGRES_USER=user.admin \
  -e POSTGRES_PASSWORD=3a4d395e-24de-41c8-8f6f-403baa654819 \
  -e POSTGRES_DATABASE=Mailman \
  --health-cmd="pg_isready -U user.admin -d Mailman" \
  --health-interval=5s \
  --health-timeout=5s \
  --health-retries=5 \
  bitnami/postgresql:latest
```
Ensure PostgreSQL is running and configured properly.

Run the application:
```sh
cd ./src/Ofernandoavila.Mailman.Api
dotnet run
```

---
<br>

## 📌 .NET Core Guide

This document provides essential information for setting up and running a .NET Core application using CLI commands.


### 📌 Requirements

Before running the project, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (if using a database)
- [Docker](https://www.docker.com/) (optional, if using containers)

To verify the .NET SDK installation, run:

```sh
 dotnet --version
```

### 📌 Running the Application Locally

To run the application using the .NET CLI, execute:

```sh
dotnet run
```
### 📌 Stopping the Application

To stop the running application, press `CTRL + C` in the terminal where it's running.

---

### 📌 Managing Migrations

### Add a Migration

To create a new migration:

```sh
dotnet ef migrations add <MIGRATION NAME> --project ..\Ofernandoavila.FoodDelivery.Data\Ofernandoavila.FoodDelivery.Data.csproj --startup-project . --context AppDbContext
```

### Remove Last Migration

If you need to undo the last migration:

```sh
dotnet ef migrations remove --project ..\Ofernandoavila.FoodDelivery.Data\Ofernandoavila.FoodDelivery.Data.csproj --startup-project . --context AppDbContext
```

### Apply Migrations

To apply migrations and update the database:

```sh
dotnet ef migrations update --project ..\Ofernandoavila.FoodDelivery.Data\Ofernandoavila.FoodDelivery.Data.csproj --startup-project . --context AppDbContext
```
<br>

## 📌 Docker Container Guide

This document provides the main commands for managing Docker containers for **.NET Core + PostgreSQL**.

---

### 📌 Start the Containers
To start the containers (API .NET Core and PostgreSQL):

```sh
docker-compose up -d
```

---

## 📌 Rebuild and Restart Containers
If there are code changes and you need to rebuild the containers:

```sh
docker-compose up -d --build
```

---

## 📌 Stop Containers (without removing them)
To stop container execution without deleting them:

```sh
docker-compose stop
```
---

## 📌 Start Stopped Containers
If you have stopped the containers and want to start them again:

```sh
docker-compose start
```

---

## 📌 Remove Containers and Volumes
If you need to completely remove the containers and volumes (such as stored databases):

```sh
docker-compose down -v
```

---

## 📌 List Running Containers
To check which containers are running:

```sh
docker ps
```

To list all containers, including stopped ones:

```sh
docker ps -a
```

---

## 📌 Check Container Logs
To view logs for the API (.NET Core) or the database (PostgreSQL):

```sh
docker logs -f minha_api
```

```sh
docker logs -f postgres_container
```
---

## 📌 Access a Container's Terminal
To access a container's shell:

```sh
docker exec -it postgres_container bash
```

Or for a .NET Core container:

```sh
docker exec -it minha_api bash
```
---

## 📌 Check Database Status (PostgreSQL)
To verify if the database is running correctly:

```sh
docker exec -it postgres_container psql -U $POSTGRES_USER -d $POSTGRES_DB
```

---

## 📌 Remove a Specific Container
If you want to remove only the API or PostgreSQL container, run:

```sh
docker rm -f minha_api
```

```sh
docker rm -f postgres_container
```
---

## 📌 Remove All Docker Images
If you need to clean up all Docker images on the system:

```sh
docker rmi $(docker images -q) -f
```

## Refferences

#### 
[Certificates in .NET Core on Linux and Docker](https://medium.com/workleap/certificates-in-net-core-on-linux-and-docker-29b3d5f09cd6)

[dotnet new console not working](https://askubuntu.com/questions/1469405/dotnet-new-console-not-working-ubuntu-23-04)