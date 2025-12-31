import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RegistryService, RegistryItemService, ContributionService } from '../../services';
import { Observable, combineLatest, map } from 'rxjs';

interface DashboardStats {
  totalRegistries: number;
  activeRegistries: number;
  totalItems: number;
  fulfilledItems: number;
  totalContributions: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private registryService = inject(RegistryService);
  private registryItemService = inject(RegistryItemService);
  private contributionService = inject(ContributionService);

  stats$!: Observable<DashboardStats>;
  loading$!: Observable<boolean>;

  ngOnInit(): void {
    const userId = '00000000-0000-0000-0000-000000000001';

    this.registryService.getRegistries(userId).subscribe();
    this.registryItemService.getRegistryItems().subscribe();
    this.contributionService.getContributions().subscribe();

    this.stats$ = combineLatest([
      this.registryService.registries$,
      this.registryItemService.registryItems$,
      this.contributionService.contributions$
    ]).pipe(
      map(([registries, items, contributions]) => ({
        totalRegistries: registries.length,
        activeRegistries: registries.filter(r => r.isActive).length,
        totalItems: items.length,
        fulfilledItems: items.filter(i => i.isFulfilled).length,
        totalContributions: contributions.length
      }))
    );

    this.loading$ = combineLatest([
      this.registryService.loading$,
      this.registryItemService.loading$,
      this.contributionService.loading$
    ]).pipe(
      map(([l1, l2, l3]) => l1 || l2 || l3)
    );
  }
}
