FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./NotificationsApi.sln ./
COPY ./NotificationsApi/NotificationsApi.csproj ./NotificationsApi/
COPY ./NotificationsApi.Tests/NotificationsApi.Tests.csproj ./NotificationsApi.Tests/

RUN dotnet restore ./NotificationsApi/NotificationsApi.csproj
RUN dotnet restore ./NotificationsApi.Tests/NotificationsApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out NotificationsApi.Tests/NotificationsApi.Tests.csproj

CMD dotnet test
