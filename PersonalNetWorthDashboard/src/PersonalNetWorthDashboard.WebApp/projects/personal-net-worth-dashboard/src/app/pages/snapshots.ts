import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { NetWorthSnapshotService } from '../services';
import { NetWorthSnapshot } from '../models';

@Component({
  selector: 'app-snapshot-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.snapshot ? 'Edit Snapshot' : 'Add Snapshot' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="snapshot-dialog__form">
        <mat-form-field class="snapshot-dialog__field">
          <mat-label>Snapshot Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="snapshotDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="snapshot-dialog__field">
          <mat-label>Total Assets</mat-label>
          <input matInput type="number" formControlName="totalAssets" required>
        </mat-form-field>

        <mat-form-field class="snapshot-dialog__field">
          <mat-label>Total Liabilities</mat-label>
          <input matInput type="number" formControlName="totalLiabilities" required>
        </mat-form-field>

        <mat-form-field class="snapshot-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .snapshot-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 400px;
        padding: 1rem 0;
      }

      &__field {
        width: 100%;
      }
    }
  `]
})
export class SnapshotDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: { snapshot?: NetWorthSnapshot } = {};
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      snapshotDate: [new Date(), Validators.required],
      totalAssets: [0, [Validators.required, Validators.min(0)]],
      totalLiabilities: [0, [Validators.required, Validators.min(0)]],
      notes: ['']
    });

    if (this.data.snapshot) {
      this.form.patchValue({
        ...this.data.snapshot,
        snapshotDate: new Date(this.data.snapshot.snapshotDate)
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        snapshotDate: formValue.snapshotDate.toISOString()
      };

      if (this.data.snapshot) {
        result.netWorthSnapshotId = this.data.snapshot.netWorthSnapshotId;
      }
    }
  }
}

@Component({
  selector: 'app-snapshots',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="snapshots">
      <div class="snapshots__header">
        <h1 class="snapshots__title">Net Worth Snapshots</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Snapshot
        </button>
      </div>

      <div class="snapshots__table-container">
        <table mat-table [dataSource]="snapshots$ | async" class="snapshots__table">
          <ng-container matColumnDef="snapshotDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let snapshot">{{ snapshot.snapshotDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="totalAssets">
            <th mat-header-cell *matHeaderCellDef>Total Assets</th>
            <td mat-cell *matCellDef="let snapshot">{{ snapshot.totalAssets | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="totalLiabilities">
            <th mat-header-cell *matHeaderCellDef>Total Liabilities</th>
            <td mat-cell *matCellDef="let snapshot">{{ snapshot.totalLiabilities | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="netWorth">
            <th mat-header-cell *matHeaderCellDef>Net Worth</th>
            <td mat-cell *matCellDef="let snapshot" [class.snapshots__net-worth--positive]="snapshot.netWorth >= 0" [class.snapshots__net-worth--negative]="snapshot.netWorth < 0">
              {{ snapshot.netWorth | currency }}
            </td>
          </ng-container>

          <ng-container matColumnDef="notes">
            <th mat-header-cell *matHeaderCellDef>Notes</th>
            <td mat-cell *matCellDef="let snapshot">{{ snapshot.notes || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let snapshot">{{ snapshot.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let snapshot">
              <button mat-icon-button color="primary" (click)="openDialog(snapshot)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(snapshot.netWorthSnapshotId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .snapshots {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        color: #333;
      }

      &__table-container {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
        background: white;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      }

      &__net-worth--positive {
        color: #4caf50;
        font-weight: 600;
      }

      &__net-worth--negative {
        color: #f44336;
        font-weight: 600;
      }
    }
  `]
})
export class Snapshots implements OnInit {
  private snapshotService = inject(NetWorthSnapshotService);
  private dialog = inject(MatDialog);

  snapshots$ = this.snapshotService.snapshots$;
  displayedColumns = ['snapshotDate', 'totalAssets', 'totalLiabilities', 'netWorth', 'notes', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.snapshotService.getSnapshots().subscribe();
  }

  openDialog(snapshot?: NetWorthSnapshot): void {
    const dialogRef = this.dialog.open(SnapshotDialog, {
      width: '500px',
      data: { snapshot }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (snapshot) {
          this.snapshotService.updateSnapshot(result).subscribe();
        } else {
          this.snapshotService.createSnapshot(result).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this snapshot?')) {
      this.snapshotService.deleteSnapshot(id).subscribe();
    }
  }
}
