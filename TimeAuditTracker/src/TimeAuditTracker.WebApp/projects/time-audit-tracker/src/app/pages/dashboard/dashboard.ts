import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { TimeBlockService, GoalService, AuditReportService } from '../../services';
import { map, combineLatest } from 'rxjs';

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
  private _timeBlockService = inject(TimeBlockService);
  private _goalService = inject(GoalService);
  private _auditReportService = inject(AuditReportService);

  dashboardData$ = combineLatest([
    this._timeBlockService.timeBlocks$,
    this._goalService.goals$,
    this._auditReportService.auditReports$
  ]).pipe(
    map(([timeBlocks, goals, reports]) => ({
      totalTimeBlocks: timeBlocks.length,
      activeGoals: goals.filter(g => g.isActive).length,
      totalReports: reports.length,
      recentTimeBlocks: timeBlocks.slice(0, 5),
      activeGoalsList: goals.filter(g => g.isActive).slice(0, 3)
    }))
  );

  ngOnInit(): void {
    this._timeBlockService.getAll().subscribe();
    this._goalService.getAll().subscribe();
    this._auditReportService.getAll().subscribe();
  }
}
