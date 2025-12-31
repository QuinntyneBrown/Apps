import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ApplianceService } from '../services';

@Component({
  selector: 'app-appliances-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './appliances-list.html',
  styleUrl: './appliances-list.scss',
})
export class AppliancesList {
  private readonly _applianceService = inject(ApplianceService);
  private readonly _router = inject(Router);

  appliances$ = this._applianceService.appliances$;
  displayedColumns = ['name', 'applianceType', 'brand', 'modelNumber', 'purchaseDate', 'actions'];

  constructor() {
    this._applianceService.getAppliances().subscribe();
  }

  viewDetails(id: string): void {
    this._router.navigate(['/appliances', id]);
  }

  deleteAppliance(id: string): void {
    if (confirm('Are you sure you want to delete this appliance?')) {
      this._applianceService.deleteAppliance(id).subscribe();
    }
  }

  addAppliance(): void {
    this._router.navigate(['/appliances/new']);
  }
}
