# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ["off-tone.WebApi/*.csproj", "off-tone.WebApi/"]
COPY ["off-tone.Persistence/*.csproj", "off-tone.Persistence/"]
COPY ["off-tone.Infrastructure/*.csproj", "off-tone.Infrastructure/"]
COPY ["off-tone.Domain/*.csproj", "off-tone.Domain/"]
COPY ["off-tone.Application/*.csproj", "off-tone.Application/"]

RUN dotnet restore "off-tone.WebApi/off-tone.WebApi.csproj"

COPY . ./

RUN dotnet publish "off-tone.WebApi/off-tone.WebApi.csproj" -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "out/off-tone.WebApi/off-tone.WebApi.dll"]