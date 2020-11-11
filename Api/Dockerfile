#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/Velvetech.Api.csproj", "Api/"]
COPY ["Shared/Velvetech.Shared.csproj", "Shared/"]
COPY ["Data/Velvetech.Data.csproj", "Data/"]
COPY ["Domain/Velvetech.Domain.csproj", "Domain/"]
RUN dotnet restore "Api/Velvetech.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Velvetech.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Velvetech.Api.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Velvetech.Api.dll"]
