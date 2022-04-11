FROM mcr.microsoft.com/dotnet/aspnet:6.0

ARG ROOMMONITOR_SERVICEURI

COPY app /app/

WORKDIR /app

ENV ROOMMONITOR__SERVICEURI "${ROOMMONITOR_SERVICEURI}"

ENTRYPOINT [ "dotnet", "Climatisation.Probes.Temperature.dll" ]
