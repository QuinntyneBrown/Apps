import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { VehicleService } from '../../services';
import { VehicleCard } from '../../components/vehicle-card';
import { VehicleDialog } from '../../components/vehicle-dialog';
import { Vehicle } from '../../models';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, MatSnackBarModule, VehicleCard],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.scss'
})
export class Vehicles {
  private vehicleService = inject(VehicleService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  vehicles$ = this.vehicleService.vehicles$;

  constructor() {
    this.vehicleService.getVehicles().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(VehicleDialog, {
      width: '600px',
      data: { mode: 'create' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.vehicleService.createVehicle(result).subscribe({
          next: () => {
            this.snackBar.open('Vehicle added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to add vehicle', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(vehicle: Vehicle): void {
    const dialogRef = this.dialog.open(VehicleDialog, {
      width: '600px',
      data: { mode: 'edit', vehicle }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updateRequest = {
          vehicleId: vehicle.vehicleId,
          ...result
        };
        this.vehicleService.updateVehicle(vehicle.vehicleId, updateRequest).subscribe({
          next: () => {
            this.snackBar.open('Vehicle updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update vehicle', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteVehicle(vehicle: Vehicle): void {
    if (confirm(`Are you sure you want to delete ${vehicle.year} ${vehicle.make} ${vehicle.model}?`)) {
      this.vehicleService.deleteVehicle(vehicle.vehicleId).subscribe({
        next: () => {
          this.snackBar.open('Vehicle deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete vehicle', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
