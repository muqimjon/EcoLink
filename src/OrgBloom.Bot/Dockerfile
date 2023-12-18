# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory in the container
WORKDIR /App

# Copy the project file and restore dependencies
COPY ./src/OrgBloom.Bot/OrgBloom.Bot.csproj ./src/OrgBloom.Bot/
RUN dotnet restore ./src/OrgBloom.Bot/OrgBloom.Bot.csproj

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
ENTRYPOINT ["dotnet", "OrgBloom.Bot.dll"]
