# Estágio base com a imagem do ASP.NET (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-buster-slim AS base
WORKDIR /app

# Estágio de build com o SDK .NET (Linux)
FROM mcr.microsoft.com/dotnet/sdk:8.0-buster-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o arquivo .csproj e restaurar dependências
COPY ["PrimeiraCrudMVC.csproj", "./"]
RUN dotnet restore "./PrimeiraCrudMVC.csproj"

# Copiar o restante dos arquivos e construir o projeto
COPY . .
RUN dotnet build "./PrimeiraCrudMVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "./PrimeiraCrudMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio final com a imagem base para execução (Linux)
FROM base AS final
WORKDIR /app

# Expor as portas que o contêiner usará
EXPOSE 8080
EXPOSE 8081

# Copiar os arquivos publicados do estágio anterior
COPY --from=publish /app/publish .

# Configurar o ponto de entrada
ENTRYPOINT ["dotnet", "PrimeiraCrudMVC.dll"]
