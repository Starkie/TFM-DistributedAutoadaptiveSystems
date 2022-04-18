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
        ProjectName = "Climatisation.Monitor.Service"
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
        ProjectName = "Climatisation.Rules.Service"
    }
)

$PublishPath = Join-Path ($PSScriptRoot) "publish"

if (Test-Path $PublishPath)
{
    Remove-Item -Recurse $PublishPath
}

mkdir $PublishPath

foreach ($project in $Projects) {
    Build-Project $project $PublishPath
}

cp "docker-compose.yml" "$PublishPath/docker-compose.yml"

$PrometheusConfigPath = "$HOME/.prometheus"

if (-not (Test-Path $PrometheusConfigPath))
{
    New-Item -ItemType Directory $PrometheusConfigPath
}

cp (Join-Path $PSScriptRoot "config/prometheus.yml") "$PrometheusConfigPath/prometheus.yml"

# Start the compose.
docker-compose -f ./publish/docker-compose.yml up --build
