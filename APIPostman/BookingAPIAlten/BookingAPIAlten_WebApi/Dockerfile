FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/BookingAPIAlten_WebApi/BookingAPIAlten_WebApi.csproj", "src/BookingAPIAlten_WebApi/"]
COPY ["src/BookingAPIAlten_Core/BookingAPIAlten_Core.csproj", "src/BookingAPIAlten_Core/"]

RUN dotnet restore "BookingAPIAlten_WebApi/BookingAPIAlten_WebApi.csproj"
COPY . .
WORKDIR "/src/BookingAPIAlten_WebApi"
RUN dotnet build "BookingAPIAlten_WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingAPIAlten_WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV DEFAULT_CONNECTION=Server=localhost;Database=reservation_db;Uid=root;Pwd=root;
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingAPIAlten_WebApi.dll"]