import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { switchMap } from 'rxjs';
import { ApplianceService } from '../services';

@Component({
  selector: 'app-appliance-detail',
  standalone: true,
  imports: [CommonModule, MatTabsModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './appliance-detail.html',
  styleUrl: './appliance-detail.scss',
})
export class ApplianceDetail {
  private readonly _route = inject(ActivatedRoute);
  private readonly _router = inject(Router);
  private readonly _applianceService = inject(ApplianceService);

  appliance$ = this._route.params.pipe(
    switchMap(params => this._applianceService.getApplianceById(params['id']))
  );

  goBack(): void {
    this._router.navigate(['/appliances']);
  }

  editAppliance(id: string): void {
    this._router.navigate(['/appliances', id, 'edit']);
  }
}
