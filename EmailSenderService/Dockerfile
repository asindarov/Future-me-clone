FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EmailSenderService/EmailSenderService.csproj", "EmailSenderService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "EmailSenderService/EmailSenderService.csproj"
COPY . .
WORKDIR "/src/EmailSenderService"
RUN dotnet build "EmailSenderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSenderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSenderService.dll"]
