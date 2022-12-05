FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackendSolidario/BackendSolidario.csproj", "BackendSolidario/"]
RUN dotnet restore "BackendSolidario/BackendSolidario.csproj"
COPY . .
WORKDIR "/src/BackendSolidario"
RUN dotnet build "BackendSolidario.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendSolidario.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendSolidario.dll"]
