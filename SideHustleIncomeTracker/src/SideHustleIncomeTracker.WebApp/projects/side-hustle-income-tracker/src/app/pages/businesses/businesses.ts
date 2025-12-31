import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BusinessService } from '../../services';
import { BusinessFormDialog } from '../../components/business-form-dialog/business-form-dialog';
import { Business } from '../../models';

@Component({
  selector: 'app-businesses',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './businesses.html',
  styleUrl: './businesses.scss'
})
export class Businesses implements OnInit {
  private _businessService = inject(BusinessService);
  private _dialog = inject(MatDialog);

  businesses$ = this._businessService.businesses$;
  displayedColumns: string[] = ['name', 'description', 'startDate', 'isActive', 'totalIncome', 'totalExpenses', 'netProfit', 'actions'];

  ngOnInit(): void {
    this._businessService.getAll().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(BusinessFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  openEditDialog(business: Business): void {
    const dialogRef = this._dialog.open(BusinessFormDialog, {
      width: '600px',
      data: { business }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  deleteBusiness(business: Business): void {
    if (confirm(`Are you sure you want to delete ${business.name}?`)) {
      this._businessService.delete(business.businessId).subscribe();
    }
  }
}
