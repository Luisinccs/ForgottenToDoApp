<#
.SYNOPSIS
    Performs an exhaustive cleanup and rebuild for a .NET MAUI solution.
    This script is designed to resolve common build and runtime issues,
    especially unhandled exceptions during app startup.

.DESCRIPTION
    This script will:
    1. Close any running instances of the MAUI app.
    2. Perform a 'dotnet clean' on all specified projects.
    3. Manually remove 'bin' and 'obj' folders from specified projects.
    4. Clear all local NuGet caches.
    5. Clean and restore .NET MAUI workloads.
    6. Perform a full 'dotnet build' on the solution.

.NOTES
    - Run this script from the root directory of your solution (where the .sln file is).
    - Requires PowerShell 5.1 or newer.
    - Make sure to replace 'ForgottenToDoApp.MauiApp', 'ForgottenToDoApp.Core', and 'ForgottenToDoApp.Infrastructure'
      with the actual names of your projects if they are different.
.EXAMPLE
    .\clean-and-build.ps1
#>

# --- Configuration ---
$solutionName = "ForgottenToDoApp.sln"
# List your project folders here that contain bin/obj directories
$projectFolders = @(
    "ForgottenToDoApp.MauiApp",
    "ForgottenToDoApp.Core",
    "ForgottenToDoApp.Infrastructure"
)

# --- Functions ---
function Write-Log {
    param (
        [string]$Message,
        [string]$Level = "INFO" # INFO, WARNING, ERROR
    )
    $timestamp = (Get-Date).ToString("yyyy-MM-dd HH:mm:ss")
    Write-Host "[$timestamp] [$Level] $Message"
}

function Check-Command {
    param (
        [string]$Command,
        [string]$Message
    )
    Write-Log "Executing: $Command"
    Invoke-Expression $Command
    if ($LASTEXITCODE -ne 0) {
        Write-Log "ERROR: $Message (Exit Code: $LASTEXITCODE)" "ERROR"
        exit 1
    }
}

# --- Script Start ---
Write-Log "Starting exhaustive .NET MAUI cleanup and rebuild process..."

# 1. Terminate any running app instances (optional, but good practice)
Write-Log "Attempting to terminate existing app processes..."
try {
    Get-Process | Where-Object {$_.ProcessName -like "ForgottenToDoApp.MauiApp*"} | Stop-Process -Force -ErrorAction SilentlyContinue
    Write-Log "Terminated any running ForgottenToDoApp.MauiApp processes."
} catch {
    Write-Log "Could not terminate app processes: $_" "WARNING"
}

# 2. Perform dotnet clean on all specified projects
foreach ($project in $projectFolders) {
    if (Test-Path "$project") {
        Write-Log "Running 'dotnet clean' for $project..."
        Check-Command "dotnet clean $project" "Failed to clean project $project."
    } else {
        Write-Log "Project folder '$project' not found, skipping clean." "WARNING"
    }
}

# 3. Manually remove bin and obj folders
Write-Log "Manually removing 'bin' and 'obj' folders..."
foreach ($project in $projectFolders) {
    $binPath = Join-Path $project "bin"
    $objPath = Join-Path $project "obj"

    if (Test-Path $binPath) {
        Write-Log "Removing: $binPath"
        Remove-Item -Path $binPath -Recurse -Force -ErrorAction SilentlyContinue
    }
    if (Test-Path $objPath) {
        Write-Log "Removing: $objPath"
        Remove-Item -Path $objPath -Recurse -Force -ErrorAction SilentlyContinue
    }
}

# 4. Clear all local NuGet caches
Write-Log "Clearing all local NuGet caches..."
Check-Command "dotnet nuget locals all --clear" "Failed to clear NuGet caches."

# 5. Clean and restore .NET MAUI workloads
Write-Log "Cleaning .NET MAUI workloads (this might take a while)..."
Check-Command "dotnet workload clean" "Failed to clean .NET MAUI workloads."

Write-Log "Restoring .NET MAUI workloads (this might take a while)..."
Check-Command "dotnet workload restore" "Failed to restore .NET MAUI workloads."

# 6. Perform a full dotnet build on the solution
Write-Log "Performing a full 'dotnet build' on the solution..."
Check-Command "dotnet build $solutionName" "Solution build failed. Check for compilation errors above."

Write-Log "Process completed. If build failed, check logs for errors."
Write-Log "Please try to run your application in Visual Studio (F5) now."

# Optional: Keep the console open after execution
# Read-Host "Press Enter to exit..."