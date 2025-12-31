import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { JournalService } from '../../services';
import { JournalDialog } from '../../components';

@Component({
  selector: 'app-journals',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './journals.html',
  styleUrl: './journals.scss'
})
export class Journals implements OnInit {
  private _journalService = inject(JournalService);
  private _dialog = inject(MatDialog);

  journals$ = this._journalService.journals$;
  displayedColumns = ['entryDate', 'title', 'tags', 'actions'];

  ngOnInit(): void {
    this._journalService.getJournals().subscribe();
  }

  openDialog(journal?: any): void {
    const dialogRef = this._dialog.open(JournalDialog, {
      width: '600px',
      data: { journal }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (journal) {
          this._journalService.updateJournal(journal.journalId, result).subscribe();
        } else {
          this._journalService.createJournal(result).subscribe();
        }
      }
    });
  }

  deleteJournal(id: string): void {
    if (confirm('Are you sure you want to delete this journal?')) {
      this._journalService.deleteJournal(id).subscribe();
    }
  }
}
