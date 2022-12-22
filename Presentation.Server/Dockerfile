#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation.Server/Presentation.Server.csproj", "Presentation.Server/"]
RUN dotnet restore "Presentation.Server/Presentation.Server.csproj"
COPY . .
WORKDIR "/src/Presentation.Server"
RUN dotnet build "Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.Server.dll"]