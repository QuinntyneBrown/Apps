import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { HoleScoresService, RoundsService } from '../services';

@Component({
  selector: 'app-scorecard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  template: `
    <div class="scorecard">
      <div class="scorecard__header">
        <button mat-button [routerLink]="roundId ? ['/rounds', roundId] : ['/rounds']">
          <mat-icon>arrow_back</mat-icon>
          Back
        </button>
        <h1>Scorecard</h1>
      </div>

      <mat-card class="scorecard__card">
        <mat-card-header>
          <mat-card-title>Hole-by-Hole Scores</mat-card-title>
          <mat-card-subtitle *ngIf="(round$ | async) as round">
            {{ round.courseName }} - {{ round.playedDate | date:'shortDate' }}
          </mat-card-subtitle>
        </mat-card-header>

        <mat-card-content>
          <div *ngIf="(holeScores$ | async) as holeScores">
            <p *ngIf="holeScores.length === 0" class="scorecard__empty">No hole scores recorded yet.</p>

            <div *ngIf="holeScores.length > 0">
              <table mat-table [dataSource]="holeScores" class="scorecard__table">
                <ng-container matColumnDef="hole">
                  <th mat-header-cell *matHeaderCellDef>Hole</th>
                  <td mat-cell *matCellDef="let score">{{ score.holeNumber }}</td>
                  <td mat-footer-cell *matFooterCellDef>Total</td>
                </ng-container>

                <ng-container matColumnDef="par">
                  <th mat-header-cell *matHeaderCellDef>Par</th>
                  <td mat-cell *matCellDef="let score">{{ score.par }}</td>
                  <td mat-footer-cell *matFooterCellDef>{{ getTotalPar(holeScores) }}</td>
                </ng-container>

                <ng-container matColumnDef="score">
                  <th mat-header-cell *matHeaderCellDef>Score</th>
                  <td mat-cell *matCellDef="let score" [class.scorecard__over-par]="score.score > score.par" [class.scorecard__under-par]="score.score < score.par" [class.scorecard__birdie]="score.score === score.par - 1" [class.scorecard__eagle]="score.score <= score.par - 2">
                    {{ score.score }}
                  </td>
                  <td mat-footer-cell *matFooterCellDef>{{ getTotalScore(holeScores) }}</td>
                </ng-container>

                <ng-container matColumnDef="putts">
                  <th mat-header-cell *matHeaderCellDef>Putts</th>
                  <td mat-cell *matCellDef="let score">{{ score.putts || '-' }}</td>
                  <td mat-footer-cell *matFooterCellDef>{{ getTotalPutts(holeScores) }}</td>
                </ng-container>

                <ng-container matColumnDef="fir">
                  <th mat-header-cell *matHeaderCellDef>FIR</th>
                  <td mat-cell *matCellDef="let score">
                    <mat-icon *ngIf="score.fairwayHit" class="scorecard__icon-success">check_circle</mat-icon>
                    <mat-icon *ngIf="!score.fairwayHit" class="scorecard__icon-fail">cancel</mat-icon>
                  </td>
                  <td mat-footer-cell *matFooterCellDef>{{ getFairwayHits(holeScores) }}/{{ holeScores.length }}</td>
                </ng-container>

                <ng-container matColumnDef="gir">
                  <th mat-header-cell *matHeaderCellDef>GIR</th>
                  <td mat-cell *matCellDef="let score">
                    <mat-icon *ngIf="score.greenInRegulation" class="scorecard__icon-success">check_circle</mat-icon>
                    <mat-icon *ngIf="!score.greenInRegulation" class="scorecard__icon-fail">cancel</mat-icon>
                  </td>
                  <td mat-footer-cell *matFooterCellDef>{{ getGreensInRegulation(holeScores) }}/{{ holeScores.length }}</td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
                <tr mat-footer-row *matFooterRowDef="displayedColumns"></tr>
              </table>

              <div class="scorecard__stats">
                <h3>Statistics</h3>
                <div class="scorecard__stats-grid">
                  <div class="scorecard__stat">
                    <span class="scorecard__stat-label">Fairways Hit:</span>
                    <span class="scorecard__stat-value">{{ getFairwayHitPercentage(holeScores) }}%</span>
                  </div>
                  <div class="scorecard__stat">
                    <span class="scorecard__stat-label">Greens in Regulation:</span>
                    <span class="scorecard__stat-value">{{ getGirPercentage(holeScores) }}%</span>
                  </div>
                  <div class="scorecard__stat">
                    <span class="scorecard__stat-label">Average Putts:</span>
                    <span class="scorecard__stat-value">{{ getAveragePutts(holeScores) }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .scorecard {
      padding: 2rem;
      max-width: 1200px;
      margin: 0 auto;
    }

    .scorecard__header {
      display: flex;
      align-items: center;
      gap: 1rem;
      margin-bottom: 2rem;
    }

    .scorecard__header h1 {
      margin: 0;
    }

    .scorecard__card {
      margin-top: 1rem;
    }

    .scorecard__empty {
      text-align: center;
      padding: 2rem;
      color: #666;
    }

    .scorecard__table {
      width: 100%;
      margin-top: 1rem;
    }

    .scorecard__over-par {
      background-color: #ffebee;
      font-weight: 500;
    }

    .scorecard__under-par {
      background-color: #e8f5e9;
      font-weight: 500;
    }

    .scorecard__birdie {
      background-color: #c8e6c9;
      font-weight: 600;
    }

    .scorecard__eagle {
      background-color: #81c784;
      font-weight: 700;
    }

    .scorecard__icon-success {
      color: #4caf50;
    }

    .scorecard__icon-fail {
      color: #f44336;
    }

    .scorecard__stats {
      margin-top: 2rem;
    }

    .scorecard__stats h3 {
      margin-bottom: 1rem;
    }

    .scorecard__stats-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
      gap: 1rem;
    }

    .scorecard__stat {
      padding: 1rem;
      background-color: #f5f5f5;
      border-radius: 4px;
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }

    .scorecard__stat-label {
      font-size: 0.875rem;
      color: #666;
    }

    .scorecard__stat-value {
      font-size: 1.5rem;
      font-weight: 500;
      color: #1976d2;
    }
  `]
})
export class Scorecard implements OnInit {
  holeScores$ = this.holeScoresService.holeScores$;
  round$ = this.roundsService.currentRound$;
  displayedColumns: string[] = ['hole', 'par', 'score', 'putts', 'fir', 'gir'];
  roundId: string | null = null;

  constructor(
    private holeScoresService: HoleScoresService,
    private roundsService: RoundsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.roundId = this.route.snapshot.queryParamMap.get('roundId');
    if (this.roundId) {
      this.holeScoresService.getHoleScores(this.roundId).subscribe();
      this.roundsService.getRoundById(this.roundId).subscribe();
    } else {
      this.holeScoresService.getHoleScores().subscribe();
    }
  }

  getTotalPar(scores: any[]): number {
    return scores.reduce((sum, score) => sum + score.par, 0);
  }

  getTotalScore(scores: any[]): number {
    return scores.reduce((sum, score) => sum + score.score, 0);
  }

  getTotalPutts(scores: any[]): number {
    return scores.reduce((sum, score) => sum + (score.putts || 0), 0);
  }

  getFairwayHits(scores: any[]): number {
    return scores.filter(score => score.fairwayHit).length;
  }

  getGreensInRegulation(scores: any[]): number {
    return scores.filter(score => score.greenInRegulation).length;
  }

  getFairwayHitPercentage(scores: any[]): string {
    if (scores.length === 0) return '0';
    return ((this.getFairwayHits(scores) / scores.length) * 100).toFixed(1);
  }

  getGirPercentage(scores: any[]): string {
    if (scores.length === 0) return '0';
    return ((this.getGreensInRegulation(scores) / scores.length) * 100).toFixed(1);
  }

  getAveragePutts(scores: any[]): string {
    const scoresWithPutts = scores.filter(score => score.putts !== null && score.putts !== undefined);
    if (scoresWithPutts.length === 0) return 'N/A';
    return (scoresWithPutts.reduce((sum, score) => sum + score.putts, 0) / scoresWithPutts.length).toFixed(1);
  }
}
