FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MinhCoach.sln ./

COPY MinhCoach.Api/MinhCoach.Api.csproj MinhCoach.Api/
COPY MinhCoach.Contracts/MinhCoach.Contracts.csproj MinhCoach.Contracts/
COPY MinhCoach.Infra/MinhCoach.Infra.csproj MinhCoach.Infra/
COPY MinhCoach.App/MinhCoach.App.csproj MinhCoach.App/
COPY MinhCoach.Domain/MinhCoach.Domain.csproj MinhCoach.Domain/

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore MinhCoach.sln

COPY MinhCoach.Api/ MinhCoach.Api/
COPY MinhCoach.Contracts/ MinhCoach.Contracts/
COPY MinhCoach.Infra/ MinhCoach.Infra/
COPY MinhCoach.App/ MinhCoach.App/
COPY MinhCoach.Domain/ MinhCoach.Domain/

WORKDIR /src/MinhCoach.Api
RUN dotnet publish MinhCoach.Api.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
RUN addgroup --system --gid 1001 csharp 
RUN adduser --system --uid 1001  dotnet
COPY --chown=dotnet:csharp --chmod=750 --from=build /app/publish /app
USER dotnet
EXPOSE 8080
ENTRYPOINT ["dotnet", "MinhCoach.Api.dll"]