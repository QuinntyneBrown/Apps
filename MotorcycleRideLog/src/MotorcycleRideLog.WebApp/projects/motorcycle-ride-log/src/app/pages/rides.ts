import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { RideService } from '../services';
import { RideTypeLabels, WeatherConditionLabels } from '../models';

@Component({
  selector: 'app-rides',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './rides.html',
  styleUrl: './rides.scss'
})
export class Rides implements OnInit {
  private readonly rideService = inject(RideService);

  rides$ = this.rideService.rides$;
  displayedColumns = ['rideDate', 'startLocation', 'endLocation', 'distanceMiles', 'type', 'weather', 'rating', 'actions'];
  rideTypeLabels = RideTypeLabels;
  weatherConditionLabels = WeatherConditionLabels;

  ngOnInit(): void {
    this.rideService.getAll().subscribe();
  }

  deleteRide(id: string): void {
    if (confirm('Are you sure you want to delete this ride?')) {
      this.rideService.delete(id).subscribe();
    }
  }
}
