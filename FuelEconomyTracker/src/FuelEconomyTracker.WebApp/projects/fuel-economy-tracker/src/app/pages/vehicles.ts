import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { VehiclesService } from '../services';
import { VehicleDialog } from './vehicle-dialog';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './vehicles.html',
  styleUrl: './vehicles.scss'
})
export class Vehicles implements OnInit {
  private vehiclesService = inject(VehiclesService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  vehicles$ = this.vehiclesService.vehicles$;
  displayedColumns = ['make', 'model', 'year', 'licensePlate', 'tankCapacity', 'isActive', 'actions'];

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {
    this.vehiclesService.getAll().subscribe();
  }

  openDialog(vehicle?: any): void {
    const dialogRef = this.dialog.open(VehicleDialog, {
      width: '600px',
      data: vehicle || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadVehicles();
      }
    });
  }

  deleteVehicle(id: string): void {
    if (confirm('Are you sure you want to delete this vehicle?')) {
      this.vehiclesService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Vehicle deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting vehicle', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
