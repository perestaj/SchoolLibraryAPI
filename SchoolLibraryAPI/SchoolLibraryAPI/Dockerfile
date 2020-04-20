#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY SchoolLibraryAPI/SchoolLibraryAPI.csproj SchoolLibraryAPI/
COPY SchoolLibraryAPI.Common/SchoolLibraryAPI.Common.csproj SchoolLibraryAPI.Common/
COPY SchoolLibraryAPI.Core/SchoolLibraryAPI.Core.csproj SchoolLibraryAPI.Core/
COPY SchoolLibraryAPI.DAL/SchoolLibraryAPI.DAL.csproj SchoolLibraryAPI.DAL/
RUN dotnet restore "SchoolLibraryAPI/SchoolLibraryAPI.csproj"
COPY . .
WORKDIR "/src/SchoolLibraryAPI"
RUN dotnet build "SchoolLibraryAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchoolLibraryAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolLibraryAPI.dll"]
