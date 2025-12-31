import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ReturnWindowService } from '../../services';

@Component({
  selector: 'app-return-windows',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './return-windows.html',
  styleUrl: './return-windows.scss'
})
export class ReturnWindows {
  private _returnWindowService = inject(ReturnWindowService);

  returnWindows$ = this._returnWindowService.returnWindows$;
  displayedColumns: string[] = ['startDate', 'endDate', 'durationDays', 'status', 'restockingFee', 'actions'];

  ngOnInit(): void {
    this._returnWindowService.getAll().subscribe();
  }

  deleteReturnWindow(id: string): void {
    if (confirm('Are you sure you want to delete this return window?')) {
      this._returnWindowService.delete(id).subscribe();
    }
  }
}
