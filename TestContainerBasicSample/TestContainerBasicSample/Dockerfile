FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestContainerBasicSample/TestContainerBasicSample.csproj", "TestContainerBasicSample/"]
RUN dotnet restore "TestContainerBasicSample/TestContainerBasicSample.csproj"
COPY . .
WORKDIR "/src/TestContainerBasicSample"

#RUN dotnet build "TestContainerBasicSample.csproj" -c Release -o /app/build
#
#RUN dotnet test "TestContainerBasicSample.csproj" -c Release