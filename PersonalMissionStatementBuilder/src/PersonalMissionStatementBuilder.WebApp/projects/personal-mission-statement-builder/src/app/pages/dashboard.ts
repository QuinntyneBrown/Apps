import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MissionStatementService, ValueService, GoalService, ProgressService } from '../services';
import { GoalStatus, GoalStatusLabels } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private missionStatementService = inject(MissionStatementService);
  private valueService = inject(ValueService);
  private goalService = inject(GoalService);
  private progressService = inject(ProgressService);

  missionStatements$ = this.missionStatementService.missionStatements$;
  values$ = this.valueService.values$;
  goals$ = this.goalService.goals$;
  progresses$ = this.progressService.progresses$;

  GoalStatus = GoalStatus;
  GoalStatusLabels = GoalStatusLabels;

  ngOnInit() {
    this.missionStatementService.getAll().subscribe();
    this.valueService.getAll().subscribe();
    this.goalService.getAll().subscribe();
    this.progressService.getAll().subscribe();
  }

  getGoalsByStatus(goals: any[], status: GoalStatus): number {
    return goals.filter(g => g.status === status).length;
  }

  getCurrentMissionStatement(missionStatements: any[]) {
    return missionStatements.find(m => m.isCurrentVersion);
  }
}
