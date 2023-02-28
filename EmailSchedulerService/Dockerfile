FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY FutureMe.sln .
COPY ["EmailSchedulerService/EmailSchedulerService.csproj", "EmailSchedulerService/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]


RUN dotnet restore "EmailSchedulerService/EmailSchedulerService.csproj"

COPY . .
WORKDIR "/src/EmailSchedulerService"
RUN dotnet build "EmailSchedulerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSchedulerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSchedulerService.dll"]
