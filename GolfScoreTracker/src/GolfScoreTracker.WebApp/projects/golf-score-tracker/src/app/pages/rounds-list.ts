import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { RoundsService } from '../services';

@Component({
  selector: 'app-rounds-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  template: `
    <div class="rounds-list">
      <div class="rounds-list__header">
        <h1>Rounds</h1>
        <a mat-raised-button color="primary" routerLink="/rounds/new">
          <mat-icon>add</mat-icon>
          New Round
        </a>
      </div>

      <mat-card class="rounds-list__card">
        <mat-card-content>
          <div *ngIf="(rounds$ | async) as rounds">
            <p *ngIf="rounds.length === 0" class="rounds-list__empty">No rounds found. Record your first round!</p>

            <table mat-table [dataSource]="rounds" *ngIf="rounds.length > 0" class="rounds-list__table">
              <ng-container matColumnDef="date">
                <th mat-header-cell *matHeaderCellDef>Date</th>
                <td mat-cell *matCellDef="let round">{{ round.playedDate | date:'shortDate' }}</td>
              </ng-container>

              <ng-container matColumnDef="course">
                <th mat-header-cell *matHeaderCellDef>Course</th>
                <td mat-cell *matCellDef="let round">{{ round.courseName || 'Unknown Course' }}</td>
              </ng-container>

              <ng-container matColumnDef="score">
                <th mat-header-cell *matHeaderCellDef>Score</th>
                <td mat-cell *matCellDef="let round">{{ round.totalScore }}</td>
              </ng-container>

              <ng-container matColumnDef="par">
                <th mat-header-cell *matHeaderCellDef>Par</th>
                <td mat-cell *matCellDef="let round">{{ round.totalPar }}</td>
              </ng-container>

              <ng-container matColumnDef="relative">
                <th mat-header-cell *matHeaderCellDef>+/-</th>
                <td mat-cell *matCellDef="let round" [class.rounds-list__over-par]="round.totalScore > round.totalPar" [class.rounds-list__under-par]="round.totalScore < round.totalPar">
                  {{ round.totalScore - round.totalPar > 0 ? '+' : '' }}{{ round.totalScore - round.totalPar }}
                </td>
              </ng-container>

              <ng-container matColumnDef="weather">
                <th mat-header-cell *matHeaderCellDef>Weather</th>
                <td mat-cell *matCellDef="let round">{{ round.weather || 'N/A' }}</td>
              </ng-container>

              <ng-container matColumnDef="actions">
                <th mat-header-cell *matHeaderCellDef>Actions</th>
                <td mat-cell *matCellDef="let round">
                  <a mat-button color="primary" [routerLink]="['/rounds', round.roundId]">View</a>
                  <a mat-button color="accent" [routerLink]="['/rounds', round.roundId, 'edit']">Edit</a>
                  <button mat-button color="warn" (click)="deleteRound(round.roundId)">Delete</button>
                </td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
            </table>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .rounds-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .rounds-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .rounds-list__header h1 {
      margin: 0;
    }

    .rounds-list__card {
      margin-top: 1rem;
    }

    .rounds-list__empty {
      text-align: center;
      padding: 2rem;
      color: #666;
    }

    .rounds-list__table {
      width: 100%;
    }

    .rounds-list__over-par {
      color: #f44336;
    }

    .rounds-list__under-par {
      color: #4caf50;
    }
  `]
})
export class RoundsList implements OnInit {
  rounds$ = this.roundsService.rounds$;
  displayedColumns: string[] = ['date', 'course', 'score', 'par', 'relative', 'weather', 'actions'];

  constructor(private roundsService: RoundsService) {}

  ngOnInit(): void {
    this.roundsService.getRounds().subscribe();
  }

  deleteRound(id: string): void {
    if (confirm('Are you sure you want to delete this round?')) {
      this.roundsService.deleteRound(id).subscribe();
    }
  }
}
