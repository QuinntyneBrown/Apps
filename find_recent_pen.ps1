# Search for any .pen files modified in the last 5 minutes
$cutoff = (Get-Date).AddMinutes(-5)
Get-ChildItem -Path 'C:\' -Filter '*.pen' -Recurse -ErrorAction SilentlyContinue |
    Where-Object { $_.LastWriteTime -gt $cutoff } |
    Select-Object FullName, Length, LastWriteTime |
    Format-Table -AutoSize
