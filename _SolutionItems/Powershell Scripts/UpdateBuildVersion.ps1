param
  (
    [Parameter(Position=0, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$csproj,
    [Parameter(Position=1, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$version
  )

if([string]::IsNullOrWhiteSpace($csproj)){
    throw "Please specify the path to the csproj file"
}
if([string]::IsNullOrWhiteSpace($version)){
    throw "Please specify the version number to be used in the format of X.Y.Z.r"
}

Write-Host "Stamping $($csproj) with version number $($version)"

$file = Resolve-Path $csproj

$xml = [xml](get-content ($file))

$versionNode = $xml.Project.PropertyGroup.AssemblyVersion
if ($versionNode -eq $null) {
    # create version node if it doesn't exist
    $versionNode = $xml.CreateElement("AssemblyVersion")
    $xml.Project.PropertyGroup.AppendChild($versionNode)
    Write-Host "AssemblyVersion XML tag added to $($csproj)"
}
$xml.Project.PropertyGroup.AssemblyVersion = $version


$xml.Save($file)