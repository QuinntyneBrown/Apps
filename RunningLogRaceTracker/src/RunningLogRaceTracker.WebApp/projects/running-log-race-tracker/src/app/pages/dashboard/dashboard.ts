import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RunService, RaceService, TrainingPlanService } from '../../services';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly _runService = inject(RunService);
  private readonly _raceService = inject(RaceService);
  private readonly _trainingPlanService = inject(TrainingPlanService);

  dashboardData$ = combineLatest([
    this._runService.runs$,
    this._raceService.races$,
    this._trainingPlanService.trainingPlans$
  ]).pipe(
    map(([runs, races, plans]) => {
      const totalRuns = runs.length;
      const totalDistance = runs.reduce((sum, run) => sum + run.distance, 0);
      const upcomingRaces = races.filter(r => !r.isCompleted && new Date(r.raceDate) > new Date()).length;
      const activePlans = plans.filter(p => p.isActive).length;

      return {
        totalRuns,
        totalDistance: totalDistance.toFixed(2),
        upcomingRaces,
        activePlans,
        recentRuns: runs.slice(0, 5),
        upcomingRacesList: races
          .filter(r => !r.isCompleted && new Date(r.raceDate) > new Date())
          .sort((a, b) => new Date(a.raceDate).getTime() - new Date(b.raceDate).getTime())
          .slice(0, 3)
      };
    })
  );

  ngOnInit() {
    this._runService.getRuns().subscribe();
    this._raceService.getRaces().subscribe();
    this._trainingPlanService.getTrainingPlans().subscribe();
  }
}
