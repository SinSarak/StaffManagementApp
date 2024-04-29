# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the entire solution into the container
COPY . .

# Restore dependencies
RUN dotnet restore

# Build the app
RUN dotnet build "StaffManagementApp.csproj" -c Release -o /app/build

# Publish the app
RUN dotnet publish "StaffManagementApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create a new image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy the published app from the build environment
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "StaffManagementApp.dll"]
