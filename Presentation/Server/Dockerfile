#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Presentation/Server/Velvetech.Presentation.Server.csproj", "Presentation/Server/"]
COPY ["Presentation/Shared/Velvetech.Presentation.Shared.csproj", "Presentation/Shared/"]
COPY ["Presentation/Client/Velvetech.Presentation.Client.csproj", "Presentation/Client/"]
COPY ["Data/Velvetech.Data.csproj", "Data/"]
COPY ["Domain/Velvetech.Domain.csproj", "Domain/"]
RUN dotnet restore "Presentation/Server/Velvetech.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/Presentation/Server"
RUN dotnet build "Velvetech.Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Velvetech.Presentation.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Velvetech.Presentation.Server.dll"]