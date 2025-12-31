import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { Observable, combineLatest, map } from 'rxjs';
import { SleepSessionService, HabitService, PatternService } from '../../services';

interface DashboardStats {
  totalSessions: number;
  totalHabits: number;
  totalPatterns: number;
  averageSleepDuration: number;
  goodQualitySessions: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private sleepSessionService = inject(SleepSessionService);
  private habitService = inject(HabitService);
  private patternService = inject(PatternService);

  stats$!: Observable<DashboardStats>;

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.sleepSessionService.getSleepSessions().subscribe();
    this.habitService.getHabits().subscribe();
    this.patternService.getPatterns().subscribe();

    this.stats$ = combineLatest([
      this.sleepSessionService.sleepSessions$,
      this.habitService.habits$,
      this.patternService.patterns$
    ]).pipe(
      map(([sessions, habits, patterns]) => ({
        totalSessions: sessions.length,
        totalHabits: habits.length,
        totalPatterns: patterns.length,
        averageSleepDuration: sessions.length > 0
          ? Math.round(sessions.reduce((sum, s) => sum + s.totalSleepMinutes, 0) / sessions.length)
          : 0,
        goodQualitySessions: sessions.filter(s => s.isGoodQuality).length
      }))
    );
  }

  formatDuration(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins}m`;
  }
}
