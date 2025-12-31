import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { GearChecklistService, TripService } from '../../services';

@Component({
  selector: 'app-gear-checklists',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatListModule],
  templateUrl: './gear-checklists.html',
  styleUrl: './gear-checklists.scss'
})
export class GearChecklists implements OnInit {
  gearChecklists$ = this.gearChecklistService.gearChecklists$;
  trips$ = this.tripService.trips$;

  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private gearChecklistService: GearChecklistService,
    private tripService: TripService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.gearChecklistService.getGearChecklists(this.userId).subscribe();
    this.tripService.getTrips(this.userId).subscribe();
  }

  getTripName(tripId: string): string {
    const trips = this.tripService['tripsSubject'].value;
    const trip = trips.find(t => t.tripId === tripId);
    return trip ? trip.name : 'Unknown Trip';
  }

  getItemsByTrip() {
    const items = this.gearChecklistService['gearChecklistsSubject'].value;
    const grouped = new Map<string, any[]>();

    items.forEach(item => {
      if (!grouped.has(item.tripId)) {
        grouped.set(item.tripId, []);
      }
      grouped.get(item.tripId)!.push(item);
    });

    return grouped;
  }
}
