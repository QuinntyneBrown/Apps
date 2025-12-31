import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { InsuranceInfoService, VehicleService } from '../../services';
import { InsuranceInfoFormDialog } from './insurance-info-form-dialog';

@Component({
  selector: 'app-insurance-infos',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './insurance-infos.html',
  styleUrl: './insurance-infos.scss'
})
export class InsuranceInfos implements OnInit {
  private _insuranceInfoService = inject(InsuranceInfoService);
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);

  insuranceInfos$ = this._insuranceInfoService.insuranceInfos$;
  vehicles$ = this._vehicleService.vehicles$;
  displayedColumns = ['insuranceCompany', 'policyNumber', 'policyHolder', 'policyEndDate', 'includesRoadsideAssistance', 'actions'];

  ngOnInit(): void {
    this._insuranceInfoService.getInsuranceInfos().subscribe();
    this._vehicleService.getVehicles().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(InsuranceInfoFormDialog, {
      width: '600px',
      data: { insuranceInfo: null, vehicles: [] }
    });

    this._vehicleService.vehicles$.subscribe(vehicles => {
      dialogRef.componentInstance.vehicles = vehicles;
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._insuranceInfoService.createInsuranceInfo(result).subscribe();
      }
    });
  }

  openEditDialog(insuranceInfo: any): void {
    const dialogRef = this._dialog.open(InsuranceInfoFormDialog, {
      width: '600px',
      data: { insuranceInfo, vehicles: [] }
    });

    this._vehicleService.vehicles$.subscribe(vehicles => {
      dialogRef.componentInstance.vehicles = vehicles;
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._insuranceInfoService.updateInsuranceInfo(insuranceInfo.insuranceInfoId, result).subscribe();
      }
    });
  }

  deleteInsuranceInfo(id: string): void {
    if (confirm('Are you sure you want to delete this insurance info?')) {
      this._insuranceInfoService.deleteInsuranceInfo(id).subscribe();
    }
  }
}
