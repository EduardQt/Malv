FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Malv.TypeWriter/Malv.TypeWriter.csproj", "Malv.TypeWriter/"]
RUN dotnet restore "Malv.TypeWriter/Malv.TypeWriter.csproj"
COPY . .
WORKDIR "/src/Malv.TypeWriter"
RUN dotnet build "Malv.TypeWriter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Malv.TypeWriter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Malv.TypeWriter.dll"]
