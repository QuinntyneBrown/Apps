import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Subscription, BillingCycle, SubscriptionStatus, Category } from '../../models';
import { SubscriptionService, CategoryService } from '../../services';

export interface SubscriptionFormData {
  subscription?: Subscription;
  mode: 'create' | 'edit';
}

@Component({
  selector: 'app-subscription-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './subscription-form.html',
  styleUrl: './subscription-form.scss'
})
export class SubscriptionForm implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<SubscriptionForm>);
  private _data = inject<SubscriptionFormData>(MAT_DIALOG_DATA);
  private _subscriptionService = inject(SubscriptionService);
  private _categoryService = inject(CategoryService);

  form!: FormGroup;
  billingCycles = [
    { value: BillingCycle.Weekly, label: 'Weekly' },
    { value: BillingCycle.Monthly, label: 'Monthly' },
    { value: BillingCycle.Quarterly, label: 'Quarterly' },
    { value: BillingCycle.Annual, label: 'Annual' }
  ];
  statuses = [
    { value: SubscriptionStatus.Active, label: 'Active' },
    { value: SubscriptionStatus.Paused, label: 'Paused' },
    { value: SubscriptionStatus.Cancelled, label: 'Cancelled' },
    { value: SubscriptionStatus.Pending, label: 'Pending' }
  ];
  categories$ = this._categoryService.categories$;

  get isEditMode(): boolean {
    return this._data.mode === 'edit';
  }

  get title(): string {
    return this.isEditMode ? 'Edit Subscription' : 'Add Subscription';
  }

  ngOnInit(): void {
    this._categoryService.getCategories().subscribe();

    this.form = this._fb.group({
      serviceName: [this._data.subscription?.serviceName || '', Validators.required],
      cost: [this._data.subscription?.cost || 0, [Validators.required, Validators.min(0)]],
      billingCycle: [this._data.subscription?.billingCycle ?? BillingCycle.Monthly, Validators.required],
      nextBillingDate: [
        this._data.subscription?.nextBillingDate ? new Date(this._data.subscription.nextBillingDate) : new Date(),
        Validators.required
      ],
      startDate: [
        this._data.subscription?.startDate ? new Date(this._data.subscription.startDate) : new Date(),
        Validators.required
      ],
      categoryId: [this._data.subscription?.categoryId || null],
      notes: [this._data.subscription?.notes || ''],
      status: [this._data.subscription?.status ?? SubscriptionStatus.Active]
    });

    if (!this.isEditMode) {
      this.form.removeControl('status');
    }
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const request = {
      ...formValue,
      nextBillingDate: formValue.nextBillingDate.toISOString(),
      startDate: formValue.startDate.toISOString()
    };

    if (this.isEditMode && this._data.subscription) {
      const updateRequest = {
        ...request,
        subscriptionId: this._data.subscription.subscriptionId,
        cancellationDate: this._data.subscription.cancellationDate
      };
      this._subscriptionService.updateSubscription(updateRequest).subscribe({
        next: (result) => this._dialogRef.close(result),
        error: (error) => console.error('Error updating subscription:', error)
      });
    } else {
      this._subscriptionService.createSubscription(request).subscribe({
        next: (result) => this._dialogRef.close(result),
        error: (error) => console.error('Error creating subscription:', error)
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
