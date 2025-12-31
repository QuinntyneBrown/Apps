import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ItemService, ValueEstimateService } from '../../services';
import { Item, ValueEstimate, CategoryLabels, RoomLabels } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class Dashboard implements OnInit {
  private itemService = inject(ItemService);
  private valueEstimateService = inject(ValueEstimateService);
  private router = inject(Router);

  items$ = this.itemService.items$;
  valueEstimates$ = this.valueEstimateService.valueEstimates$;
  loading$ = this.itemService.loading$;

  totalItems = 0;
  totalValue = 0;
  estimatesCount = 0;
  recentItems: Item[] = [];

  readonly categoryLabels = CategoryLabels;
  readonly roomLabels = RoomLabels;

  ngOnInit(): void {
    const userId = '00000000-0000-0000-0000-000000000001'; // Demo user ID
    this.itemService.getItems(userId).subscribe();
    this.valueEstimateService.getValueEstimates().subscribe();

    this.items$.subscribe(items => {
      this.totalItems = items.length;
      this.totalValue = items.reduce((sum, item) => sum + (item.currentValue || 0), 0);
      this.recentItems = items
        .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
        .slice(0, 5);
    });

    this.valueEstimates$.subscribe(estimates => {
      this.estimatesCount = estimates.length;
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
  }

  navigateToItems(): void {
    this.router.navigate(['/items']);
  }

  navigateToValueEstimates(): void {
    this.router.navigate(['/value-estimates']);
  }
}
