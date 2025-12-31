import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { JournalEntryService } from '../../services';
import { JournalEntry } from '../../models';
import { JournalEntryCard } from '../../components/journal-entry-card/journal-entry-card';
import { JournalEntryDialog } from '../../components/journal-entry-dialog/journal-entry-dialog';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    JournalEntryCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly journalEntryService = inject(JournalEntryService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);

  entries$: Observable<JournalEntry[]>;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  constructor() {
    this.entries$ = this.journalEntryService.entries$;
  }

  ngOnInit(): void {
    this.loadEntries();
  }

  loadEntries(): void {
    this.journalEntryService.getAll(this.userId).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(JournalEntryDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.journalEntryService.create(result).subscribe();
      }
    });
  }

  onEdit(entry: JournalEntry): void {
    const dialogRef = this.dialog.open(JournalEntryDialog, {
      width: '600px',
      data: { entry, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.journalEntryService.update(entry.journalEntryId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this entry?')) {
      this.journalEntryService.delete(id).subscribe();
    }
  }

  onToggleFavorite(id: string): void {
    this.journalEntryService.toggleFavorite(id).subscribe();
  }

  navigateToAllEntries(): void {
    this.router.navigate(['/journal-entries']);
  }
}
