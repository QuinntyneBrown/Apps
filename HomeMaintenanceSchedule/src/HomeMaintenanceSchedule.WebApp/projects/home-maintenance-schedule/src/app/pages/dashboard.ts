import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MaintenanceTaskService, ContractorService, ServiceLogService } from '../services';
import { TaskStatus } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>task</mat-icon>
            <mat-card-title>Maintenance Tasks</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (tasks$ | async)?.length || 0 }} total tasks</p>
            <p class="dashboard__substat">{{ pendingTasksCount }} pending</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/tasks">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>engineering</mat-icon>
            <mat-card-title>Contractors</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (contractors$ | async)?.length || 0 }} contractors</p>
            <p class="dashboard__substat">{{ activeContractorsCount }} active</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/contractors">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>history</mat-icon>
            <mat-card-title>Service Logs</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__stat">{{ (logs$ | async)?.length || 0 }} service logs</p>
            <p class="dashboard__substat">Maintenance history</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/service-logs">View All</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 1.5rem;
    }
    .dashboard__title {
      margin-bottom: 1.5rem;
    }
    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 1.5rem;
    }
    .dashboard__card {
      mat-icon[mat-card-avatar] {
        font-size: 40px;
        width: 40px;
        height: 40px;
      }
    }
    .dashboard__stat {
      font-size: 1.5rem;
      font-weight: 500;
      margin: 0.5rem 0;
    }
    .dashboard__substat {
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private _taskService = inject(MaintenanceTaskService);
  private _contractorService = inject(ContractorService);
  private _logService = inject(ServiceLogService);

  tasks$ = this._taskService.tasks$;
  contractors$ = this._contractorService.contractors$;
  logs$ = this._logService.logs$;

  pendingTasksCount = 0;
  activeContractorsCount = 0;

  ngOnInit(): void {
    this._taskService.getAll().subscribe(tasks => {
      this.pendingTasksCount = tasks.filter(t => t.status === TaskStatus.Scheduled || t.status === TaskStatus.InProgress).length;
    });
    this._contractorService.getAll().subscribe(contractors => {
      this.activeContractorsCount = contractors.filter(c => c.isActive).length;
    });
    this._logService.getAll().subscribe();
  }
}
