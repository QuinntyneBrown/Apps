import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { InjuryService, MilestoneService, RecoveryExerciseService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatProgressBarModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Recovery Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>healing</mat-icon>
            <mat-card-title>Active Injuries</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ activeInjuriesCount }}</p>
            <p class="dashboard__substat">{{ (injuries$ | async)?.length || 0 }} total tracked</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/injuries">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>fitness_center</mat-icon>
            <mat-card-title>Active Exercises</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ activeExercisesCount }}</p>
            <p class="dashboard__substat">Recovery exercises</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/exercises">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>flag</mat-icon>
            <mat-card-title>Milestones</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ achievedMilestonesCount }}/{{ (milestones$ | async)?.length || 0 }}</p>
            <p class="dashboard__substat">Achieved</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/milestones">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--progress">
          <mat-card-header>
            <mat-icon mat-card-avatar>trending_up</mat-icon>
            <mat-card-title>Overall Progress</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ averageProgress }}%</p>
            <mat-progress-bar mode="determinate" [value]="averageProgress"></mat-progress-bar>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 1.5rem;
    }
    .dashboard__title {
      margin-bottom: 1.5rem;
    }
    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 1.5rem;
    }
    .dashboard__card {
      mat-icon[mat-card-avatar] {
        font-size: 40px;
        width: 40px;
        height: 40px;
      }
    }
    .dashboard__stat {
      font-size: 1.5rem;
      font-weight: 500;
      margin: 0.5rem 0;
    }
    .dashboard__substat {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private _injuryService = inject(InjuryService);
  private _milestoneService = inject(MilestoneService);
  private _exerciseService = inject(RecoveryExerciseService);

  injuries$ = this._injuryService.injuries$;
  milestones$ = this._milestoneService.milestones$;
  exercises$ = this._exerciseService.exercises$;

  activeInjuriesCount = 0;
  activeExercisesCount = 0;
  achievedMilestonesCount = 0;
  averageProgress = 0;

  ngOnInit(): void {
    this._injuryService.getAll().subscribe(injuries => {
      this.activeInjuriesCount = injuries.filter(i => i.status !== 'Recovered').length;
      this.averageProgress = injuries.length > 0
        ? Math.round(injuries.reduce((sum, i) => sum + i.progressPercentage, 0) / injuries.length)
        : 0;
    });
    this._milestoneService.getAll().subscribe(milestones => {
      this.achievedMilestonesCount = milestones.filter(m => m.isAchieved).length;
    });
    this._exerciseService.getAll().subscribe(exercises => {
      this.activeExercisesCount = exercises.filter(e => e.isActive).length;
    });
  }
}
