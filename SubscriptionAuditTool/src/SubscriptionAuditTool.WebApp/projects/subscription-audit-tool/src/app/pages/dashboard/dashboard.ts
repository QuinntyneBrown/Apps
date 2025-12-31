import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { map, combineLatest } from 'rxjs';
import { SubscriptionService, CategoryService } from '../../services';
import { Subscription, SubscriptionStatus, BillingCycle, Category } from '../../models';
import { SubscriptionForm } from '../../components';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule,
    MatDialogModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _subscriptionService = inject(SubscriptionService);
  private _categoryService = inject(CategoryService);
  private _dialog = inject(MatDialog);

  subscriptions$ = this._subscriptionService.subscriptions$;
  categories$ = this._categoryService.categories$;

  displayedColumns = ['serviceName', 'cost', 'billingCycle', 'nextBillingDate', 'categoryName', 'status', 'actions'];

  viewModel$ = combineLatest([
    this.subscriptions$,
    this.categories$
  ]).pipe(
    map(([subscriptions, categories]) => ({
      subscriptions,
      categories,
      totalMonthly: this._subscriptionService.calculateTotalMonthlyCost(subscriptions),
      totalAnnual: this._subscriptionService.calculateTotalAnnualCost(subscriptions),
      activeCount: subscriptions.filter(s => s.status === SubscriptionStatus.Active).length
    }))
  );

  constructor() {
    this._subscriptionService.getSubscriptions().subscribe();
    this._categoryService.getCategories().subscribe();
  }

  getBillingCycleLabel(cycle: BillingCycle): string {
    const labels = {
      [BillingCycle.Weekly]: 'Weekly',
      [BillingCycle.Monthly]: 'Monthly',
      [BillingCycle.Quarterly]: 'Quarterly',
      [BillingCycle.Annual]: 'Annual'
    };
    return labels[cycle] || 'Unknown';
  }

  getStatusLabel(status: SubscriptionStatus): string {
    const labels = {
      [SubscriptionStatus.Active]: 'Active',
      [SubscriptionStatus.Paused]: 'Paused',
      [SubscriptionStatus.Cancelled]: 'Cancelled',
      [SubscriptionStatus.Pending]: 'Pending'
    };
    return labels[status] || 'Unknown';
  }

  getStatusColor(status: SubscriptionStatus): string {
    const colors = {
      [SubscriptionStatus.Active]: 'primary',
      [SubscriptionStatus.Paused]: 'accent',
      [SubscriptionStatus.Cancelled]: '',
      [SubscriptionStatus.Pending]: 'warn'
    };
    return colors[status] || '';
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(SubscriptionForm, {
      width: '600px',
      data: { mode: 'create' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._subscriptionService.getSubscriptions().subscribe();
      }
    });
  }

  openEditDialog(subscription: Subscription): void {
    const dialogRef = this._dialog.open(SubscriptionForm, {
      width: '600px',
      data: { subscription, mode: 'edit' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._subscriptionService.getSubscriptions().subscribe();
      }
    });
  }

  deleteSubscription(subscription: Subscription): void {
    if (confirm(`Are you sure you want to delete ${subscription.serviceName}?`)) {
      this._subscriptionService.deleteSubscription(subscription.subscriptionId).subscribe({
        next: () => {
          this._subscriptionService.getSubscriptions().subscribe();
        },
        error: (error) => console.error('Error deleting subscription:', error)
      });
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
