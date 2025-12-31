import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { VehicleService } from '../../services';
import { VehicleFormDialog } from './vehicle-form-dialog';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.scss'
})
export class Vehicles implements OnInit {
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);

  vehicles$ = this._vehicleService.vehicles$;
  displayedColumns = ['make', 'model', 'year', 'licensePlate', 'isActive', 'actions'];

  ngOnInit(): void {
    this._vehicleService.getVehicles().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(VehicleFormDialog, {
      width: '600px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._vehicleService.createVehicle(result).subscribe();
      }
    });
  }

  openEditDialog(vehicle: any): void {
    const dialogRef = this._dialog.open(VehicleFormDialog, {
      width: '600px',
      data: vehicle
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._vehicleService.updateVehicle(vehicle.vehicleId, result).subscribe();
      }
    });
  }

  deleteVehicle(id: string): void {
    if (confirm('Are you sure you want to delete this vehicle?')) {
      this._vehicleService.deleteVehicle(id).subscribe();
    }
  }
}
