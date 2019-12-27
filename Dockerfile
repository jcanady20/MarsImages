# Base image with .Net core 3.0 Runtime Alpine linux 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-alpine3.9 AS base
WORKDIR /app
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add icu-libs

# Build image with .Net core 3.0 SDK Alphine linux
FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-alpine3.9 AS build
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
# Install Node JS
RUN apk add --update nodejs npm
RUN mkdir MarsImages
WORKDIR /MarsImages
COPY ./src/MarsImages/ .
ARG build_version
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore "MarsImages.csproj" -c Release /p:Version=${build_version} /p:AssemblyVersion=${build_version} /p:FileVersion=${build_version} -o /bin/release

# publish step
FROM build AS publish
ARG build_version
RUN dotnet publish "MarsImages.csproj" -c Release /p:Version=${build_version} /p:AssemblyVersion=${build_version} /p:FileVersion=${build_version} -o /bin/publish

# final image
FROM base AS final
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:5002
EXPOSE 5002
WORKDIR /app
COPY --from=publish /bin/publish .
ENTRYPOINT [ "dotnet", "MarsImages.dll" ]