
 
try {

    mkdir package -ErrorAction SilentlyContinue | Out-Null
    Remove-Item package\* -Recurse -Force -ErrorAction SilentlyContinue
    $packageFolder = "./package/MiniMapMoveCamera"
    mkdir $packageFolder | Out-Null

    # ---- Build the projects.  The projects will automatically deploy to the Steam Workshop folder.

    "Bootstrap"
    dotnet clean ./src\MiniMapMoveCamera_Bootstrap.csproj
    dotnet build -c Release ./src\MiniMapMoveCamera_Bootstrap.csproj -o $packageFolder
    
    "Stable"
    dotnet clean ..\main-repo\src\MiniMapMoveCamera.csproj
    dotnet build -c Release ..\main-repo\src\MiniMapMoveCamera.csproj -o $packageFolder\Stable
    # dotnet build has a bug where it always copies any project references.
    del $packageFolder/Stable/MiniMapMoveCamera_Bootstrap.*

    "Beta"
    dotnet clean ..\beta\src\MiniMapMoveCamera.csproj
    dotnet build -c Release ..\beta\src\MiniMapMoveCamera.csproj -o $packageFolder\Beta
    # dotnet build has a bug where it always copies any project references.
    del $packageFolder/Beta/MiniMapMoveCamera_Bootstrap.*

    # ---- Create the package zip file.
    Copy-Item ../main-repo/media/thumbnail.png $packageFolder
    Copy-Item ../main-repo/README.md $packageFolder
    Copy-Item version-info.json $packageFolder
    Copy-Item modmanifest.json $packageFolder

    Compress-Archive -Path $packageFolder\* -DestinationPath ./MiniMapMoveCamera.zip -Force

    # Add the beta text if beta is not disabled.
    # Otherwise remove?

    "Build completed"
} catch {
    Write-Error "Build Failure: $_"
    exit 1
}



