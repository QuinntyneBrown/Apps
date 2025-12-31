import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { HoldingService } from '../services';
import { Holding } from '../models';

@Component({
  selector: 'app-holdings',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="holdings">
      <div class="holdings__header">
        <h1>Holdings</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Holding
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(holdings$ | async) || []" class="holdings__table">
          <ng-container matColumnDef="symbol">
            <th mat-header-cell *matHeaderCellDef>Symbol</th>
            <td mat-cell *matCellDef="let holding" class="holdings__symbol">{{ holding.symbol }}</td>
          </ng-container>

          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let holding">{{ holding.name }}</td>
          </ng-container>

          <ng-container matColumnDef="shares">
            <th mat-header-cell *matHeaderCellDef>Shares</th>
            <td mat-cell *matCellDef="let holding">{{ holding.shares | number:'1.2-4' }}</td>
          </ng-container>

          <ng-container matColumnDef="price">
            <th mat-header-cell *matHeaderCellDef>Price</th>
            <td mat-cell *matCellDef="let holding">{{ holding.currentPrice | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="value">
            <th mat-header-cell *matHeaderCellDef>Market Value</th>
            <td mat-cell *matCellDef="let holding">{{ holding.marketValue | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="gainLoss">
            <th mat-header-cell *matHeaderCellDef>Gain/Loss</th>
            <td mat-cell *matCellDef="let holding" [class.positive]="holding.unrealizedGainLoss >= 0" [class.negative]="holding.unrealizedGainLoss < 0">
              {{ holding.unrealizedGainLoss >= 0 ? '+' : '' }}{{ holding.unrealizedGainLoss | currency }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let holding">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteHolding(holding)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .holdings {
      padding: 1.5rem;
    }
    .holdings__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .holdings__table {
      width: 100%;
    }
    .holdings__symbol {
      font-weight: 600;
    }
    .positive {
      color: #4caf50;
    }
    .negative {
      color: #f44336;
    }
  `]
})
export class Holdings implements OnInit {
  private _holdingService = inject(HoldingService);

  holdings$ = this._holdingService.holdings$;
  displayedColumns = ['symbol', 'name', 'shares', 'price', 'value', 'gainLoss', 'actions'];

  ngOnInit(): void {
    this._holdingService.getAll().subscribe();
  }

  deleteHolding(holding: Holding): void {
    if (confirm(`Are you sure you want to delete ${holding.symbol}?`)) {
      this._holdingService.delete(holding.holdingId).subscribe();
    }
  }
}
