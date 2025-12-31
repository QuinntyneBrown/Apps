import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AssetService } from '../services';
import { Asset, AssetType, AssetTypeLabels } from '../models';

@Component({
  selector: 'app-asset-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.asset ? 'Edit Asset' : 'Add Asset' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="asset-dialog__form">
        <mat-form-field class="asset-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Asset Type</mat-label>
          <mat-select formControlName="assetType" required>
            <mat-option *ngFor="let type of assetTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Current Value</mat-label>
          <input matInput type="number" formControlName="currentValue" required>
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Purchase Price</mat-label>
          <input matInput type="number" formControlName="purchasePrice">
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Purchase Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="purchaseDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Institution</mat-label>
          <input matInput formControlName="institution">
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
          <mat-label>Account Number</mat-label>
          <input matInput formControlName="accountNumber">
        </mat-form-field>

        <mat-form-field class="asset-dialog__field">
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
    .asset-dialog {
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
export class AssetDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: { asset?: Asset } = {};
  form: FormGroup;
  assetTypes = Object.values(AssetType)
    .filter(v => typeof v === 'number')
    .map(v => ({ value: v as AssetType, label: AssetTypeLabels[v as AssetType] }));

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      assetType: [AssetType.Cash, Validators.required],
      currentValue: [0, [Validators.required, Validators.min(0)]],
      purchasePrice: [null],
      purchaseDate: [null],
      institution: [''],
      accountNumber: [''],
      notes: ['']
    });

    if (this.data.asset) {
      this.form.patchValue({
        ...this.data.asset,
        purchaseDate: this.data.asset.purchaseDate ? new Date(this.data.asset.purchaseDate) : null
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null
      };

      if (this.data.asset) {
        result.assetId = this.data.asset.assetId;
      }
    }
  }
}

@Component({
  selector: 'app-assets',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="assets">
      <div class="assets__header">
        <h1 class="assets__title">Assets</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Asset
        </button>
      </div>

      <div class="assets__table-container">
        <table mat-table [dataSource]="assets$ | async" class="assets__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let asset">{{ asset.name }}</td>
          </ng-container>

          <ng-container matColumnDef="assetType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let asset">{{ getAssetTypeLabel(asset.assetType) }}</td>
          </ng-container>

          <ng-container matColumnDef="currentValue">
            <th mat-header-cell *matHeaderCellDef>Current Value</th>
            <td mat-cell *matCellDef="let asset">{{ asset.currentValue | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="institution">
            <th mat-header-cell *matHeaderCellDef>Institution</th>
            <td mat-cell *matCellDef="let asset">{{ asset.institution || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="lastUpdated">
            <th mat-header-cell *matHeaderCellDef>Last Updated</th>
            <td mat-cell *matCellDef="let asset">{{ asset.lastUpdated | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let asset">
              <button mat-icon-button color="primary" (click)="openDialog(asset)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(asset.assetId)">
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
    .assets {
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
    }
  `]
})
export class Assets implements OnInit {
  private assetService = inject(AssetService);
  private dialog = inject(MatDialog);

  assets$ = this.assetService.assets$;
  displayedColumns = ['name', 'assetType', 'currentValue', 'institution', 'lastUpdated', 'actions'];

  ngOnInit(): void {
    this.assetService.getAssets().subscribe();
  }

  getAssetTypeLabel(type: AssetType): string {
    return AssetTypeLabels[type];
  }

  openDialog(asset?: Asset): void {
    const dialogRef = this.dialog.open(AssetDialog, {
      width: '500px',
      data: { asset }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (asset) {
          this.assetService.updateAsset(result).subscribe();
        } else {
          this.assetService.createAsset(result).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this asset?')) {
      this.assetService.deleteAsset(id).subscribe();
    }
  }
}
