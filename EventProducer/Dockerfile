FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["EventProducer/EventProducer.csproj", "EventProducer/"]
RUN dotnet restore "EventProducer/EventProducer.csproj" -r linux-x64
COPY . .
WORKDIR "/src/EventProducer"
RUN dotnet build "EventProducer.csproj" -c Release -o /app -r linux-x64

FROM build AS publish
RUN dotnet publish "EventProducer.csproj" -c Release -o /app -r linux-x64 --self-contained false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventProducer.dll"]