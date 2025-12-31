import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MotorcycleService, RideService, MaintenanceService, RouteService } from '../services';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly motorcycleService = inject(MotorcycleService);
  private readonly rideService = inject(RideService);
  private readonly maintenanceService = inject(MaintenanceService);
  private readonly routeService = inject(RouteService);

  viewModel$ = combineLatest({
    motorcycles: this.motorcycleService.motorcycles$,
    rides: this.rideService.rides$,
    maintenance: this.maintenanceService.maintenance$,
    routes: this.routeService.routes$
  }).pipe(
    map(({ motorcycles, rides, maintenance, routes }) => ({
      totalMotorcycles: motorcycles.length,
      totalRides: rides.length,
      totalMaintenance: maintenance.length,
      totalRoutes: routes.length,
      recentRides: rides.slice(0, 5),
      upcomingMaintenance: maintenance.slice(0, 5),
      favoriteRoutes: routes.filter(r => r.isFavorite),
      totalMileage: rides.reduce((sum, ride) => sum + ride.distanceMiles, 0)
    }))
  );

  ngOnInit(): void {
    this.motorcycleService.getAll().subscribe();
    this.rideService.getAll().subscribe();
    this.maintenanceService.getAll().subscribe();
    this.routeService.getAll().subscribe();
  }
}
