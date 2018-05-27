FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /app

COPY ./src/*.csproj ./
RUN dotnet restore

COPY ./src/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "LocalstackS3SetupForNetCore.dll"]