FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Source/Common/RecipeBook.API/RecipeBook.API.csproj", "Source/Common/RecipeBook.API/"]
COPY ["Source/Shared/RecipeBook.Exceptions/RecipeBook.Exceptions.csproj", "Source/Shared/RecipeBook.Exceptions/"]
COPY ["Source/Common/RecipeBook.Application/RecipeBook.Application.csproj", "Source/Common/RecipeBook.Application/"]
COPY ["Source/Common/RecipeBook.Domain/RecipeBook.Domain.csproj", "Source/Common/RecipeBook.Domain/"]
COPY ["Source/Common/RecipeBook.Infrastructure/RecipeBook.Infrastructure.csproj", "Source/Common/RecipeBook.Infrastructure/"]
RUN dotnet restore "Source/Common/RecipeBook.API/RecipeBook.API.csproj"
COPY . .
WORKDIR "/src/Source/Common/RecipeBook.API"
RUN dotnet build "RecipeBook.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecipeBook.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeBook.API.dll"]