import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatChipsModule } from '@angular/material/chips';
import { combineLatest, switchMap, map, of } from 'rxjs';
import { VehicleService, ServiceRecordService, MaintenanceScheduleService } from '../../services';
import { ServiceRecordDialog } from '../../components/service-record-dialog';
import { ServiceRecord } from '../../models';

@Component({
  selector: 'app-vehicle-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTabsModule,
    MatTableModule,
    MatDialogModule,
    MatSnackBarModule,
    MatChipsModule
  ],
  templateUrl: './vehicle-details.html',
  styleUrl: './vehicle-details.scss'
})
export class VehicleDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private vehicleService = inject(VehicleService);
  private serviceRecordService = inject(ServiceRecordService);
  private maintenanceScheduleService = inject(MaintenanceScheduleService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  serviceColumns = ['serviceDate', 'serviceType', 'mileageAtService', 'cost', 'serviceProvider', 'actions'];
  scheduleColumns = ['serviceType', 'description', 'nextServiceDate', 'nextServiceMileage', 'isActive'];

  viewModel$ = this.route.paramMap.pipe(
    switchMap(params => {
      const vehicleId = params.get('id');
      if (!vehicleId) {
        this.router.navigate(['/vehicles']);
        return of(null);
      }

      return combineLatest([
        this.vehicleService.getVehicleById(vehicleId),
        this.serviceRecordService.getServiceRecords(vehicleId),
        this.maintenanceScheduleService.getSchedules(vehicleId)
      ]).pipe(
        map(([vehicle, serviceRecords, schedules]) => ({
          vehicle,
          serviceRecords: serviceRecords.sort((a, b) =>
            new Date(b.serviceDate).getTime() - new Date(a.serviceDate).getTime()
          ),
          schedules: schedules.filter(s => s.isActive),
          totalCost: serviceRecords.reduce((sum, record) => sum + record.cost, 0)
        }))
      );
    })
  );

  ngOnInit(): void {
  }

  openAddServiceDialog(vehicleId: string): void {
    const dialogRef = this.dialog.open(ServiceRecordDialog, {
      width: '700px',
      data: { mode: 'create', vehicleId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.serviceRecordService.createServiceRecord(result).subscribe({
          next: () => {
            this.snackBar.open('Service record added successfully', 'Close', { duration: 3000 });
            const vehicleId = this.route.snapshot.paramMap.get('id');
            if (vehicleId) {
              this.serviceRecordService.getServiceRecords(vehicleId).subscribe();
            }
          },
          error: () => {
            this.snackBar.open('Failed to add service record', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditServiceDialog(serviceRecord: ServiceRecord): void {
    const dialogRef = this.dialog.open(ServiceRecordDialog, {
      width: '700px',
      data: { mode: 'edit', vehicleId: serviceRecord.vehicleId, serviceRecord }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updateRequest = {
          serviceRecordId: serviceRecord.serviceRecordId,
          ...result
        };
        this.serviceRecordService.updateServiceRecord(serviceRecord.serviceRecordId, updateRequest).subscribe({
          next: () => {
            this.snackBar.open('Service record updated successfully', 'Close', { duration: 3000 });
            const vehicleId = this.route.snapshot.paramMap.get('id');
            if (vehicleId) {
              this.serviceRecordService.getServiceRecords(vehicleId).subscribe();
            }
          },
          error: () => {
            this.snackBar.open('Failed to update service record', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteServiceRecord(serviceRecord: ServiceRecord): void {
    if (confirm('Are you sure you want to delete this service record?')) {
      this.serviceRecordService.deleteServiceRecord(serviceRecord.serviceRecordId).subscribe({
        next: () => {
          this.snackBar.open('Service record deleted successfully', 'Close', { duration: 3000 });
          const vehicleId = this.route.snapshot.paramMap.get('id');
          if (vehicleId) {
            this.serviceRecordService.getServiceRecords(vehicleId).subscribe();
          }
        },
        error: () => {
          this.snackBar.open('Failed to delete service record', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
