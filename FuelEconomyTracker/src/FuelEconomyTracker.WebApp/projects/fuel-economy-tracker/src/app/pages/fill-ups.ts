import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { FillUpsService } from '../services';
import { FillUpDialog } from './fill-up-dialog';

@Component({
  selector: 'app-fill-ups',
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
  templateUrl: './fill-ups.html',
  styleUrl: './fill-ups.scss'
})
export class FillUps implements OnInit {
  private fillUpsService = inject(FillUpsService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  fillUps$ = this.fillUpsService.fillUps$;
  displayedColumns = ['fillUpDate', 'odometer', 'gallons', 'pricePerGallon', 'totalCost', 'milesPerGallon', 'gasStation', 'actions'];

  ngOnInit(): void {
    this.loadFillUps();
  }

  loadFillUps(): void {
    this.fillUpsService.getAll().subscribe();
  }

  openDialog(fillUp?: any): void {
    const dialogRef = this.dialog.open(FillUpDialog, {
      width: '600px',
      data: fillUp || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadFillUps();
      }
    });
  }

  deleteFillUp(id: string): void {
    if (confirm('Are you sure you want to delete this fill-up?')) {
      this.fillUpsService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Fill-up deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting fill-up', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
