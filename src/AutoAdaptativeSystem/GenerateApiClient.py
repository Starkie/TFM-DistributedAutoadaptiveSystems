#!/bin/python3

from io import FileIO
from pathlib import Path
import os
import requests

codegen_library_version = "5.3.0"
codegen_library_name = "openapi-generator-cli-5.3.0.jar" 

def generate_api_spec(project_name, project_path, api_name):
    print(f"Building '{project_path}'...")
    result = os.system(f"dotnet build {project_path} -c Release")

    if result != 0:
        return False

    print(f"Generating OpenApi spec of '{project_path}'...")

    parent_folder = Path(project_path).parent
    dll_path = parent_folder.joinpath(f"bin/Release/net5.0/{project_name}.dll")

    openapi_spec_path = f"{project_name}-OpenAPISpec.json"
    openapi_spec_path = parent_folder.joinpath(openapi_spec_path).absolute()

    result = os.system(f"swagger tofile --output {openapi_spec_path} \"{dll_path}\" \"{api_name}\"")

    print(f"Generated '{openapi_spec_path}'")
    
    return openapi_spec_path

def download_codegen_jar():
    print(f"Downloading OpenAPI Codegen version '{codegen_library_version}'...")

    if Path(codegen_library_name).exists():
        return Path(codegen_library_name).absolute()
    
    codegen_jar_descriptor = requests.get(f"https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/{codegen_library_version}/{codegen_library_name}")
    
    fd = os.open(codegen_library_name, os.O_WRONLY | os.O_CREAT)
    os.write(fd, codegen_jar_descriptor.content)

    absolute_path = Path(codegen_library_name).absolute()
    print(f"Downloaded file '{absolute_path}'...")

    return absolute_path

def generate_api_client(codegen_path, openapi_path, project_name, output_path):
    print(f"Generating API Client from '{openapi_path}' in '{output_path}'...")

    os.system(f"java -jar {codegen_path} generate -i {openapi_path} -o {output_path} -g csharp-netcore --library httpclient --additional-properties=packageName={project_name}.ApiClient,netCoreProjectFile=true,targetFramework=net5.0,sourceFolder=\"\"")

project_list = [
    { "path": "./RoomMonitor/RoomMonitor.csproj", "name": "RoomMonitor", "api_name": "v1", "output_path": "./RoomMonitor/ApiClient" },
    { "path": "./MonitoringModule/MonitoringModule.csproj", "name": "MonitoringModule", "api_name": "v1", "output_path": "./MonitoringModule/ApiClient" }
]

codegen_path = download_codegen_jar()

for project in project_list:
    openapi_spec_path = generate_api_spec(project["name"], project["path"], project["api_name"])

    generate_api_client(codegen_path, openapi_spec_path, project["name"], project["output_path"])