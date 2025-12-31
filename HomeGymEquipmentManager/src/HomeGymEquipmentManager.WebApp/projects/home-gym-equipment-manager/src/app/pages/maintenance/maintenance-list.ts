import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MaintenanceService } from '../../services';
import { Maintenance } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-maintenance-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './maintenance-list.html',
  styleUrl: './maintenance-list.scss'
})
export class MaintenanceList implements OnInit {
  maintenanceList$: Observable<Maintenance[]>;
  displayedColumns: string[] = ['maintenanceDate', 'description', 'cost', 'nextMaintenanceDate', 'actions'];

  constructor(
    private maintenanceService: MaintenanceService,
    private router: Router
  ) {
    this.maintenanceList$ = this.maintenanceService.maintenanceList$;
  }

  ngOnInit(): void {
    this.maintenanceService.getAll().subscribe();
  }

  formatDate(date: string | undefined): string {
    if (!date) return '-';
    return new Date(date).toLocaleDateString();
  }

  onCreate(): void {
    this.router.navigate(['/maintenance/create']);
  }

  onEdit(id: string): void {
    this.router.navigate(['/maintenance/edit', id]);
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this maintenance record?')) {
      this.maintenanceService.delete(id).subscribe();
    }
  }
}
