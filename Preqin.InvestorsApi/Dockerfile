FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Preqin.InvestorsApi/Preqin.InvestorsApi.csproj", "Preqin.InvestorsApi/"]
RUN dotnet restore "Preqin.InvestorsApi/Preqin.InvestorsApi.csproj"
COPY . .
WORKDIR "/src/Preqin.InvestorsApi"
RUN dotnet build "Preqin.InvestorsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Preqin.InvestorsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Add health check
HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
    CMD curl -f http://localhost:80/ping || exit 1
    
ENTRYPOINT ["dotnet", "Preqin.InvestorsApi.dll"]
