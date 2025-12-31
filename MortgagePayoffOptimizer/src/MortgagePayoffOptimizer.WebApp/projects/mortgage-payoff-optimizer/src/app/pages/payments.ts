import { Component, inject, OnInit } from '@angular/core';
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
import { MatCardModule } from '@angular/material/card';
import { PaymentService, MortgageService } from '../services';
import { Payment, CreatePayment, UpdatePayment, Mortgage } from '../models';

@Component({
  selector: 'app-payment-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatButtonModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Payment' : 'Add Payment' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="payment-form">
        <mat-form-field class="payment-form__field">
          <mat-label>Mortgage</mat-label>
          <mat-select formControlName="mortgageId" required>
            <mat-option *ngFor="let mortgage of mortgages$ | async" [value]="mortgage.mortgageId">
              {{ mortgage.propertyAddress }} - {{ mortgage.lender }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Payment Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="paymentDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Amount</mat-label>
          <input matInput type="number" formControlName="amount" required>
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Principal Amount</mat-label>
          <input matInput type="number" formControlName="principalAmount" required>
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Interest Amount</mat-label>
          <input matInput type="number" formControlName="interestAmount" required>
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Extra Principal</mat-label>
          <input matInput type="number" formControlName="extraPrincipal">
        </mat-form-field>

        <mat-form-field class="payment-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .payment-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
    }

    .payment-form__field {
      width: 100%;
    }
  `]
})
export class PaymentDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private mortgageService = inject(MortgageService);

  data: Payment | null = null;
  form: FormGroup;
  mortgages$ = this.mortgageService.mortgages$;

  constructor() {
    this.mortgageService.getMortgages().subscribe();

    this.form = this.fb.group({
      mortgageId: ['', Validators.required],
      paymentDate: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(0)]],
      principalAmount: [0, [Validators.required, Validators.min(0)]],
      interestAmount: [0, [Validators.required, Validators.min(0)]],
      extraPrincipal: [null],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        paymentDate: new Date(this.data.paymentDate)
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const payment = {
        ...formValue,
        paymentDate: formValue.paymentDate.toISOString()
      };

      // Dialog will close with the form value
    }
  }
}

@Component({
  selector: 'app-payments',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  template: `
    <div class="payments">
      <div class="payments__header">
        <h1 class="payments__title">Payments</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="payments__add-btn">
          <mat-icon>add</mat-icon>
          Add Payment
        </button>
      </div>

      <mat-card class="payments__card">
        <table mat-table [dataSource]="payments$ | async" class="payments__table">
          <ng-container matColumnDef="paymentDate">
            <th mat-header-cell *matHeaderCellDef>Payment Date</th>
            <td mat-cell *matCellDef="let payment">{{ payment.paymentDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="mortgageId">
            <th mat-header-cell *matHeaderCellDef>Mortgage</th>
            <td mat-cell *matCellDef="let payment">{{ getMortgageAddress(payment.mortgageId) }}</td>
          </ng-container>

          <ng-container matColumnDef="amount">
            <th mat-header-cell *matHeaderCellDef>Amount</th>
            <td mat-cell *matCellDef="let payment">{{ payment.amount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="principalAmount">
            <th mat-header-cell *matHeaderCellDef>Principal</th>
            <td mat-cell *matCellDef="let payment">{{ payment.principalAmount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="interestAmount">
            <th mat-header-cell *matHeaderCellDef>Interest</th>
            <td mat-cell *matCellDef="let payment">{{ payment.interestAmount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="extraPrincipal">
            <th mat-header-cell *matHeaderCellDef>Extra Principal</th>
            <td mat-cell *matCellDef="let payment">{{ payment.extraPrincipal ? (payment.extraPrincipal | currency) : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let payment">
              <button mat-icon-button (click)="openDialog(payment)" class="payments__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(payment.paymentId)" class="payments__action-btn">
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
    .payments {
      padding: 2rem;
    }

    .payments__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .payments__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 400;
    }

    .payments__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .payments__card {
      overflow-x: auto;
    }

    .payments__table {
      width: 100%;
    }

    .payments__action-btn {
      margin-right: 0.5rem;
    }
  `]
})
export class Payments implements OnInit {
  private paymentService = inject(PaymentService);
  private mortgageService = inject(MortgageService);
  private dialog = inject(MatDialog);

  payments$ = this.paymentService.payments$;
  mortgages$ = this.mortgageService.mortgages$;
  displayedColumns = ['paymentDate', 'mortgageId', 'amount', 'principalAmount', 'interestAmount', 'extraPrincipal', 'actions'];

  private mortgagesMap = new Map<string, Mortgage>();

  ngOnInit(): void {
    this.paymentService.getPayments().subscribe();
    this.mortgageService.getMortgages().subscribe(mortgages => {
      mortgages.forEach(m => this.mortgagesMap.set(m.mortgageId, m));
    });
  }

  openDialog(payment?: Payment): void {
    const dialogRef = this.dialog.open(PaymentDialog, {
      width: '600px',
      data: payment
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (payment) {
          const updatePayment: UpdatePayment = {
            paymentId: payment.paymentId,
            ...result
          };
          this.paymentService.updatePayment(updatePayment).subscribe();
        } else {
          const createPayment: CreatePayment = result;
          this.paymentService.createPayment(createPayment).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this payment?')) {
      this.paymentService.deletePayment(id).subscribe();
    }
  }

  getMortgageAddress(mortgageId: string): string {
    const mortgage = this.mortgagesMap.get(mortgageId);
    return mortgage ? mortgage.propertyAddress : 'Unknown';
  }
}
