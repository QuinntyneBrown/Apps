import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { WarrantyService } from '../../services';

@Component({
  selector: 'app-warranties',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './warranties.html',
  styleUrl: './warranties.scss'
})
export class Warranties {
  private _warrantyService = inject(WarrantyService);

  warranties$ = this._warrantyService.warranties$;
  displayedColumns: string[] = ['warrantyType', 'provider', 'startDate', 'endDate', 'durationMonths', 'status', 'actions'];

  ngOnInit(): void {
    this._warrantyService.getAll().subscribe();
  }

  deleteWarranty(id: string): void {
    if (confirm('Are you sure you want to delete this warranty?')) {
      this._warrantyService.delete(id).subscribe();
    }
  }
}
