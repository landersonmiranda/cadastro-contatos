# Expor as portas que o cont�iner usar�
EXPOSE 8080
EXPOSE 8081

# Est�gio base com a imagem do ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app

# Est�gio de build com o SDK .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o arquivo .csproj e restaurar depend�ncias
COPY ["PrimeiraCrudMVC.csproj", "./"]
RUN dotnet restore "./PrimeiraCrudMVC.csproj"

# Copiar o restante dos arquivos e construir o projeto
COPY . . 
RUN dotnet build "./PrimeiraCrudMVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Est�gio de publica��o
FROM build AS publish
RUN dotnet publish "./PrimeiraCrudMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Est�gio final com a imagem base para execu��o
FROM base AS final
WORKDIR /app

# Copiar os arquivos publicados do est�gio anterior
COPY --from=publish /app/publish .

# Configurar o ponto de entrada
ENTRYPOINT ["dotnet", "PrimeiraCrudMVC.dll"]
