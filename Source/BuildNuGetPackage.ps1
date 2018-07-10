param (
	[switch]$Pack = $true,
	[switch]$Push,
	[string]$Package = "LoadBalancerExtensions"
)

Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

if($pack) {
	& msbuild LoadBalancerExtensions.sln /p:Configuration=Release
	if(!$?){throw "msbuild returned exit code $LASTEXITCODE"}
	dotnet test .\UnitTest\UnitTest.csproj  --no-build
	if(!$?){throw "dotnet test returned exit code $LASTEXITCODE"}
}

if($push) {
	$filename = Get-ChildItem "$Package/bin/Release/$Package*.nupkg" | where Name -NotLike "*.symbols.*" | Sort-Object LastWriteTime -Descending | Select -First 1
	echo "Pushing $filename"
	& .nuget\nuget.exe push "$filename" -source https://api.nuget.org/v3/index.json
	if(!$?){throw "nuget returned exit code $lastexitcode"}
}
