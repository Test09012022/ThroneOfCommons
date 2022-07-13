#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ThroneOfCommons.Mvc/ThroneOfCommons.Mvc.csproj", "ThroneOfCommons.Mvc/"]
COPY ["ThroneOfCommons.Core/ThroneOfCommons.Core.csproj", "ThroneOfCommons.Core/"]
RUN dotnet restore "ThroneOfCommons.Mvc/ThroneOfCommons.Mvc.csproj"
COPY . .
WORKDIR "/src/ThroneOfCommons.Mvc"
RUN dotnet build "ThroneOfCommons.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThroneOfCommons.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThroneOfCommons.Mvc.dll"]