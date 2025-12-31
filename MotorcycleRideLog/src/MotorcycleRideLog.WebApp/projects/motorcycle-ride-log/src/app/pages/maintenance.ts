import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MaintenanceService } from '../services';
import { MaintenanceTypeLabels } from '../models';

@Component({
  selector: 'app-maintenance',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './maintenance.html',
  styleUrl: './maintenance.scss'
})
export class MaintenancePage implements OnInit {
  private readonly maintenanceService = inject(MaintenanceService);

  maintenance$ = this.maintenanceService.maintenance$;
  displayedColumns = ['maintenanceDate', 'type', 'description', 'mileageAtMaintenance', 'cost', 'serviceProvider', 'actions'];
  maintenanceTypeLabels = MaintenanceTypeLabels;

  ngOnInit(): void {
    this.maintenanceService.getAll().subscribe();
  }

  deleteMaintenance(id: string): void {
    if (confirm('Are you sure you want to delete this maintenance record?')) {
      this.maintenanceService.delete(id).subscribe();
    }
  }
}
