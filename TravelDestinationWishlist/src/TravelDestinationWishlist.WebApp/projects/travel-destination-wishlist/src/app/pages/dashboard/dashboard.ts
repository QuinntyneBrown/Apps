import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { map, combineLatest } from 'rxjs';
import { DestinationService, TripService, MemoryService } from '../../services';

@Component({
  selector: 'app-dashboard',
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private destinationService = inject(DestinationService);
  private tripService = inject(TripService);
  private memoryService = inject(MemoryService);

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  stats$ = combineLatest([
    this.destinationService.destinations$,
    this.tripService.trips$,
    this.memoryService.memories$
  ]).pipe(
    map(([destinations, trips, memories]) => ({
      totalDestinations: destinations.length,
      visitedDestinations: destinations.filter(d => d.isVisited).length,
      pendingDestinations: destinations.filter(d => !d.isVisited).length,
      totalTrips: trips.length,
      totalMemories: memories.length
    }))
  );

  ngOnInit(): void {
    this.destinationService.getDestinations(this.userId).subscribe();
    this.tripService.getTrips(this.userId).subscribe();
    this.memoryService.getMemories(this.userId).subscribe();
  }
}
