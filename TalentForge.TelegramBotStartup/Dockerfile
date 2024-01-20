FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TalentForge/TalentForge.csproj", "TalentForge/"]
RUN dotnet restore "TalentForge/TalentForge.csproj"
COPY . .
WORKDIR "/src/TalentForge"
RUN dotnet build "TalentForge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TalentForge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentForge.dll"]
