param
  (
    [Parameter(Position=0, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$ClientKey,
    [Parameter(Position=1, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$ClientSecret,
    [Parameter(Position=2, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$ProjectID,
    [Parameter(Position=3, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$ArtifactPath,
    [Parameter(Position=4, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [ValidateSet("Integration", "Preproduction", "Production")]
    [string]$TargetEnvironment
  )

#Checking for Non-Interactive Shell
function Test-IsNonInteractiveShell {
    if ([Environment]::UserInteractive) {
        foreach ($arg in [Environment]::GetCommandLineArgs()) {
            #Test each Arg for match of abbreviated '-NonInteractive' command.
            if ($arg -like '-NonI*') {
                return $true
            }
        }
    }

    return $false
}

function DeployToEnvironment {
    param (
        [Parameter(Position=0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientKey,
        [Parameter(Position=1, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientSecret,
        [Parameter(Position=2, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectID,
        [Parameter(Position=3, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ArtifactPath,
        [Parameter(Position=4, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [ValidateSet("Integration", "Preproduction", "Production")]
        [string]$TargetEnvironment,
        [Parameter(Position=5, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [ValidateSet($true, $false, 0, 1)]
        [bool]$UseMaintenancePage,
        [Parameter(Position=6)]
        [ValidateSet("ReadOnly", "ReadWrite")]
        [string]$ZeroDowntimeMode,
        [Parameter(Position=7)]
        [ValidateSet($true, $false, 0, 1)]
        [bool]$DirectDeploy = 0
    )

    Write-Host "--DEPLOY-TO-ENVIRONMENT--"

    #From the Artifact Path, getting the nupkg file
    $packagePath = Get-ChildItem -Path $ArtifactPath -Filter *.nupkg

    #If no NUPKG file is found, throw error and exit
    if($packagePath.Length -eq 0){
        throw "No NUPKG files were found. Please ensure you're passing the correct path."
    }

    Write-Host "Package Found. Name:" $packagePath.Name


    Write-Host "Setting up the deployment configuration"

    #Setting up the object for the EpiServer environment deployment
    $startEpiDeploymentSplat = @{
        DeploymentPackage = $packagePath.Name
        ProjectId = "$ProjectID"
        Wait = $false
        TargetEnvironment = "$TargetEnvironment"
        UseMaintenancePage = $UseMaintenancePage
        ClientSecret = "$ClientSecret"
        ClientKey = "$ClientKey"
    }

    if($DirectDeploy -eq $true){
        $startEpiDeploymentSplat.Add("DirectDeploy", $true)
    }


    if(![string]::IsNullOrWhiteSpace($ZeroDownTimeMode)){
        $startEpiDeploymentSplat.Add("ZeroDownTimeMode", $ZeroDownTimeMode)
    }

    Write-Host "Starting the Deployment to" $TargetEnvironment

    #Starting the Deployment
    $deploy = Start-EpiDeployment @startEpiDeploymentSplat

    $deployId = $deploy | Select -ExpandProperty "id"

    #Setting up the object for the EpiServer Deployment Updates
    $getEpiDeploymentSplat = @{
        ProjectId = "$ProjectID"
        ClientSecret = "$ClientSecret"
        ClientKey = "$ClientKey"
        Id = "$deployId"
    }

    #Setting up Variables for progress output
    $percentComplete = 0
    $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat | Select-Object -First 1
    $status = $currDeploy | Select -ExpandProperty "status"
    $exit = 0

    Write-Host "Percent Complete: $percentComplete%"
    Write-Output "##vso[task.setprogress value=$percentComplete]Percent Complete: $percentComplete%"

    #While the exit flag is not true
    while($exit -ne 1){
        #Get the current Deploy
        $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat | Select-Object -First 1

        #Set the current Percent and Status
        $currPercent = $currDeploy | Select -ExpandProperty "percentComplete"
        $status = $currDeploy | Select -ExpandProperty "status"

        #If the current percent is not equal to what it was before, send an update
        #(This is done this way to prevent a bunch of messages to the screen)
        if($currPercent -ne $percentComplete){
            Write-Host "Percent Complete: $currPercent%"
            Write-Output "##vso[task.setprogress value=$currPercent]Percent Complete: $currPercent%"
            #Set the overall percent complete variable to the new percent complete
            $percentComplete = $currPercent
        }

        #If the Percent Complete is equal to 100%, Set the exit flag to true
        if($percentComplete -eq 100){
            $exit = 1    
        }

        #If the status of the deployment is not what it should be for this scipt, Set the exit flad to true
        if($status -ne 'InProgress'){
            $exit = 1
        }

        #Wait 1 second between checks
        start-sleep -Milliseconds 1000
    }

    #If the status is set to Failed, throw an error
    if($status -eq "Failed"){
        Write-Output "##vso[task.complete result=Failed;]"
        throw "Deployment Failed. Errors: \n" + $deploy.deploymentErrors
    }

    Write-Host "Deployment Complete"

    #Set the Output variable for the Deployment ID, if needed
    Write-Output "##vso[task.setvariable variable=DeploymentId;]'$deployId'"
    Write-Verbose "Output Variable Created. Name: DeploymentId | Value: $deployId"
}

function UploadEpiPackage {
    param (
        [Parameter(Position=0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientKey,
        [Parameter(Position=1, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientSecret,
        [Parameter(Position=2, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectID,
        [Parameter(Position=3, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ArtifactPath
    )
    Write-Host "--UPLOAD-EPI-PACKAGE--"

    Write-Host "Searching for NUPKG file..."

    #From the Artifact Path, getting the nupkg file
    $packagePath = Get-ChildItem -Path $ArtifactPath -Filter *.nupkg

    #If no NUPKG file is found, throw error and exit
    if($packagePath.Length -eq 0){
        throw "No NUPKG files were found. Please ensure you're passing the correct path."
    }

    Write-Host "Package Found. Name: " $packagePath.Name

    Write-Host "Setting up the deployment configuration"
    #Setting up the object for the Epi Deployment. This is found in the PAAS portal settings.
    $getEpiDeploymentPackageLocationSplat = @{
        ClientKey = "$ClientKey"
        ClientSecret = "$ClientSecret"
        ProjectId = "$ProjectID"
    }
    
    Write-Host "Finding deployment location..."
    
    #Generating the Blob storage location URL to upload the package
    $packageLocation = Get-EpiDeploymentPackageLocation @getEpiDeploymentPackageLocationSplat
    
    Write-Host "Blob Location Found: " $packageLocation 
    Write-Host "Starting Upload..." 
    
    #Uploading the package to the Blob location
    $deploy = Add-EpiDeploymentPackage -SasUrl $packageLocation -Path $packagePath.FullName
    
    $deploy
    
    Write-Host "Upload Success. Files are ready for deploy into environments."
}

function CompleteOrResetDeployment {
    param (
        [Parameter(Position=0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientKey,
        [Parameter(Position=1, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ClientSecret,
        [Parameter(Position=2, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectID,
        [Parameter(Position=3, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [ValidateSet("Reset","Complete")]
        [string]$Action,
        [Parameter(Position=4)]
        [string]$DeploymentId,
        [Parameter(Position=5, Mandatory)]
        [ValidateSet("Integration", "Preproduction", "Production")]
        [ValidateNotNullOrEmpty()]
        [string]$targetEnvironment
    )

    Write-Host "--COMPLETE-OR-RESET-DEPLOYMENT--"

    #Setting up the object for the EpiServer Deployment Updates
    #If Deployment ID is specified, input it into the object.
    #Otherwise, create object without itif([string]::IsNullOrWhiteSpace($DeploymentId)){
    if([string]::IsNullOrWhiteSpace($DeploymentId)){
        $getEpiDeploymentSplat = @{
            ProjectId = "$ProjectID"
            ClientSecret = "$ClientSecret"
            ClientKey = "$ClientKey"
            id = ""
        }
    } else {
        $getEpiDeploymentSplat = @{
            ProjectId = "$ProjectID"
            ClientSecret = "$ClientSecret"
            ClientKey = "$ClientKey"
            id = "$DeploymentId"
        }
    }

    #Search for Deployment based on the provided object
    $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat  

    #If DeploymentID is not set, search for it using the previously found deployment
    if([string]::IsNullOrWhiteSpace($DeploymentId)){
        Write-Host "No Deployment ID Supplied. Searching for In-Progress Deployment..."
        $currDeploy = $currDeploy | Where-Object {$_.endTime -eq $null} | Where-Object {$_.parameters.targetEnvironment -eq $targetEnvironment -and $_.percentComplete -eq "100"} | Sort-Object -Property startTime -Descending | Select-Object -First 1
        $DeploymentId = $currDeploy | Select -ExpandProperty "id"
        Write-Host "Deployment ID Found: $DeploymentId"
        $getEpiDeploymentSplat.id = $DeploymentId
    }

    Write-Host "Setting up the deployment configuration"

    #Setting up the object for the EpiServer environment deployment completion
    $completeOrResetEpiDeploymentSplat = @{
        ProjectId = "$ProjectID"
        Id = "$DeploymentId"
        Wait = $false
        ClientSecret = "$ClientSecret"
        ClientKey = "$ClientKey"
    }

    Write-Host "Starting the Process. Action: $Action..."

    if($Action -eq "Complete"){
        #Starting the Deployment
        $deploy = Complete-EpiDeployment @completeOrResetEpiDeploymentSplat
    }
    else {
        #Starting the Deployment
        $deploy = Reset-EpiDeployment @completeOrResetEpiDeploymentSplat
    }


    #Setting up Variables for progress output
    $percentComplete = 0
    $status = $currDeploy | Select -ExpandProperty "status"
    $exit = 0

    Write-Host "Percent Complete: $percentComplete%"
    Write-Output "##vso[task.setprogress value=$percentComplete]Percent Complete: $percentComplete%"

    #While the exit flag is not true
    while($exit -ne 1){

        #Get the current Deploy
        $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat | Select-Object -First 1

        #Set the current Percent and Status
        $currPercent = $currDeploy | Select -ExpandProperty "percentComplete"
        $status = $currDeploy | Select -ExpandProperty "status"

        #If the current percent is not equal to what it was before, send an update
        #(This is done this way to prevent a bunch of messages to the screen)
        if($currPercent -ne $percentComplete){
            Write-Host "Percent Complete: $currPercent%"
            Write-Output "##vso[task.setprogress value=$currPercent]Percent Complete: $currPercent%"
            #Set the overall percent complete variable to the new percent complete
            $percentComplete = $currPercent
        }

        #If the Percent Complete is equal to 100%, Set the exit flag to true
        if($percentComplete -eq 100){
            $exit = 1    
        }

        #If the status of the deployment is not what it should be for this scipt, Set the exit flag to true
        if($action -eq 'Complete' -and $status -ne 'Completing'){
            $exit = 1
        }

        if($action -eq 'Reset' -and $status -ne 'Resetting'){
            $exit = 1
        }

        #Wait 1 second between checks
        start-sleep -Milliseconds 1000

    }

    Write-Host "Process Completed Successfully. Action: $Action"
}

function PromoteToEnvironment {
    param
    (
      [Parameter(Position=0, Mandatory)]
      [ValidateNotNullOrEmpty()]
      [string]$ClientKey,
      [Parameter(Position=1, Mandatory)]
      [ValidateNotNullOrEmpty()]
      [string]$ClientSecret,
      [Parameter(Position=2, Mandatory)]
      [ValidateNotNullOrEmpty()]
      [string]$ProjectID,
      [Parameter(Position=3, Mandatory)]
      [ValidateNotNullOrEmpty()]
      [ValidateSet("Integration", "Preproduction", "Production")]
      [string]$SourceEnvironment,
      [Parameter(Position=4, Mandatory)]
      [ValidateNotNullOrEmpty()]
      [ValidateSet("Integration", "Preproduction", "Production")]
      [string]$TargetEnvironment,
      [Parameter(Position=5)]
      [ValidateSet($true, $false, 0, 1)]
      [bool]$UseMaintenancePage = 0,
      [Parameter(Position=6)]
      [ValidateSet($true, $false, 0, 1)]
      [bool]$IncludeCode = 1,
      [Parameter(Position=7)]
      [ValidateSet($true, $false, 0, 1)]
      [bool]$IncludeBlobs = 0,
      [Parameter(Position=8)]
      [ValidateSet($true, $false, 0, 1)]
      [bool]$IncludeDb = 0,
      [Parameter(Position=9)]
      [ValidateSet('cms','commerce')]
      [String]$SourceApp,
      [Parameter(Position=10)]
      [ValidateSet("ReadOnly", "ReadWrite")]
      [String]$ZeroDowntimeMode,
      [Parameter(Position=11)]
      [ValidateSet($true, $false, 0, 1)]
      [bool]$DirectDeploy = 0
    )

    Write-Host "--PROMOTE-TO-ENVIRONMENT--"

    if($SourceEnvironment -eq $TargetEnvironment){
        throw "The source environment cannot be the same as the target environment."    
    }

    if(![string]::IsNullOrWhiteSpace($ZeroDowntimeMode) -and ($IncludeCode -ne $true -or [string]::IsNullOrWhiteSpace($SourceApp))){
        throw "Zero Downtime Deployment requires code to be pushed. Please use the -IncludeCode flag, and also include the -SourceApp flag"
    }
    
    Write-Host "Validation passed. Starting Deployment from $SourceEnvironment to $TargetEnvironment"

    Write-Host "Setting up the deployment configuration"

    $startEpiDeploymentSplat = @{
                ProjectId = "$ProjectID"
                Wait = $false
                TargetEnvironment = "$TargetEnvironment"
                SourceEnvironment = "$SourceEnvironment"
                IncludeBlob = $IncludeBlobs
                IncludeDb = $IncludeDb
                ClientSecret = "$ClientSecret"
                ClientKey = "$ClientKey"
    }

    if($IncludeCode){
        $startEpiDeploymentSplat.Add("SourceApp", $SourceApp)
        $startEpiDeploymentSplat.Add("UseMaintenancePage", $UseMaintenancePage)
    }

    if($DirectDeploy -eq $true){
        $startEpiDeploymentSplat.Add("DirectDeploy", $true)
    }

    if(![string]::IsNullOrWhiteSpace($ZeroDownTimeMode)){
        $startEpiDeploymentSplat.Add("ZeroDownTimeMode", $ZeroDownTimeMode)
    }

    Write-Host "Starting the Deployment to" $TargetEnvironment

    #Starting the Deployment
    $deploy = Start-EpiDeployment @startEpiDeploymentSplat

    $deployId = $deploy | Select -ExpandProperty "id"

    #Setting up the object for the EpiServer Deployment Updates
    $getEpiDeploymentSplat = @{
        ProjectId = "$ProjectID"
        ClientSecret = "$ClientSecret"
        ClientKey = "$ClientKey"
        Id = "$deployId"
    }

    #Setting up Variables for progress output
    $percentComplete = 0
    $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat | Select-Object -First 1
    $status = $currDeploy | Select -ExpandProperty "status"
    $exit = 0

    Write-Host "Percent Complete: $percentComplete%"
    Write-Output "##vso[task.setprogress value=$percentComplete]Percent Complete: $percentComplete%"

    #While the exit flag is not true
    while($exit -ne 1){

    #Get the current Deploy
    $currDeploy = Get-EpiDeployment @getEpiDeploymentSplat | Select-Object -First 1

    #Set the current Percent and Status
    $currPercent = $currDeploy | Select -ExpandProperty "percentComplete"
    $status = $currDeploy | Select -ExpandProperty "status"

    #If the current percent is not equal to what it was before, send an update
    #(This is done this way to prevent a bunch of messages to the screen)
    if($currPercent -ne $percentComplete){
        Write-Host "Percent Complete: $currPercent%"
        Write-Output "##vso[task.setprogress value=$currPercent]Percent Complete: $currPercent%"
        #Set the overall percent complete variable to the new percent complete
        $percentComplete = $currPercent
    }

    #If the Percent Complete is equal to 100%, Set the exit flag to true
    if($percentComplete -eq 100){
        $exit = 1    
    }

    #If the status of the deployment is not what it should be for this scipt, Set the exit flad to true
    if($status -ne 'InProgress'){
        $exit = 1
    }

    #Wait 1 second between checks
    start-sleep -Milliseconds 1000

    }

    #If the status is set to Failed, throw an error
    if($status -eq "Failed"){
        throw "Deployment Failed. Errors: \n" + $deploy.deploymentErrors
    }

    Write-Host "Deployment Complete"
}

if([string]::IsNullOrWhiteSpace($ClientKey)){
    throw "A Client Key is needed. Please supply one."
}
if([string]::IsNullOrWhiteSpace($ClientSecret)){
    throw "A Client Secret Key is needed. Please supply one."
}
if([string]::IsNullOrWhiteSpace($ProjectID)){
    throw "A Project ID GUID is needed. Please supply one."
}
if([string]::IsNullOrWhiteSpace($ArtifactPath)){
    throw "A valid artifact path is required. Please supply one."
}
if([string]::IsNullOrWhiteSpace($TargetEnvironment)){
    throw "A target deployment environment is needed. Please supply one."
}



$IsInTheCloud = Test-IsNonInteractiveShell

if($IsInTheCloud -eq  $true)
{
   Write-Host "Non-Interactive and/or Cloud shell detected. Force Installing EpiCloud Powershell Module"
   Install-Module EpiCloud -Scope CurrentUser -Repository PSGallery -AllowClobber -MinimumVersion 1.0.0 -Force
}  
else
{
   Write-Host "Installing EpiCloud Powershell Module"
   Install-Module EpiCloud -Scope CurrentUser -Repository PSGallery -AllowClobber -MinimumVersion 1.0.0     
}


Write-Host "Validation passed. Starting Deployment to $TargetEnvironment"

if ($TargetEnvironment -eq "Integration") {
    UploadEpiPackage -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -ArtifactPath $ArtifactPath
    DeployToEnvironment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -ArtifactPath $ArtifactPath -TargetEnvironment "Integration" -UseMaintenancePage 0 -DirectDeploy 1
} elseif ($TargetEnvironment -eq "Preproduction") {
    UploadEpiPackage -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -ArtifactPath $ArtifactPath
    DeployToEnvironment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -ArtifactPath $ArtifactPath -TargetEnvironment "Preproduction" -UseMaintenancePage 1
    CompleteOrResetDeployment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -Action "Complete" -TargetEnvironment "Preproduction"
} elseif ($TargetEnvironment -eq "Production") {
    PromoteToEnvironment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -SourceEnvironment "Preproduction" -TargetEnvironment "Production" -UseMaintenancePage 0 -IncludeCode 1 -IncludeBlobs 0 -IncludeDb 0 -SourceApp "cms" # -ZeroDowntimeMode "ReadOnly"
    CompleteOrResetDeployment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectID $ProjectID -Action "Complete" -TargetEnvironment "Production"
} else {
    Write-Host "Unknown environment. No deployment executed."
}


