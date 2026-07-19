FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo csproj y restaurar dependencias
COPY ["ZiurTest.Blazor/ZiurTest.Blazor.csproj", "ZiurTest.Blazor/"]
RUN dotnet restore "ZiurTest.Blazor/ZiurTest.Blazor.csproj"

# Copiar el codigo fuente completo y compilar
COPY . .
WORKDIR "/src/ZiurTest.Blazor"
RUN dotnet publish "ZiurTest.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen de runtime liviana
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ZiurTest.Blazor.dll"]
