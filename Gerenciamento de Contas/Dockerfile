#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gerenciamento de Contas/Gerenciamento de Contas.csproj", "Gerenciamento de Contas/"]
RUN dotnet restore "Gerenciamento de Contas/Gerenciamento de Contas.csproj"
COPY . .
WORKDIR "/src/Gerenciamento de Contas"
RUN dotnet build "Gerenciamento de Contas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gerenciamento de Contas.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gerenciamento de Contas.dll"]