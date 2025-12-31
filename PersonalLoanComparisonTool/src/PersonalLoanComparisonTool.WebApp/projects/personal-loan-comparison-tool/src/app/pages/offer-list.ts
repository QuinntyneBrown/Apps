import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { OfferService } from '../services';

@Component({
  selector: 'app-offer-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="offer-list">
      <div class="offer-list__header">
        <h1 class="offer-list__title">Offers</h1>
        <button mat-raised-button color="primary" routerLink="/offers/new" class="offer-list__add-button">
          <mat-icon>add</mat-icon>
          Add Offer
        </button>
      </div>

      <mat-card class="offer-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="(offerService.offers$ | async) || []" class="offer-list__table">
            <ng-container matColumnDef="lenderName">
              <th mat-header-cell *matHeaderCellDef>Lender</th>
              <td mat-cell *matCellDef="let offer">{{ offer.lenderName }}</td>
            </ng-container>

            <ng-container matColumnDef="loanAmount">
              <th mat-header-cell *matHeaderCellDef>Loan Amount</th>
              <td mat-cell *matCellDef="let offer">{{ offer.loanAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="interestRate">
              <th mat-header-cell *matHeaderCellDef>Interest Rate</th>
              <td mat-cell *matCellDef="let offer">{{ offer.interestRate }}%</td>
            </ng-container>

            <ng-container matColumnDef="termMonths">
              <th mat-header-cell *matHeaderCellDef>Term (Months)</th>
              <td mat-cell *matCellDef="let offer">{{ offer.termMonths }}</td>
            </ng-container>

            <ng-container matColumnDef="monthlyPayment">
              <th mat-header-cell *matHeaderCellDef>Monthly Payment</th>
              <td mat-cell *matCellDef="let offer">{{ offer.monthlyPayment | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="totalCost">
              <th mat-header-cell *matHeaderCellDef>Total Cost</th>
              <td mat-cell *matCellDef="let offer">{{ offer.totalCost | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="fees">
              <th mat-header-cell *matHeaderCellDef>Fees</th>
              <td mat-cell *matCellDef="let offer">{{ offer.fees | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let offer">
                <button mat-icon-button color="primary" [routerLink]="['/offers', offer.offerId, 'edit']" class="offer-list__action-button">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteOffer(offer.offerId)" class="offer-list__action-button">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .offer-list {
      padding: 24px;
    }

    .offer-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .offer-list__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .offer-list__add-button {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .offer-list__card {
      overflow-x: auto;
    }

    .offer-list__table {
      width: 100%;
    }

    .offer-list__action-button {
      margin-right: 8px;
    }
  `]
})
export class OfferList implements OnInit {
  offerService = inject(OfferService);
  displayedColumns = ['lenderName', 'loanAmount', 'interestRate', 'termMonths', 'monthlyPayment', 'totalCost', 'fees', 'actions'];

  ngOnInit(): void {
    this.offerService.getAll().subscribe();
  }

  deleteOffer(id: string): void {
    if (confirm('Are you sure you want to delete this offer?')) {
      this.offerService.delete(id).subscribe();
    }
  }
}
