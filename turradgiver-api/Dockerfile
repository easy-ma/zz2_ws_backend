FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dev
WORKDIR /src
COPY ["turradgiver-api/turradgiver-api.csproj", "turradgiver-api/"]
COPY ["data-access/data-access.csproj", "data-access/"]

# Commented because of the volume mount
# COPY . .

RUN dotnet tool install --global dotnet-ef
RUN export PATH="$PATH:/root/.dotnet/tools"

RUN dotnet restore "turradgiver-api/turradgiver-api.csproj"
WORKDIR "/src/turradgiver-api"

# by default the ip is 127.0.0.1 equivalent of localhost 
# which doesn't work when trying to access the api from the host
# have to be 0.0.0.0 could be set here or within the project file
CMD dotnet watch run --urls=http://0.0.0.0:5001/
