FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /app

# RUN dotnet dev-certs https --clean \
#     && dotnet dev-certs https --trust

# Set up the container for HTTPS
ENV ASPNETCORE_URLS=http://+:5062
# ENV ASPNETCORE_HTTPS_PORT=5062

# Copy the published application to the container
COPY --from=build /app/out ./
# ENTRYPOINT ["dotnet", "user.dll", "--launch-profile", "https"]
ENTRYPOINT ["dotnet", "user.dll"]