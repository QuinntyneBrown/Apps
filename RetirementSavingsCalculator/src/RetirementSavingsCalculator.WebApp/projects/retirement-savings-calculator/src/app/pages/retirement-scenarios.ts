import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { RetirementScenarioService } from '../services';
import { RetirementScenario } from '../models';

@Component({
  selector: 'app-retirement-scenarios',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  template: `
    <div class="retirement-scenarios">
      <div class="retirement-scenarios__header">
        <h1 class="retirement-scenarios__title">Retirement Scenarios</h1>
        <button mat-raised-button color="primary" routerLink="/retirement-scenarios/new" class="retirement-scenarios__add-btn">
          <mat-icon>add</mat-icon>
          New Scenario
        </button>
      </div>

      <mat-card class="retirement-scenarios__card">
        <mat-card-content>
          <table mat-table [dataSource]="(scenarios$ | async) || []" class="retirement-scenarios__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.name }}</td>
            </ng-container>

            <ng-container matColumnDef="currentAge">
              <th mat-header-cell *matHeaderCellDef>Current Age</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.currentAge }}</td>
            </ng-container>

            <ng-container matColumnDef="retirementAge">
              <th mat-header-cell *matHeaderCellDef>Retirement Age</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.retirementAge }}</td>
            </ng-container>

            <ng-container matColumnDef="yearsToRetirement">
              <th mat-header-cell *matHeaderCellDef>Years to Retirement</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.yearsToRetirement }}</td>
            </ng-container>

            <ng-container matColumnDef="currentSavings">
              <th mat-header-cell *matHeaderCellDef>Current Savings</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.currentSavings | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="projectedSavings">
              <th mat-header-cell *matHeaderCellDef>Projected Savings</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.projectedSavings | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="annualWithdrawalNeeded">
              <th mat-header-cell *matHeaderCellDef>Annual Withdrawal</th>
              <td mat-cell *matCellDef="let scenario">{{ scenario.annualWithdrawalNeeded | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let scenario">
                <button mat-icon-button color="primary" [routerLink]="['/retirement-scenarios', scenario.retirementScenarioId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteScenario(scenario)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(scenarios$ | async)?.length === 0" class="retirement-scenarios__empty">
            <p>No retirement scenarios found. Create your first scenario to get started!</p>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .retirement-scenarios {
      padding: 2rem;
    }

    .retirement-scenarios__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .retirement-scenarios__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .retirement-scenarios__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .retirement-scenarios__card {
      overflow-x: auto;
    }

    .retirement-scenarios__table {
      width: 100%;
    }

    .retirement-scenarios__empty {
      padding: 3rem;
      text-align: center;
      color: #666;
    }
  `]
})
export class RetirementScenarios implements OnInit {
  private scenarioService = inject(RetirementScenarioService);
  private router = inject(Router);

  scenarios$ = this.scenarioService.scenarios$;
  displayedColumns = ['name', 'currentAge', 'retirementAge', 'yearsToRetirement', 'currentSavings', 'projectedSavings', 'annualWithdrawalNeeded', 'actions'];

  ngOnInit(): void {
    this.scenarioService.loadScenarios().subscribe();
  }

  deleteScenario(scenario: RetirementScenario): void {
    if (confirm(`Are you sure you want to delete "${scenario.name}"?`)) {
      this.scenarioService.deleteScenario(scenario.retirementScenarioId).subscribe();
    }
  }
}
