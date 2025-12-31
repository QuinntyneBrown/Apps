import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { MoodEntryService, JournalService, TriggerService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _moodEntryService = inject(MoodEntryService);
  private _journalService = inject(JournalService);
  private _triggerService = inject(TriggerService);

  stats$ = combineLatest([
    this._moodEntryService.moodEntries$,
    this._journalService.journals$,
    this._triggerService.triggers$
  ]).pipe(
    map(([moodEntries, journals, triggers]) => ({
      moodEntriesCount: moodEntries.length,
      journalsCount: journals.length,
      triggersCount: triggers.length,
      recentMoodEntries: moodEntries.slice(0, 5),
      recentJournals: journals.slice(0, 5)
    }))
  );

  ngOnInit(): void {
    this._moodEntryService.getMoodEntries().subscribe();
    this._journalService.getJournals().subscribe();
    this._triggerService.getTriggers().subscribe();
  }
}
