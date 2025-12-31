import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MoodEntryService } from '../../services';
import { MoodEntryDialog } from '../../components';

@Component({
  selector: 'app-mood-entries',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './mood-entries.html',
  styleUrl: './mood-entries.scss'
})
export class MoodEntries implements OnInit {
  private _moodEntryService = inject(MoodEntryService);
  private _dialog = inject(MatDialog);

  moodEntries$ = this._moodEntryService.moodEntries$;
  displayedColumns = ['entryTime', 'moodLevel', 'stressLevel', 'activities', 'actions'];

  ngOnInit(): void {
    this._moodEntryService.getMoodEntries().subscribe();
  }

  openDialog(moodEntry?: any): void {
    const dialogRef = this._dialog.open(MoodEntryDialog, {
      width: '500px',
      data: { moodEntry }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (moodEntry) {
          this._moodEntryService.updateMoodEntry(moodEntry.moodEntryId, result).subscribe();
        } else {
          this._moodEntryService.createMoodEntry(result).subscribe();
        }
      }
    });
  }

  deleteMoodEntry(id: string): void {
    if (confirm('Are you sure you want to delete this mood entry?')) {
      this._moodEntryService.deleteMoodEntry(id).subscribe();
    }
  }
}
