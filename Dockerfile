FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
COPY ./MinhCoach.Api/MinhCoach.Api.csproj Api/
COPY ./MinhCoach.Contracts/MinhCoach.Contracts.csproj Contracts/
COPY ./MinhCoach.Infra/MinhCoach.Infra.csproj Infra/
COPY ./MinhCoach.App/MinhCoach.App.csproj App/
COPY ./MinhCoach.Domain/MinhCoach.Domain.csproj Domain/

RUN dotnet restore "MinhCoach.sln"

COPY ./MinhCoach.Api/ Api/
COPY ./MinhCoach.Contracts/ Contracts/
COPY ./MinhCoach.Infra/ Infra/
COPY ./MinhCoach.App/ App/
COPY ./MinhCoach.Domain/ Domain/

WORKDIR /src/Api
RUN dotnet build "MinhCoach.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinhCoach.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
RUN addgroup --system --gid 1001 dotnet
RUN adduser --system --uid 1001 csharp
COPY --from=publish /app/publish .
RUN chown -R csharp:dotnet /app && chmod -R 750 /app
USER csharp
EXPOSE 8080
ENTRYPOINT ["dotnet", "MinhCoach.Api.dll"]
