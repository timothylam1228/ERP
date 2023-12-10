# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
# Create /app
RUN dotnet publish "ERP.csproj" -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /source
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ERP.dll"]
EXPOSE 80