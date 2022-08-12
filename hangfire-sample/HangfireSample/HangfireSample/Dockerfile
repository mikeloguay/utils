#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HangfireSample/HangfireSample.csproj", "HangfireSample/"]
RUN dotnet restore "HangfireSample/HangfireSample.csproj"
COPY . .
WORKDIR "/src/HangfireSample"
RUN dotnet build "HangfireSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HangfireSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HangfireSample.dll"]