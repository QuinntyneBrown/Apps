import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { BankrollService } from '../services';
import { Bankroll, CreateBankroll, UpdateBankroll } from '../models';

@Component({
  selector: 'app-bankroll-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Bankroll</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="dialog__content">
        <mat-form-field class="dialog__field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Amount</mat-label>
          <input matInput type="number" formControlName="amount" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Recorded Date</mat-label>
          <input matInput type="datetime-local" formControlName="recordedDate" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="!form.valid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog {
      &__content {
        display: flex;
        flex-direction: column;
        min-width: 400px;
        padding: 20px 24px;
      }

      &__field {
        width: 100%;
        margin-bottom: 16px;
      }
    }
  `]
})
export class BankrollDialog {
  private fb = inject(FormBuilder);
  private bankrollService = inject(BankrollService);
  private dialogRef = inject(MatDialog);

  data: Bankroll | null = null;
  form: FormGroup;

  constructor() {
    const now = new Date().toISOString().slice(0, 16);
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      amount: [0, [Validators.required, Validators.min(0)]],
      recordedDate: [now, Validators.required],
      notes: ['']
    });

    if (this.data) {
      const recordedDate = new Date(this.data.recordedDate).toISOString().slice(0, 16);
      this.form.patchValue({
        userId: this.data.userId,
        amount: this.data.amount,
        recordedDate: recordedDate,
        notes: this.data.notes
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const recordedDate = new Date(formValue.recordedDate).toISOString();

      if (this.data) {
        const updateData: UpdateBankroll = {
          bankrollId: this.data.bankrollId,
          userId: formValue.userId,
          amount: formValue.amount,
          recordedDate: recordedDate,
          notes: formValue.notes || undefined
        };
        this.bankrollService.updateBankroll(updateData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      } else {
        const createData: CreateBankroll = {
          userId: formValue.userId,
          amount: formValue.amount,
          recordedDate: recordedDate,
          notes: formValue.notes || undefined
        };
        this.bankrollService.createBankroll(createData).subscribe(() => {
          this.dialogRef.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-bankrolls',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="bankrolls">
      <div class="bankrolls__container">
        <div class="bankrolls__header">
          <h1 class="bankrolls__title">Bankrolls</h1>
          <button mat-raised-button color="primary" (click)="openDialog()">
            <mat-icon>add</mat-icon>
            Add Bankroll
          </button>
        </div>

        <mat-card class="bankrolls__card">
          <table mat-table [dataSource]="bankrolls$ | async" class="bankrolls__table">
            <ng-container matColumnDef="recordedDate">
              <th mat-header-cell *matHeaderCellDef>Recorded Date</th>
              <td mat-cell *matCellDef="let bankroll">{{ bankroll.recordedDate | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let bankroll">\${{ bankroll.amount | number:'1.2-2' }}</td>
            </ng-container>

            <ng-container matColumnDef="notes">
              <th mat-header-cell *matHeaderCellDef>Notes</th>
              <td mat-cell *matCellDef="let bankroll">{{ bankroll.notes || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let bankroll">
                <button mat-icon-button color="primary" (click)="openDialog(bankroll)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteBankroll(bankroll.bankrollId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .bankrolls {
      padding: 24px;

      &__container {
        max-width: 1400px;
        margin: 0 auto;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 32px;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class Bankrolls implements OnInit {
  private bankrollService = inject(BankrollService);
  private dialog = inject(MatDialog);

  bankrolls$ = this.bankrollService.bankrolls$;
  displayedColumns = ['recordedDate', 'amount', 'notes', 'actions'];

  ngOnInit() {
    this.bankrollService.getBankrolls().subscribe();
  }

  openDialog(bankroll?: Bankroll) {
    const dialogRef = this.dialog.open(BankrollDialog, {
      width: '500px'
    });

    if (bankroll) {
      dialogRef.componentInstance.data = bankroll;
      const recordedDate = new Date(bankroll.recordedDate).toISOString().slice(0, 16);
      dialogRef.componentInstance.form.patchValue({
        userId: bankroll.userId,
        amount: bankroll.amount,
        recordedDate: recordedDate,
        notes: bankroll.notes
      });
    }
  }

  deleteBankroll(id: string) {
    if (confirm('Are you sure you want to delete this bankroll?')) {
      this.bankrollService.deleteBankroll(id).subscribe();
    }
  }
}
