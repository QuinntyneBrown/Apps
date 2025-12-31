import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { JournalEntryService } from '../../services';
import { JournalEntry, Mood } from '../../models';
import { JournalEntryCard } from '../../components/journal-entry-card/journal-entry-card';
import { JournalEntryDialog } from '../../components/journal-entry-dialog/journal-entry-dialog';

@Component({
  selector: 'app-journal-entries',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    JournalEntryCard
  ],
  templateUrl: './journal-entries.html',
  styleUrl: './journal-entries.scss'
})
export class JournalEntries implements OnInit {
  private readonly journalEntryService = inject(JournalEntryService);
  private readonly dialog = inject(MatDialog);

  entries$: Observable<JournalEntry[]>;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  // Filters
  startDate: Date | null = null;
  endDate: Date | null = null;
  selectedMood: Mood | null = null;
  favoritesOnly = false;

  moods = [
    { value: Mood.VeryHappy, label: 'Very Happy' },
    { value: Mood.Happy, label: 'Happy' },
    { value: Mood.Neutral, label: 'Neutral' },
    { value: Mood.Sad, label: 'Sad' },
    { value: Mood.VerySad, label: 'Very Sad' },
    { value: Mood.Anxious, label: 'Anxious' },
    { value: Mood.Calm, label: 'Calm' },
    { value: Mood.Energetic, label: 'Energetic' },
    { value: Mood.Tired, label: 'Tired' }
  ];

  constructor() {
    this.entries$ = this.journalEntryService.entries$;
  }

  ngOnInit(): void {
    this.loadEntries();
  }

  loadEntries(): void {
    this.journalEntryService.getAll(
      this.userId,
      this.startDate?.toISOString(),
      this.endDate?.toISOString(),
      this.selectedMood ?? undefined,
      this.favoritesOnly || undefined
    ).subscribe();
  }

  applyFilters(): void {
    this.loadEntries();
  }

  clearFilters(): void {
    this.startDate = null;
    this.endDate = null;
    this.selectedMood = null;
    this.favoritesOnly = false;
    this.loadEntries();
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
}
