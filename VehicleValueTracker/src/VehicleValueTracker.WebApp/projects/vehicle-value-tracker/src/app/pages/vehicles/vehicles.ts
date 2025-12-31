import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { VehicleService } from '../../services';
import { Vehicle } from '../../models';
import { VehicleDialog } from '../../components';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.scss'
})
export class Vehicles implements OnInit {
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  vehicles$!: Observable<Vehicle[]>;
  displayedColumns: string[] = ['make', 'model', 'year', 'vin', 'currentMileage', 'isCurrentlyOwned', 'actions'];

  ngOnInit(): void {
    this.vehicles$ = this._vehicleService.vehicles$;
    this._vehicleService.getVehicles().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(VehicleDialog, {
      width: '800px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._vehicleService.createVehicle(result).subscribe({
          next: () => {
            this._snackBar.open('Vehicle added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error adding vehicle', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(vehicle: Vehicle): void {
    const dialogRef = this._dialog.open(VehicleDialog, {
      width: '800px',
      data: { vehicle }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._vehicleService.updateVehicle(vehicle.vehicleId, result).subscribe({
          next: () => {
            this._snackBar.open('Vehicle updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error updating vehicle', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteVehicle(vehicle: Vehicle): void {
    if (confirm(`Are you sure you want to delete ${vehicle.year} ${vehicle.make} ${vehicle.model}?`)) {
      this._vehicleService.deleteVehicle(vehicle.vehicleId).subscribe({
        next: () => {
          this._snackBar.open('Vehicle deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this._snackBar.open('Error deleting vehicle', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
