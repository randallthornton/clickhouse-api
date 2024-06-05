# Clickhouse API

Run clickhouse in a docker compose environment and connect to it with an asp.net api!

## Dependencies

1. [DBeaver](https://dbeaver.io/download/) for running `create-my-first-table.sql`  (or equivalent sql console)
1. Visual Studio

## Setup

### Launch the container

```powershell
podman compose up -d
```

or with docker

```powershell
docker-compose up -d
```

### Run the `create-my-first-table.sql` script with DBeaver

### Run the API with Visual Studio