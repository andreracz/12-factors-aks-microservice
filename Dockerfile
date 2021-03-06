FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY *.csproj *.sln ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "aks-12-factors-microservice.dll"]