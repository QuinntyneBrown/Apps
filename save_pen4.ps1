Add-Type -AssemblyName System.Windows.Forms
Add-Type @"
using System;
using System.Runtime.InteropServices;
public class Win32 {
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
}
"@

$proc = Get-Process -Name "Code" | Where-Object { $_.MainWindowTitle -like '*pencil*' } | Select-Object -First 1
if ($proc) {
    Write-Host "Found window: $($proc.MainWindowTitle)"
    [Win32]::SetForegroundWindow($proc.MainWindowHandle)
    Start-Sleep -Milliseconds 500
    $fg = [Win32]::GetForegroundWindow()
    Write-Host "Foreground window handle: $fg, Target: $($proc.MainWindowHandle)"

    # Try Ctrl+Shift+S for Save As
    [System.Windows.Forms.SendKeys]::SendWait("^+s")
    Write-Host "Sent Ctrl+Shift+S"
    Start-Sleep -Seconds 5

    # Type the path
    [System.Windows.Forms.SendKeys]::SendWait("C:\projects\Apps\DailyJournalingApp\designs\design.pen")
    Write-Host "Typed path"
    Start-Sleep -Seconds 2

    # Press Enter to confirm
    [System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
    Write-Host "Pressed Enter"
    Start-Sleep -Seconds 4

    # Press Enter again for overwrite confirmation
    [System.Windows.Forms.SendKeys]::SendWait("{ENTER}")
    Write-Host "Pressed Enter again"
    Start-Sleep -Seconds 2
    Write-Host "Done"
} else {
    Write-Host "Window not found"
}
