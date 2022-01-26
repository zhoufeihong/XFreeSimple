#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["XFree.Simple.Web/XFreeSimpleService.Host.csproj", "XFree.Simple.Web/"]
COPY ["XFree.Simple.HttpApi/5.XFree.Simple.HttpApi.csproj", "XFree.Simple.HttpApi/"]
COPY ["XFree.Simple.Application.Contracts/3.XFree.Simple.Application.Contracts.csproj", "XFree.Simple.Application.Contracts/"]
COPY ["XFree.Simple.Domain.Shared/0.XFree.Simple.Domain.Shared.csproj", "XFree.Simple.Domain.Shared/"]
COPY ["XFree.Simple.Application/4.XFree.Simple.Application.csproj", "XFree.Simple.Application/"]
COPY ["XFree.Simple.Domain/1.XFree.Simple.Domain.csproj", "XFree.Simple.Domain/"]
COPY ["XFree.Simple.EntityFrameworkCore/2.XFree.Simple.EntityFrameworkCore.csproj", "XFree.Simple.EntityFrameworkCore/"]
RUN dotnet restore "XFree.Simple.Web/XFreeSimpleService.Host.csproj"
COPY . .
WORKDIR "/src/XFree.Simple.Web"
RUN dotnet build "XFreeSimpleService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XFreeSimpleService.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XFreeSimpleService.Host.dll"]