$cutoff = (Get-Date).AddMinutes(-10)
# Search limited locations
$paths = @(
    "$env:USERPROFILE",
    "$env:APPDATA",
    "$env:LOCALAPPDATA",
    "C:\projects",
    "$env:TEMP"
)
foreach ($p in $paths) {
    Get-ChildItem -Path $p -Filter '*.pen' -Recurse -Depth 5 -ErrorAction SilentlyContinue |
        Where-Object { $_.LastWriteTime -gt $cutoff } |
        Select-Object FullName, Length, LastWriteTime
}
