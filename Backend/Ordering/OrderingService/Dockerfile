FROM mcr.microsoft.com/dotnet/core/runtime:3.1-bionic AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Backend/Ordering/OrderingService/OrderingSerice.csproj", "OrderingService/"]
RUN dotnet restore "Backend/Ordering/OrderingService/OrderingSerice.csproj" -r linux-x64
COPY . .
WORKDIR "/src/OrderingService"
RUN dotnet build "OrderingService.csproj" -c Release -o /app  -r linux-x64

FROM build AS publish
RUN dotnet publish "OrderingService.csproj" -c Release -o /app  -r linux-x64 --self-contained false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderingService.dll"]