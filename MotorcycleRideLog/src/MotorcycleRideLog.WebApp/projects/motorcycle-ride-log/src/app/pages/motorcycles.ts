import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MotorcycleService } from '../services';
import { MotorcycleTypeLabels } from '../models';

@Component({
  selector: 'app-motorcycles',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './motorcycles.html',
  styleUrl: './motorcycles.scss'
})
export class Motorcycles implements OnInit {
  private readonly motorcycleService = inject(MotorcycleService);

  motorcycles$ = this.motorcycleService.motorcycles$;
  displayedColumns = ['make', 'model', 'year', 'type', 'licensePlate', 'currentMileage', 'actions'];
  motorcycleTypeLabels = MotorcycleTypeLabels;

  ngOnInit(): void {
    this.motorcycleService.getAll().subscribe();
  }

  deleteMotorcycle(id: string): void {
    if (confirm('Are you sure you want to delete this motorcycle?')) {
      this.motorcycleService.delete(id).subscribe();
    }
  }
}
