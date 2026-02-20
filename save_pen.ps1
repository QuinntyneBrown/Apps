Add-Type -AssemblyName System.Windows.Forms
Start-Sleep -Seconds 1
[System.Windows.Forms.SendKeys]::SendWait('^+s')
Start-Sleep -Seconds 4
[System.Windows.Forms.SendKeys]::SendWait('C:\projects\Apps\DailyJournalingApp\designs\design.pen')
Start-Sleep -Seconds 2
[System.Windows.Forms.SendKeys]::SendWait('{ENTER}')
Start-Sleep -Seconds 3
[System.Windows.Forms.SendKeys]::SendWait('{ENTER}')
