import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { PropertyService } from '../services';
import { Property } from '../models';
import { PropertyTypeLabels } from '../models';

@Component({
  selector: 'app-properties-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="properties-list">
      <div class="properties-list__header">
        <h1 class="properties-list__title">Properties</h1>
        <a mat-raised-button color="primary" routerLink="/properties/new">
          <mat-icon>add</mat-icon>
          Add Property
        </a>
      </div>

      <mat-card class="properties-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="properties" class="properties-list__table">
            <ng-container matColumnDef="address">
              <th mat-header-cell *matHeaderCellDef>Address</th>
              <td mat-cell *matCellDef="let property">{{ property.address }}</td>
            </ng-container>

            <ng-container matColumnDef="propertyType">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let property">{{ getPropertyTypeLabel(property.propertyType) }}</td>
            </ng-container>

            <ng-container matColumnDef="purchasePrice">
              <th mat-header-cell *matHeaderCellDef>Purchase Price</th>
              <td mat-cell *matCellDef="let property">{{ property.purchasePrice | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="currentValue">
              <th mat-header-cell *matHeaderCellDef>Current Value</th>
              <td mat-cell *matCellDef="let property">{{ property.currentValue | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="equity">
              <th mat-header-cell *matHeaderCellDef>Equity</th>
              <td mat-cell *matCellDef="let property">{{ property.equity | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="roi">
              <th mat-header-cell *matHeaderCellDef>ROI</th>
              <td mat-cell *matCellDef="let property">{{ property.roi | percent:'1.2-2' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let property">
                <div class="properties-list__actions">
                  <a mat-icon-button color="primary" [routerLink]="['/properties', property.propertyId]">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button mat-icon-button color="warn" (click)="deleteProperty(property.propertyId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
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
    .properties-list {
      padding: 2rem;
    }

    .properties-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .properties-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .properties-list__card {
      overflow: auto;
    }

    .properties-list__table {
      width: 100%;
    }

    .properties-list__actions {
      display: flex;
      gap: 0.5rem;
    }
  `]
})
export class PropertiesList implements OnInit {
  private readonly propertyService = inject(PropertyService);

  properties: Property[] = [];
  displayedColumns = ['address', 'propertyType', 'purchasePrice', 'currentValue', 'equity', 'roi', 'actions'];

  ngOnInit(): void {
    this.propertyService.properties$.subscribe(properties => {
      this.properties = properties;
    });
    this.propertyService.getProperties().subscribe();
  }

  getPropertyTypeLabel(type: number): string {
    return PropertyTypeLabels[type] || 'Unknown';
  }

  deleteProperty(id: string): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(id).subscribe();
    }
  }
}
