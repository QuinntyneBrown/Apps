import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TripService, GearChecklistService } from '../../services';

@Component({
  selector: 'app-trip-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './trip-detail.html',
  styleUrl: './trip-detail.scss'
})
export class TripDetail implements OnInit, OnDestroy {
  trip$ = this.tripService.selectedTrip$;
  gearChecklists$ = this.gearChecklistService.gearChecklists$;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tripService: TripService,
    private gearChecklistService: GearChecklistService
  ) {}

  ngOnInit(): void {
    const tripId = this.route.snapshot.paramMap.get('id');
    if (tripId) {
      this.loadTrip(tripId);
    }
  }

  ngOnDestroy(): void {
    this.tripService.clearSelectedTrip();
  }

  loadTrip(tripId: string): void {
    this.tripService.getTripById(tripId).subscribe();
    this.gearChecklistService.getGearChecklists(undefined, tripId).subscribe();
  }

  goBack(): void {
    this.router.navigate(['/trips']);
  }

  isUpcoming(startDate: Date): boolean {
    return new Date(startDate) > new Date();
  }

  isPast(endDate: Date): boolean {
    return new Date(endDate) < new Date();
  }

  getDuration(startDate: Date, endDate: Date): number {
    const start = new Date(startDate);
    const end = new Date(endDate);
    const diff = end.getTime() - start.getTime();
    return Math.ceil(diff / (1000 * 60 * 60 * 24));
  }
}
