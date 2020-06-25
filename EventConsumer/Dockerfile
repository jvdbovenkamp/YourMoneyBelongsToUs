FROM mcr.microsoft.com/dotnet/core/runtime:3.1-bionic AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["EventConsumer/EventConsumer.csproj", "EventConsumer/"]
RUN dotnet restore "EventConsumer/EventConsumer.csproj" -r linux-x64
COPY . .
WORKDIR "/src/EventConsumer"
RUN dotnet build "EventConsumer.csproj" -c Release -o /app  -r linux-x64

FROM build AS publish
RUN dotnet publish "EventConsumer.csproj" -c Release -o /app  -r linux-x64 --self-contained false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventConsumer.dll"]