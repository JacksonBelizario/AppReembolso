FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore App.Web.csproj

COPY . ./
RUN dotnet publish App.Web.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime

WORKDIR /app

# Expose port 80 to your local machine so you can access the app.
EXPOSE 80

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "App.Web.dll"]