# 使用官方 .NET SDK 作為建置階段
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 設定工作目錄
WORKDIR /src

# 複製解決方案檔案和每個專案的 .csproj
COPY *.sln ./
COPY Application/*.csproj ./Application/
COPY Common/*.csproj ./Common/
COPY Domain/*.csproj ./Domain/
COPY DataSource/*.csproj ./DataSource/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY EcommerceBackend/*.csproj ./EcommerceBackend/


# 還原相依性
RUN dotnet restore ./EcommerceBackend/EcommerceBackend.csproj

# 複製所有檔案
COPY . .

# 建置 Web API 專案
WORKDIR /src/EcommerceBackend
RUN dotnet publish -c Release -o /app

# 使用官方 ASP.NET 執行階段作為執行環境
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# 設定工作目錄
WORKDIR /app

# 從建置階段複製已建置的檔案
COPY --from=build /app .

# 暴露應用程式埠
EXPOSE 8080

# 設定啟動命令
ENTRYPOINT ["dotnet", "EcommerceBackend.dll", "--urls", "http://0.0.0.0:8080"]