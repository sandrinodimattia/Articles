param([string]$ConnectionString = $(throw "The ConnectionString parameter is required."), 
      [string]$DatabaseName = $(throw "The DatabaseName parameter is required."),
      [string]$OutputFile = $(throw "The OutputFile parameter is required."), 
      [string]$SqlInstallationFolder = "C:\Program Files (x86)\Microsoft SQL Server")
      
# Load DAC assembly.
$DacAssembly = "$SqlInstallationFolder\110\DAC\bin\Microsoft.SqlServer.Dac.dll"
Write-Host "Loading Dac Assembly: $DacAssembly"
Add-Type -Path $DacAssembly
Write-Host "Dac Assembly loaded."

# Initialize Dac service.
$now = $(Get-Date).ToString("HH:mm:ss")
$Services = new-object Microsoft.SqlServer.Dac.DacServices $ConnectionString
if ($Services -eq $null)
{
    exit
}

# Start the actual export.
Write-Host "Starting backup at $DatabaseName at $now"
$Watch = New-Object System.Diagnostics.StopWatch
$Watch.Start()
$Services.ExportBacpac($OutputFile, $DatabaseName)
$Watch.Stop()
Write-Host "Backup completed in" $Watch.Elapsed.ToString()
