#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/VaccinationCard/RedisExample.VaccinationCard.WebApi/RedisExample.VaccinationCard.WebApi.csproj", "src/VaccinationCard/RedisExample.VaccinationCard.WebApi/"]
RUN dotnet restore "src/VaccinationCard/RedisExample.VaccinationCard.WebApi/RedisExample.VaccinationCard.WebApi.csproj"
COPY . .
WORKDIR "/src/src/VaccinationCard/RedisExample.VaccinationCard.WebApi"
RUN dotnet build "RedisExample.VaccinationCard.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisExample.VaccinationCard.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisExample.VaccinationCard.WebApi.dll"]