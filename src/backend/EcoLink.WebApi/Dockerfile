FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./src/backend/EcoLink.WebApi/EcoLink.WebApi.csproj ./src/backend/EcoLink.WebApi/
RUN dotnet restore ./src/backend/EcoLink.WebApi/EcoLink.WebApi.csproj

COPY . .
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "EcoLink.WebApi.dll"]
