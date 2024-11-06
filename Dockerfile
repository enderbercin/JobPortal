## Temel imajı belirleyin
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 5001
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY ["JobPortal.API/JobPortal.API.csproj", "JobPortal.API/"]
#RUN dotnet restore "JobPortal.API/JobPortal.API.csproj"
#COPY . .
#WORKDIR "/src/JobPortal.API"
#RUN dotnet build "JobPortal.API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "JobPortal.API.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "JobPortal.API.dll"]
#

# Temel imajı belirleyin
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["JobPortal.API/JobPortal.API.csproj", "JobPortal.API/"]
RUN dotnet restore "JobPortal.API/JobPortal.API.csproj"
COPY . .
WORKDIR "/src/JobPortal.API"
RUN dotnet build "JobPortal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobPortal.API.csproj" -c Release -o /app/publish

# Veritabanı komutlarını çalıştırmak için EF Tool'u ekliyoruz
RUN dotnet tool install --global dotnet-ef

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobPortal.API.dll"]