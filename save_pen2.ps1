Add-Type -AssemblyName System.Windows.Forms
Add-Type @"
using System;
using System.Runtime.InteropServices;
public class Win32 {
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
}
"@

$proc = Get-Process -Name "Code" | Where-Object { $_.MainWindowTitle -like '*pencil*' } | Select-Object -First 1
if ($proc) {
    [Win32]::SetForegroundWindow($proc.MainWindowHandle)
    Start-Sleep -Seconds 1
    [System.Windows.Forms.SendKeys]::SendWait("^+s")
    Start-Sleep -Seconds 4
    [System.Windows.Forms.SendKeys]::SendWait("C:\projects\Apps\DailyJournalingApp\designs\design.pen")
    Start-Sleep -Seconds 2
    [System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
    Start-Sleep -Seconds 3
    [System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
    Write-Host "Save commands sent successfully"
} else {
    Write-Host "Could not find VS Code pencil window"
}
