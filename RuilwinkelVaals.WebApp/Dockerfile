#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0.100 AS build
WORKDIR /src
COPY ["RuilwinkelVaals.WebApp/RuilwinkelVaals.WebApp.csproj", "RuilwinkelVaals.WebApp/"]
RUN dotnet restore "RuilwinkelVaals.WebApp/RuilwinkelVaals.WebApp.csproj"
COPY . .
WORKDIR "/src/RuilwinkelVaals.WebApp"
RUN dotnet build "RuilwinkelVaals.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RuilwinkelVaals.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RuilwinkelVaals.WebApp.dll"]