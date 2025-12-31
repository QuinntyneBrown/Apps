import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CampsiteService, TripService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  campsites$ = this.campsiteService.campsites$;
  trips$ = this.tripService.trips$;

  // For demo purposes, using a hardcoded userId
  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private campsiteService: CampsiteService,
    private tripService: TripService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.campsiteService.getCampsites(this.userId).subscribe();
    this.tripService.getTrips(this.userId).subscribe();
  }
}
