<#
.SYNOPSIS
    .
.DESCRIPTION
    Copy this script to folder where your .sln file is and run it from there.
    This script will rename the base solution and codebase with the new project name and the solution name.
    New project and soulution names are provided via input parameters.
    Thera are two input parameters: source project name and the target project name.
    Source project name is 'Perficient' (from the code base) by default but it can be overriden via input parameter.
    Target project name parameter is the name that we want to use for our new project. This is mandatory parameter.
.PARAMETER SourceProject
    Name of the project in the source code base. Not mandatory filed, default is 'Perficient'.
.PARAMETER TargetProject
    Name of the new project and soulution. Mandatory field.
.EXAMPLE
    C:\PS>RenameSolution.ps1 -TargetProject <Target project name>
    C:\PS>RenameSolution.ps1 -SourceProject <Source project name> -TargetProject <Target project name>
.NOTES
    Author: Perficient
    Date:   2022    
#>
[CmdletBinding(DefaultParameterSetName='SourceProject')]
Param (
    [Parameter(Position=0,ParameterSetName="SourceProject",HelpMessage="Your Source Project name")]
    [string]$SourceProject = "Perficient",
    [Parameter(Mandatory=$True,Position=0,ParameterSetName="TargetProject",HelpMessage="Your Target Project name")]
    [string]$TargetProject = $(Read-Host -Prompt "Please enter a Target Project name")
)

function CheckVersion() {
    $major = $PSVersionTable.PSVersion.Major;
    $minor = $PSVersionTable.PSVersion.MinorRevision;

	Write-Host "Powershell version on this machine is ${major}.${minor}."; 
        
    if ( $major -lt 3) {
		Write-Host "To run the scaffold script, you must run Powershell version 3.0 or later." -foregroundcolor "red";
        Exit 
    }
}

function ReplaceInFilesRecurse($filter, $pattern, $replace) {
    $files = Get-ChildItem -filter $filter -exclude scaffold.ps1 -recurse | 
                Where-Object { (!$_.PSIsContainer) `
                        -and ($_.Name -notlike 'RenameSolution.ps1') `
                        -and ($_.extension -ne ".zip") `
                        -and ($_.extension -ne ".dll") `
                        -and ($_.extension -ne ".pdb") `
                        -and ($_.extension -ne ".png") `
                        -and ($_.extension -ne ".jpg") `
                        -and ($_.extension -ne ".csv") `
                        -and ($_.extension -ne ".mmdb") `
                        -and ($_.extension -ne ".svg") `
                        -and ($_.extension -ne ".exe") }

    foreach ($file in $files) 
    {
        (Get-Content $file.FullName) -replace $pattern, $replace | Out-File $file.FullName -Encoding ascii
    }
}

if($TargetProject -eq ""){
    $TargetProject = Read-Host -Prompt "Please enter a Solution name"
}

CheckVersion

$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Web

"   - Renaming files ... "

Get-ChildItem -filter "*$SourceProject*" -recurse | Rename-Item -NewName { $_.name -replace $SourceProject, $TargetProject }

"   - Replacing token in files ... "

ReplaceInFilesRecurse * $SourceProject $TargetProject

Write-Host "All Done." -foregroundcolor "green";
## Debug output
##Write-Host "sourceProject " $SourceProject " TargetProject " $TargetProject;