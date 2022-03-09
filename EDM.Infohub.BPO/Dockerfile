FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["EDM.Infohub.BPO.csproj", ""]
RUN dotnet restore "./EDM.Infohub.BPO.csproj"
COPY . .
RUN rm -rf /src/appdynamics
WORKDIR "/src/."
RUN dotnet build "EDM.Infohub.BPO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EDM.Infohub.BPO.csproj" -c Release -o /app/publish

FROM base AS final

#RUN mkdir -p /opt/appdynamics/dotnet
#ADD /AppDynamics/libappdprofiler.so /opt/appdynamics/dotnet/
#ADD /AppDynamics/AppDynamics.Agent.netstandard.dll /opt/appdynamics/dotnet/
#ADD /AppDynamics/AppDynamicsConfig.json /opt/appdynamics/dotnet/
#ENV APPDYNAMICS_AGENT_REUSE_NODE_NAME=true
#
#ENV CORECLR_PROFILER="{57e1aa68-2229-41aa-9931-a6e93bbc64d8}"
#ENV CORECLR_ENABLE_PROFILING="1"
#ENV CORECLR_PROFILER_PATH="/opt/appdynamics/dotnet/libappdprofiler.so"

WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ=America/Sao_Paulo
ENTRYPOINT ["dotnet", "EDM.Infohub.BPO.dll"]