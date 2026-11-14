# ==========================================
# TAHAP 1: Build Aplikasi
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file project (.csproj) terlebih dahulu untuk restore dependencies
COPY ["ApiSIS/ApiSIS.csproj", "ApiSIS/"]
RUN dotnet restore "ApiSIS/ApiSIS.csproj"

# Copy semua sisa file proyek
COPY . .
WORKDIR "/src/ApiSIS"

# Build dan Publish
RUN dotnet publish "ApiSIS.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# TAHAP 2: Runtime (Image yang akan dijalankan)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Cloudflare Containers mengharuskan aplikasi berjalan di port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Jalankan aplikasi (Pastikan nama DLL sesuai dengan nama project Anda)
ENTRYPOINT ["dotnet", "ApiSIS.dll"]