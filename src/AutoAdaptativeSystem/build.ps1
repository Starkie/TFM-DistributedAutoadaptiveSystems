function Build-Project([psobject]$Project, [string]$PublishPath)
{
    $PublishDirectory = Join-Path $PublishPath $Project.Name

    mkdir $PublishDirectory

    Write-Host $PublishDirectory

    dotnet publish "$($Project.Path)/$($Project.ProjectName).csproj" -c Release -r linux-x64 --no-self-contained -o "$PublishDirectory/app" -v m /p:clp=Summary -m

    cp (Join-Path $Project.Path "Dockerfile") "$PublishDirectory/Dockerfile"
}

$Projects = @(
    @{
        Name = "knowledge"
        Path = "AdaptionLoop/Knowledge"
        ProjectName = "Knowledge.Service"
    },
    @{
        Name = "monitoring"
        Path = "AdaptionLoop/Monitoring"
        ProjectName = "Monitoring.Service"
    },
    @{
        Name = "climatisation_monitor"
        Path = "Climatisation/Monitor"
        ProjectName = "Climatisation.Monitor"
    },
    @{
        Name = "temperatureprobe"
        Path = "Climatisation/Probes/Temperature"
        ProjectName = "Climatisation.Probes.Temperature"
    },
    @{
        Name = "analysis"
        Path = "AdaptionLoop/Analysis"
        ProjectName = "Analysis.Service"
    },
    @{
        Name = "planning"
        Path = "AdaptionLoop/Planning"
        ProjectName = "Planning.Service"
    },
    @{
        Name = "climatisation_rules"
        Path = "Climatisation/Rules"
        ProjectName = "Climatisation.Rules"
    }
)

$PublishPath = Join-Path (Get-Location) "publish"

if (Test-Path $PublishPath)
{
    Remove-Item -Recurse $PublishPath
}

mkdir $PublishPath

foreach ($project in $Projects) {
    Build-Project $project $PublishPath
}

cp "docker-compose.yml" "$PublishPath/docker-compose.yml"

# Start the compose.
docker-compose -f ./publish/docker-compose.yml up --build
