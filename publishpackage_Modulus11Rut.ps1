# Fetch the API key from the environment variable
$apiKey = $env:NUGET_API_KEY
$version = "1.1.0"

# Check if the API key is present
if (-not $apiKey) {
    Write-Host "Error: NUGET_API_KEY environment variable not set."
    exit 1
}

# Construct the nuget push command with the API key
# $nugetPushCommand = "dotnet nuget push ./bin/Release/Sur.Modulus11.Rut.1.0.2.nupkg --api-key $apiKey --source https://api.nuget.org/v3/index.json"

$nugetPushCommand = "dotnet nuget push ./Modulus.11.Rut/bin/Release/Sur.Modulus11.Rut.$version.nupkg --api-key $apiKey --source https://api.nuget.org/v3/index.json"

Write-Host "Executing command: $nugetPushCommand"

# Execute the nuget push command
Invoke-Expression -Command $nugetPushCommand