param
  (
	[Parameter(Position=0, Mandatory)]
	[ValidateNotNullOrEmpty()]
	[string]$sourceBranch,
    [Parameter(Position=1, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$rootDir,
    [Parameter(Position=2, Mandatory)]
    [ValidateNotNullOrEmpty()]
    [string]$version
  )
  if([string]::IsNullOrWhiteSpace($sourceBranch)){
	  throw "Source branch must be specified."
  }
  if([string]::IsNullOrWhiteSpace($rootDir)){
    throw "Please specify the root path containing the CSProj files to update."
  }
  if([string]::IsNullOrWhiteSpace($version)){
    throw "Please specify the version number to be used."
  }

$sourceBranch = $sourceBranch.Replace("refs/heads/","")
$tempBranch = "temp/UpdateVersion_$($version)"
# create a new branch for hosting update
git branch $tempBranch --quiet
# switch to new branch for performing updates
git checkout $tempBranch --quiet

# function to update or add and update XML nodes in project group
Function Set-NodeValue($xmlRootNode, [string]$nodeName, [string]$nodeValue) {
	$propGroup = $xmlRootNode.Node.SelectSingleNode("PropertyGroup")
	$node = $propGroup.SelectSingleNode("./$($nodeName)")
	
	if ($node -eq $null) {
		Write-Host "Adding node for $($nodeName)"
		$node = $propGroup.OwnerDocument.CreateElement($nodeName)
		$nodeAdded = $propGroup.AppendChild($node)	# do this to avoid console output
	}
	else {
		Write-Host "Existing node found for $($nodeName)"
	}
	$node.InnerText = $nodeValue	
	Write-Host "Set value $($nodeValue) for node $($nodeName)"
}	
# loop all CSProj files so they are all on same version
Get-ChildItem -Path $rootDir -Filter "*.csproj" -Recurse -File |
  ForEach-Object {
	  Write-Host "Found project file $($_.FullName)"
	  $projPath = $_.FullName
	  $projXml = Select-Xml $projPath -XPath "//Project"
	  
	  Set-NodeValue $projXml "Version" $version
	  Set-NodeValue $projXml "AssemblyVersion" $version
	  Set-NodeValue $projXml "FileVersion" $version
	  Set-NodeValue $projXml "InformationalVersion" $version
	  
	  $doc = $projXml.Node.OwnerDocument	  
	  $doc.PreserveWhitespace = $true
	  $doc.save($projPath)
	  
	# add the updated CSProj to GIT
	git add $projPath	  
  }

# update email and username to identify repo updates by automated pipe - not a real address
git config user.email "azure.agent@perficient.com"
git config user.name "Azure Agent" --replace-all
# commit changes with a comment identifying pipeline mod
git commit -m "[skip ci] Pipeline Modification: Set project versions to $($version)"
# switch back to the original branch that triggered this
git checkout $sourceBranch --quiet
# pull latest from git
git pull --quiet
# merge the proj updates back into source branch, with comment to indicate operation
git merge $tempBranch -m "[skip ci] Pipeline Modification: Complete version to $($version) for source $($sourceBranch)" --quiet
# push changes back into source branch
git push -u origin --quiet
