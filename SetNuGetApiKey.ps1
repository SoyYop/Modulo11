param(
    [string]$apiKey
)

# Check if the apiKey parameter is provided
if (-not $apiKey) {
    Write-Host "Usage: SetNuGetApiKey.ps1 -apiKey <YourApiKey>"
    exit 1
}

# Set the NUGET_API_KEY environment variable
$env:NUGET_API_KEY = $apiKey

# Print a confirmation message
Write-Host "NUGET_API_KEY environment variable set to '$apiKey'"
