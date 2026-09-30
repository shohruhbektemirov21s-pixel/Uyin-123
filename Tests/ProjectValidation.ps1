$ErrorActionPreference = 'Stop'

$projectRoot = Split-Path -Parent $PSScriptRoot
$failures = [System.Collections.Generic.List[string]]::new()

function Assert-SourceContains {
    param(
        [string]$RelativePath,
        [string]$Pattern,
        [string]$Guarantee
    )

    $path = Join-Path $projectRoot $RelativePath
    if (-not (Test-Path -LiteralPath $path)) {
        $failures.Add("$Guarantee (missing: $RelativePath)")
        return
    }

    $source = Get-Content -LiteralPath $path -Raw
    if ($source -notmatch $Pattern) {
        $failures.Add("$Guarantee ($RelativePath)")
    }
}

Assert-SourceContains 'Packages/manifest.json' '"com\.unity\.ugui"' 'Unity UI dependency is declared'
Assert-SourceContains 'Assets/Scripts/UI/UIManager.cs' 'new GameObject\("EventSystem"\)' 'Runtime UI creates an EventSystem'
Assert-SourceContains 'Assets/Scripts/UI/UIManager.cs' 'LegacyRuntime\.ttf' 'Runtime UI assigns a built-in font'
Assert-SourceContains 'Assets/Scripts/UI/UIManager.cs' 'UpdateScreens\(GameManager\.Instance\.State\)' 'Initial UI state is synchronized'
Assert-SourceContains 'Assets/Scripts/Grid/DraggableBlock.cs' 'Input\.GetMouseButtonDown' 'Blocks support mouse input in the editor'
Assert-SourceContains 'Assets/Scripts/Grid/DraggableBlock.cs' 'SpriteFactory\.CreateSquare' 'Draggable shapes create visible cells'
Assert-SourceContains 'Assets/Scripts/Grid/GridManager.cs' '_cellRenderers' 'The board has runtime cell visuals'
Assert-SourceContains 'Assets/Scripts/Gameplay/ShapeGenerator.cs' '_previousState != GameState\.Paused' 'Resume does not spawn an extra batch'
Assert-SourceContains 'Assets/Scripts/Grid/BlockSpawner.cs' 'ClearBlocks\(' 'A new game can clear stale blocks'
Assert-SourceContains 'Packages/manifest.json' '"com\.unity\.modules\.androidjni"\s*:\s*"1\.0\.0"' 'Android builds enable the JNI engine module used by haptics'
Assert-SourceContains 'Assets/Scripts/Editor/AndroidBuildSettings.cs' 'buildAppBundle = false' 'Android output is configured as APK'
Assert-SourceContains 'Assets/Scripts/Editor/AndroidBuilder.cs' 'Builds/Android/BlockBlast\.apk' 'Android builder writes the requested APK artifact'

if ($failures.Count -gt 0) {
    Write-Host 'Project validation failed:' -ForegroundColor Red
    $failures | ForEach-Object { Write-Host " - $_" }
    exit 1
}

Write-Host 'Project validation passed.' -ForegroundColor Green
