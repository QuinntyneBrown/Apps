import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { RoundsService, HoleScoresService } from '../services';
import { HoleScore } from '../models';

@Component({
  selector: 'app-round-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  template: `
    <div class="round-detail">
      <div class="round-detail__header">
        <button mat-button routerLink="/rounds">
          <mat-icon>arrow_back</mat-icon>
          Back to Rounds
        </button>
      </div>

      <mat-card *ngIf="(round$ | async) as round" class="round-detail__card">
        <mat-card-header>
          <mat-card-title>Round Details</mat-card-title>
          <mat-card-subtitle>{{ round.courseName }} - {{ round.playedDate | date:'fullDate' }}</mat-card-subtitle>
        </mat-card-header>

        <mat-card-content>
          <div class="round-detail__info">
            <div class="round-detail__info-item">
              <strong>Total Score:</strong> {{ round.totalScore }}
            </div>
            <div class="round-detail__info-item">
              <strong>Par:</strong> {{ round.totalPar }}
            </div>
            <div class="round-detail__info-item">
              <strong>Score vs Par:</strong>
              <span [class.round-detail__over-par]="round.totalScore > round.totalPar" [class.round-detail__under-par]="round.totalScore < round.totalPar">
                {{ round.totalScore - round.totalPar > 0 ? '+' : '' }}{{ round.totalScore - round.totalPar }}
              </span>
            </div>
            <div class="round-detail__info-item" *ngIf="round.weather">
              <strong>Weather:</strong> {{ round.weather }}
            </div>
            <div class="round-detail__info-item" *ngIf="round.notes">
              <strong>Notes:</strong> {{ round.notes }}
            </div>
            <div class="round-detail__info-item">
              <strong>Created:</strong> {{ round.createdAt | date:'medium' }}
            </div>
          </div>

          <div class="round-detail__scorecard" *ngIf="(holeScores$ | async) as holeScores">
            <h3>Scorecard</h3>
            <p *ngIf="holeScores.length === 0">No hole scores recorded yet.</p>

            <table mat-table [dataSource]="holeScores" *ngIf="holeScores.length > 0" class="round-detail__table">
              <ng-container matColumnDef="hole">
                <th mat-header-cell *matHeaderCellDef>Hole</th>
                <td mat-cell *matCellDef="let score">{{ score.holeNumber }}</td>
              </ng-container>

              <ng-container matColumnDef="par">
                <th mat-header-cell *matHeaderCellDef>Par</th>
                <td mat-cell *matCellDef="let score">{{ score.par }}</td>
              </ng-container>

              <ng-container matColumnDef="score">
                <th mat-header-cell *matHeaderCellDef>Score</th>
                <td mat-cell *matCellDef="let score">{{ score.score }}</td>
              </ng-container>

              <ng-container matColumnDef="putts">
                <th mat-header-cell *matHeaderCellDef>Putts</th>
                <td mat-cell *matCellDef="let score">{{ score.putts || 'N/A' }}</td>
              </ng-container>

              <ng-container matColumnDef="fir">
                <th mat-header-cell *matHeaderCellDef>FIR</th>
                <td mat-cell *matCellDef="let score">{{ score.fairwayHit ? 'Yes' : 'No' }}</td>
              </ng-container>

              <ng-container matColumnDef="gir">
                <th mat-header-cell *matHeaderCellDef>GIR</th>
                <td mat-cell *matCellDef="let score">{{ score.greenInRegulation ? 'Yes' : 'No' }}</td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="scoreDisplayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: scoreDisplayedColumns;"></tr>
            </table>
          </div>
        </mat-card-content>

        <mat-card-actions>
          <a mat-raised-button color="primary" [routerLink]="['/rounds', round.roundId, 'edit']">
            <mat-icon>edit</mat-icon>
            Edit
          </a>
          <button mat-raised-button color="warn" (click)="deleteRound(round.roundId)">
            <mat-icon>delete</mat-icon>
            Delete
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .round-detail {
      padding: 2rem;
      max-width: 1000px;
      margin: 0 auto;
    }

    .round-detail__header {
      margin-bottom: 1rem;
    }

    .round-detail__card {
      margin-top: 1rem;
    }

    .round-detail__info {
      display: grid;
      gap: 1rem;
      margin-top: 1rem;
    }

    .round-detail__info-item {
      padding: 0.5rem 0;
      border-bottom: 1px solid #eee;
    }

    .round-detail__info-item:last-child {
      border-bottom: none;
    }

    .round-detail__over-par {
      color: #f44336;
    }

    .round-detail__under-par {
      color: #4caf50;
    }

    .round-detail__scorecard {
      margin-top: 2rem;
    }

    .round-detail__table {
      width: 100%;
      margin-top: 1rem;
    }
  `]
})
export class RoundDetail implements OnInit {
  round$ = this.roundsService.currentRound$;
  holeScores$ = this.holeScoresService.holeScores$;
  scoreDisplayedColumns: string[] = ['hole', 'par', 'score', 'putts', 'fir', 'gir'];
  private roundId: string = '';

  constructor(
    private roundsService: RoundsService,
    private holeScoresService: HoleScoresService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.roundId = this.route.snapshot.paramMap.get('id')!;
    this.roundsService.getRoundById(this.roundId).subscribe();
    this.holeScoresService.getHoleScores(this.roundId).subscribe();
  }

  deleteRound(id: string): void {
    if (confirm('Are you sure you want to delete this round?')) {
      this.roundsService.deleteRound(id).subscribe(() => {
        this.router.navigate(['/rounds']);
      });
    }
  }
}
