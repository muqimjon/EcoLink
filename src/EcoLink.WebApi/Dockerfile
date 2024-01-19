# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory in the container
WORKDIR /App

# Copy the project file and restore dependencies
COPY ./src/EcoLink.WebApi/EcoLink.WebApi.csproj ./src/EcoLink.WebApi/
RUN dotnet restore ./src/EcoLink.WebApi/EcoLink.WebApi.csproj

# Copy the remaining files
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "EcoLink.WebApi.dll"]
