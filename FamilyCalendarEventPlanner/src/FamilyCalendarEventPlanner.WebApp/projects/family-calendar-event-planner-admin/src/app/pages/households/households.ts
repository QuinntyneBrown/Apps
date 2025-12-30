import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BehaviorSubject, switchMap } from 'rxjs';
import { HouseholdsService } from '../../services/households.service';
import { HouseholdDto, CanadianProvince, getProvinceLabel } from '../../models/household-dto';
import { CreateOrEditHouseholdDialog, CreateOrEditHouseholdDialogResult } from '../../components/create-or-edit-household-dialog';
import { ConfirmDialog, ConfirmDialogResult } from '../../components/confirm-dialog';

@Component({
  selector: 'app-households',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
    MatSnackBarModule
  ],
  templateUrl: './households.html',
  styleUrls: ['./households.scss']
})
export class Households {
  private householdsService = inject(HouseholdsService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['name', 'street', 'city', 'province', 'postalCode', 'actions'];

  households$ = this.refresh$.pipe(
    switchMap(() => this.householdsService.getHouseholds())
  );

  getProvinceLabel(province: CanadianProvince): string {
    return getProvinceLabel(province);
  }

  onCreateHousehold(): void {
    const dialogRef = this.dialog.open(CreateOrEditHouseholdDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditHouseholdDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.householdsService.createHousehold(result.data).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Household created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating household:', error);
            this.snackBar.open('Error creating household', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditHousehold(household: HouseholdDto): void {
    const dialogRef = this.dialog.open(CreateOrEditHouseholdDialog, {
      width: '500px',
      data: { household }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditHouseholdDialogResult) => {
      if (result?.action === 'update' && result.data) {
        this.householdsService.updateHousehold(household.householdId, { ...result.data, householdId: household.householdId }).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Household updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating household:', error);
            this.snackBar.open('Error updating household', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteHousehold(household: HouseholdDto): void {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      width: '400px',
      data: {
        title: 'Delete Household',
        message: `Are you sure you want to delete ${household.name}?`
      }
    });

    dialogRef.afterClosed().subscribe((result: ConfirmDialogResult) => {
      if (result?.confirmed) {
        this.householdsService.deleteHousehold(household.householdId).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Household deleted successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error deleting household:', error);
            this.snackBar.open('Error deleting household', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }
}
