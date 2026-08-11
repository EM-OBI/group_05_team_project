FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY fims.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish fims.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000}
EXPOSE 10000
ENTRYPOINT ["dotnet", "fims.dll"]
