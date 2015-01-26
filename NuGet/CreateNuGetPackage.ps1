param($apiKey)

#(new-object Net.WebClient).DownloadString("https://raw.github.com/ligershark/psbuild/master/src/GetPSBuild.ps1") | iex
#get-command -Module psbuild
#Get-Help Invoke-MSBuild
#Get-Help Invoke-MSBuild -Examples

function Get-ScriptDirectory 
{ 
 $Invocation = (Get-Variable MyInvocation -Scope 1).Value 
 Split-Path $Invocation.MyCommand.Path 
} 
$scriptDir = Get-ScriptDirectory;
write $scriptDir;

#Invoke-MSBuild "..\NewportVS2012.sln" -configuration Release
$sln = Join-Path (Get-ScriptDirectory) "..\NewportVS2013.sln"
write $sln;
Invoke-MSBuild $sln -configuration Release -visualStudioVersion 12.0


# Get Version of Newport.WindowsPhone assembly.
# We will use that as the version of our NuGet package.
$asmPath = Join-Path (Get-ScriptDirectory) "..\Newport.WindowsPhone8\Bin\Release\Newport.WindowsPhone.dll"
write($asmPath);
$asm = [Reflection.Assembly]::ReflectionOnlyLoadFrom($asmPath)
$asmVersion = $asm.GetName().Version.ToString()
write($asmVersion);

# Store version in NuSpec
$nuspecPath = Join-Path (Get-ScriptDirectory) "Newport.nuspec"
write($nuspecPath);
[xml]$nuspec = Get-Content $nuspecPath
$nuspec.package.metadata.version = $asmVersion
$nuspec.Save($nuspecPath);

cd $scriptDir;
.\NuGet.exe pack $nuspecPath

#If an API key was specified, upload the package.
if (!$apiKey)
{
  Write-Warning "No ApiKey specified";
  return;
} 

write($apiKey)
.\NuGet.exe SetApiKey $apiKey
.\NuGet.exe push .\Newport.$asmVersion.nupkg