import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AlertsService } from '../../services';
import { AlertTypeLabels } from '../../models';

@Component({
  selector: 'app-alerts-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatSlideToggleModule],
  templateUrl: './alerts-list.component.html',
  styleUrl: './alerts-list.component.scss'
})
export class AlertsList implements OnInit {
  private readonly alertsService = inject(AlertsService);
  alerts$ = this.alertsService.alerts$;
  displayedColumns = ['name', 'alertType', 'isActive', 'lastTriggered', 'actions'];
  AlertTypeLabels = AlertTypeLabels;

  ngOnInit(): void { this.alertsService.loadAlerts().subscribe(); }
  onToggle(id: string, isActive: boolean): void { this.alertsService.toggleAlert(id, isActive).subscribe(); }
  onDelete(id: string): void { if (confirm('Delete this alert?')) this.alertsService.deleteAlert(id).subscribe(); }
}
