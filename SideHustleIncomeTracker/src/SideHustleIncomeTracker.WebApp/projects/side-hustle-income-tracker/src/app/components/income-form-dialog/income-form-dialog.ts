import { Component, inject, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { Income } from '../../models';
import { IncomeService, BusinessService } from '../../services';

@Component({
  selector: 'app-income-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  templateUrl: './income-form-dialog.html',
  styleUrl: './income-form-dialog.scss'
})
export class IncomeFormDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _incomeService = inject(IncomeService);
  private _businessService = inject(BusinessService);
  private _dialogRef = inject(MatDialogRef<IncomeFormDialog>);

  form: FormGroup;
  isEditMode: boolean;
  businesses$ = this._businessService.businesses$;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { income?: Income }) {
    this.isEditMode = !!data?.income;

    this.form = this._fb.group({
      businessId: [data?.income?.businessId || '', Validators.required],
      description: [data?.income?.description || '', Validators.required],
      amount: [data?.income?.amount || 0, [Validators.required, Validators.min(0)]],
      incomeDate: [data?.income?.incomeDate ? new Date(data.income.incomeDate) : new Date(), Validators.required],
      client: [data?.income?.client || ''],
      invoiceNumber: [data?.income?.invoiceNumber || ''],
      isPaid: [data?.income?.isPaid ?? true],
      notes: [data?.income?.notes || '']
    });
  }

  ngOnInit(): void {
    this._businessService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const incomeData = {
      ...formValue,
      incomeDate: formValue.incomeDate.toISOString()
    };

    if (this.isEditMode && this.data.income) {
      this._incomeService.update(this.data.income.incomeId, incomeData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error updating income:', error)
      });
    } else {
      this._incomeService.create(incomeData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error creating income:', error)
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close(false);
  }
}
