import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ProjectService, BudgetService, MaterialService, ContractorService } from '../services';
import { ProjectStatus } from '../models';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatTableModule,
    MatChipsModule
  ],
  templateUrl: './project-detail.html',
  styleUrl: './project-detail.scss'
})
export class ProjectDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private projectService = inject(ProjectService);
  private budgetService = inject(BudgetService);
  private materialService = inject(MaterialService);
  private contractorService = inject(ContractorService);

  project$ = this.projectService.currentProject$;
  budgets$ = this.budgetService.budgets$;
  materials$ = this.materialService.materials$;
  contractors$ = this.contractorService.contractors$;

  ProjectStatus = ProjectStatus;

  budgetColumns = ['category', 'allocatedAmount', 'spentAmount', 'remaining'];
  materialColumns = ['name', 'quantity', 'unit', 'unitCost', 'totalCost', 'supplier'];
  contractorColumns = ['name', 'trade', 'phoneNumber', 'email', 'rating'];

  ngOnInit() {
    const projectId = this.route.snapshot.paramMap.get('id');
    if (projectId) {
      this.projectService.getById(projectId).subscribe();
      this.budgetService.getByProjectId(projectId).subscribe();
      this.materialService.getByProjectId(projectId).subscribe();
      this.contractorService.getByProjectId(projectId).subscribe();
    }
  }

  goBack() {
    this.router.navigate(['/projects']);
  }

  getStatusLabel(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning: return 'Planning';
      case ProjectStatus.InProgress: return 'In Progress';
      case ProjectStatus.Completed: return 'Completed';
      case ProjectStatus.OnHold: return 'On Hold';
      case ProjectStatus.Cancelled: return 'Cancelled';
      default: return 'Unknown';
    }
  }

  getStatusColor(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning: return 'accent';
      case ProjectStatus.InProgress: return 'primary';
      case ProjectStatus.Completed: return 'primary';
      case ProjectStatus.OnHold: return 'warn';
      case ProjectStatus.Cancelled: return '';
      default: return '';
    }
  }

  calculateRemaining(allocated: number, spent?: number): number {
    return allocated - (spent || 0);
  }
}
