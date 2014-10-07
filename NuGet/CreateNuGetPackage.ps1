param($apiKey)

function Get-ScriptDirectory 
{ 
 $Invocation = (Get-Variable MyInvocation -Scope 1).Value 
 Split-Path $Invocation.MyCommand.Path 
} 

$scriptDir = Get-ScriptDirectory;
cd $scriptDir

# Get Version of Newport.WindowsPhone assembly.
# We will use that as the version of our NuGet package.
$asmPath = "..\Newport.WindowsPhone8\Bin\Release\Newport.WindowsPhone.dll"
$asm = [Reflection.Assembly]::ReflectionOnlyLoadFrom($asmPath)
$asmVersion = $asm.GetName().Version.ToString()
write($asmVersion);

# Store version in NuSpec
[xml]$nuspec = Get-Content .\Newport.nuspec
$nuspec.package.metadata.version = $asmVersion
$nuspec.Save(".\Newport.nuspec");

.\NuGet.exe pack .\Newport.nuspec

#If an API key was specified, upload the package.
if (!$apiKey)
{
  Write-Warning "No ApiKey specified";
  return;
} 

write($apiKey)
.\NuGet.exe SetApiKey $apiKey
.\NuGet.exe push .\Newport.$asmVersion.nupkg