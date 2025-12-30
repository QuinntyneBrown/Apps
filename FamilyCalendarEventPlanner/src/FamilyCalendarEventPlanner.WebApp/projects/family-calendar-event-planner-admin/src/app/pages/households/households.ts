import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { BehaviorSubject, switchMap } from 'rxjs';
import { HouseholdsService } from '../../services/households.service';
import { HouseholdDto, getProvinceLabel } from '../../models/household-dto';
import { CreateOrEditHouseholdDialog, CreateOrEditHouseholdDialogResult } from '../../components/create-or-edit-household-dialog';

@Component({
  selector: 'app-households',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule
  ],
  templateUrl: './households.html',
  styleUrls: ['./households.scss']
})
export class Households {
  private householdsService = inject(HouseholdsService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['name', 'street', 'city', 'province', 'postalCode', 'actions'];

  households$ = this.refresh$.pipe(
    switchMap(() => this.householdsService.getHouseholds())
  );

  getProvinceLabel(province: string): string {
    return getProvinceLabel(province as any);
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
          },
          error: (error) => {
            console.error('Error creating household:', error);
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
          },
          error: (error) => {
            console.error('Error updating household:', error);
          }
        });
      }
    });
  }

  onDeleteHousehold(household: HouseholdDto): void {
    if (confirm(`Are you sure you want to delete ${household.name}?`)) {
      this.householdsService.deleteHousehold(household.householdId).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (error) => {
          console.error('Error deleting household:', error);
        }
      });
    }
  }
}
