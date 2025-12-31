import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { map } from 'rxjs/operators';
import { combineLatest } from 'rxjs';
import { VehicleService, ServiceRecordService, MaintenanceScheduleService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private vehicleService = inject(VehicleService);
  private serviceRecordService = inject(ServiceRecordService);
  private maintenanceScheduleService = inject(MaintenanceScheduleService);

  viewModel$ = combineLatest([
    this.vehicleService.vehicles$,
    this.serviceRecordService.serviceRecords$,
    this.maintenanceScheduleService.schedules$
  ]).pipe(
    map(([vehicles, serviceRecords, schedules]) => {
      const activeVehicles = vehicles.filter(v => v.isActive);
      const recentServices = serviceRecords
        .sort((a, b) => new Date(b.serviceDate).getTime() - new Date(a.serviceDate).getTime())
        .slice(0, 5);
      const upcomingMaintenance = schedules
        .filter(s => s.isActive && s.nextServiceDate)
        .sort((a, b) => {
          const dateA = a.nextServiceDate ? new Date(a.nextServiceDate).getTime() : Infinity;
          const dateB = b.nextServiceDate ? new Date(b.nextServiceDate).getTime() : Infinity;
          return dateA - dateB;
        })
        .slice(0, 5);

      return {
        totalVehicles: activeVehicles.length,
        totalServices: serviceRecords.length,
        totalSchedules: schedules.filter(s => s.isActive).length,
        recentServices,
        upcomingMaintenance,
        vehicles: activeVehicles
      };
    })
  );

  constructor() {
    this.vehicleService.getVehicles().subscribe();
    this.serviceRecordService.getServiceRecords().subscribe();
    this.maintenanceScheduleService.getSchedules().subscribe();
  }
}
