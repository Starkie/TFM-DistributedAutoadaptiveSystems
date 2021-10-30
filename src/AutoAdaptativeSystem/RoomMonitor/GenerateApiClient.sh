#!/bin/bash

dotnet build RoomMonitor.csproj -c Release

swagger tofile --output openapi.json "bin/Release/net5.0/RoomMonitor.dll" "v1"

wget https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/5.3.0/openapi-generator-cli-5.3.0.jar -O codegen-cli.jar
# wget https://oss.sonatype.org/content/repositories/snapshots/org/openapitools/openapi-generator-cli/6.0.0-SNAPSHOT/openapi-generator-cli-6.0.0-20211025.061654-22.jar -O codegen-cli.jar
# wget https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.29/swagger-codegen-cli-3.0.29.jar -O codegen-cli.jar

# openapi
java -jar codegen-cli.jar generate -i openapi.json -o ApiClient -g csharp-netcore -c config.yml --library httpclient
# swagger
# java -jar codegen-cli.jar generate -i openapi.json -o ApiClient -l csharp -c ApiClient/swagger-config.json