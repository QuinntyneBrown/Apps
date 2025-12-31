import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatBadgeModule } from '@angular/material/badge';
import { CatchesService } from '../services';
import { Catch, FishSpecies } from '../models';

@Component({
  selector: 'app-catches',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatChipsModule, MatBadgeModule],
  template: `
    <div class="catches">
      <div class="catches__header">
        <h1 class="catches__title">Catches</h1>
        <button mat-raised-button color="primary" (click)="onCreateCatch()">
          <mat-icon>add</mat-icon>
          New Catch
        </button>
      </div>

      <mat-card>
        <mat-card-content>
          <table mat-table [dataSource]="catches$ | async" class="catches__table">
            <ng-container matColumnDef="catchTime">
              <th mat-header-cell *matHeaderCellDef>Time</th>
              <td mat-cell *matCellDef="let catch">{{ catch.catchTime | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="species">
              <th mat-header-cell *matHeaderCellDef>Species</th>
              <td mat-cell *matCellDef="let catch">
                <mat-chip-set>
                  <mat-chip>{{ getSpeciesName(catch.species) }}</mat-chip>
                </mat-chip-set>
              </td>
            </ng-container>

            <ng-container matColumnDef="size">
              <th mat-header-cell *matHeaderCellDef>Size</th>
              <td mat-cell *matCellDef="let catch">
                <div class="catches__size">
                  <span *ngIf="catch.length">{{ catch.length }}"</span>
                  <span *ngIf="catch.weight">{{ catch.weight }} lbs</span>
                  <span *ngIf="!catch.length && !catch.weight">N/A</span>
                </div>
              </td>
            </ng-container>

            <ng-container matColumnDef="bait">
              <th mat-header-cell *matHeaderCellDef>Bait</th>
              <td mat-cell *matCellDef="let catch">{{ catch.baitUsed || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="released">
              <th mat-header-cell *matHeaderCellDef>Released</th>
              <td mat-cell *matCellDef="let catch">
                <mat-icon [class.catches__released--yes]="catch.wasReleased"
                          [class.catches__released--no]="!catch.wasReleased">
                  {{ catch.wasReleased ? 'check_circle' : 'cancel' }}
                </mat-icon>
              </td>
            </ng-container>

            <ng-container matColumnDef="photo">
              <th mat-header-cell *matHeaderCellDef>Photo</th>
              <td mat-cell *matCellDef="let catch">
                <mat-icon *ngIf="catch.photoUrl" class="catches__photo-icon">photo_camera</mat-icon>
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let catch">
                <button mat-icon-button (click)="onEditCatch(catch)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="onDeleteCatch(catch)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="catches__row"></tr>
          </table>

          <div *ngIf="(catches$ | async)?.length === 0" class="catches__empty">
            <mat-icon>phishing</mat-icon>
            <p>No catches yet. Record your first catch!</p>
            <button mat-raised-button color="primary" (click)="onCreateCatch()">
              Record Catch
            </button>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .catches {
      padding: 24px;
    }

    .catches__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .catches__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .catches__table {
      width: 100%;
    }

    .catches__row:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    .catches__size {
      display: flex;
      gap: 8px;
    }

    .catches__released--yes {
      color: #4caf50;
    }

    .catches__released--no {
      color: #f44336;
    }

    .catches__photo-icon {
      color: #3f51b5;
    }

    .catches__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 48px;
      text-align: center;
    }

    .catches__empty mat-icon {
      font-size: 72px;
      width: 72px;
      height: 72px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 16px;
    }

    .catches__empty p {
      margin: 0 0 24px 0;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Catches implements OnInit {
  catches$ = this.catchesService.catches$;
  displayedColumns: string[] = ['catchTime', 'species', 'size', 'bait', 'released', 'photo', 'actions'];

  constructor(private catchesService: CatchesService) {}

  ngOnInit(): void {
    this.catchesService.getCatches().subscribe();
  }

  getSpeciesName(species: FishSpecies): string {
    return FishSpecies[species];
  }

  onCreateCatch(): void {
    // TODO: Open dialog to create catch
    console.log('Create catch');
  }

  onEditCatch(catchData: Catch): void {
    // TODO: Open dialog to edit catch
    console.log('Edit catch', catchData);
  }

  onDeleteCatch(catchData: Catch): void {
    if (confirm(`Are you sure you want to delete this catch?`)) {
      this.catchesService.deleteCatch(catchData.catchId).subscribe();
    }
  }
}
