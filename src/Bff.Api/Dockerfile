FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Bff.Api/Bff.Api.csproj", "Bff.Api/"]
RUN dotnet restore "Bff.Api/Bff.Api.csproj"
COPY . .
WORKDIR "/src/src/Bff.Api"
RUN dotnet build "Bff.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bff.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Bff.Api.dll"]
