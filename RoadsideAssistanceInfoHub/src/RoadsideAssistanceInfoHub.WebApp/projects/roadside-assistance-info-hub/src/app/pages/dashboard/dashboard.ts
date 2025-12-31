import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { VehicleService, EmergencyContactService, InsuranceInfoService, PolicyService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _vehicleService = inject(VehicleService);
  private _emergencyContactService = inject(EmergencyContactService);
  private _insuranceInfoService = inject(InsuranceInfoService);
  private _policyService = inject(PolicyService);

  stats$ = combineLatest([
    this._vehicleService.vehicles$,
    this._emergencyContactService.emergencyContacts$,
    this._insuranceInfoService.insuranceInfos$,
    this._policyService.policies$
  ]).pipe(
    map(([vehicles, contacts, insurances, policies]) => ({
      vehiclesCount: vehicles.length,
      contactsCount: contacts.length,
      insurancesCount: insurances.length,
      policiesCount: policies.length,
      activeVehicles: vehicles.filter(v => v.isActive).length,
      activeContacts: contacts.filter(c => c.isActive).length
    }))
  );

  ngOnInit(): void {
    this._vehicleService.getVehicles().subscribe();
    this._emergencyContactService.getEmergencyContacts().subscribe();
    this._insuranceInfoService.getInsuranceInfos().subscribe();
    this._policyService.getPolicies().subscribe();
  }
}
