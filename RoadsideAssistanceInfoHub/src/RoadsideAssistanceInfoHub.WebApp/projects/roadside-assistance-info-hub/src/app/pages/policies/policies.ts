import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PolicyService, VehicleService } from '../../services';
import { PolicyFormDialog } from './policy-form-dialog';

@Component({
  selector: 'app-policies',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './policies.html',
  styleUrl: './policies.scss'
})
export class Policies implements OnInit {
  private _policyService = inject(PolicyService);
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);

  policies$ = this._policyService.policies$;
  vehicles$ = this._vehicleService.vehicles$;
  displayedColumns = ['provider', 'policyNumber', 'emergencyPhone', 'endDate', 'coverageCount', 'actions'];

  ngOnInit(): void {
    this._policyService.getPolicies().subscribe();
    this._vehicleService.getVehicles().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(PolicyFormDialog, {
      width: '600px',
      data: { policy: null, vehicles: [] }
    });

    this._vehicleService.vehicles$.subscribe(vehicles => {
      dialogRef.componentInstance.vehicles = vehicles;
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._policyService.createPolicy(result).subscribe();
      }
    });
  }

  openEditDialog(policy: any): void {
    const dialogRef = this._dialog.open(PolicyFormDialog, {
      width: '600px',
      data: { policy, vehicles: [] }
    });

    this._vehicleService.vehicles$.subscribe(vehicles => {
      dialogRef.componentInstance.vehicles = vehicles;
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._policyService.updatePolicy(policy.policyId, result).subscribe();
      }
    });
  }

  deletePolicy(id: string): void {
    if (confirm('Are you sure you want to delete this policy?')) {
      this._policyService.deletePolicy(id).subscribe();
    }
  }

  getCoverageCount(policy: any): number {
    let count = 0;
    if (policy.coversBatteryService) count++;
    if (policy.coversFlatTire) count++;
    if (policy.coversFuelDelivery) count++;
    if (policy.coversLockout) count++;
    return count;
  }
}
