FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
COPY ./src/OrgBloom.Bot/OrgBloom.Bot.csproj ./src/OrgBloom.Bot/
RUN dotnet restore ./src/OrgBloom.Bot/OrgBloom.Bot.csproj
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "OrgBloom.Bot.dll"]