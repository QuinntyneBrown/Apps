import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTableModule } from '@angular/material/table';
import { Observable } from 'rxjs';
import { ProjectService, PartService, WorkLogService, PhotoLogService } from '../../services';
import { Project, Part, WorkLog, PhotoLog, ProjectPhase } from '../../models';

@Component({
  selector: 'app-project-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTabsModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatTableModule
  ],
  templateUrl: './project-detail.html',
  styleUrl: './project-detail.scss'
})
export class ProjectDetail implements OnInit {
  project$: Observable<Project | null>;
  parts$: Observable<Part[]>;
  workLogs$: Observable<WorkLog[]>;
  photoLogs$: Observable<PhotoLog[]>;

  partsColumns = ['name', 'partNumber', 'supplier', 'cost', 'status'];
  workLogsColumns = ['workDate', 'hoursWorked', 'description'];
  photoLogsColumns = ['photoDate', 'description', 'phase'];

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private partService: PartService,
    private workLogService: WorkLogService,
    private photoLogService: PhotoLogService
  ) {
    this.project$ = this.projectService.currentProject$;
    this.parts$ = this.partService.parts$;
    this.workLogs$ = this.workLogService.workLogs$;
    this.photoLogs$ = this.photoLogService.photoLogs$;
  }

  ngOnInit(): void {
    const projectId = this.route.snapshot.paramMap.get('id');
    if (projectId) {
      this.loadProjectData(projectId);
    }
  }

  loadProjectData(projectId: string): void {
    this.projectService.getProjectById(projectId).subscribe();
    this.partService.getParts(projectId).subscribe();
    this.workLogService.getWorkLogs(projectId).subscribe();
    this.photoLogService.getPhotoLogs(projectId).subscribe();
  }

  getPhaseLabel(phase: ProjectPhase): string {
    return ProjectPhase[phase];
  }

  getPhaseColor(phase: ProjectPhase): string {
    const colors: Record<number, string> = {
      [ProjectPhase.Planning]: 'accent',
      [ProjectPhase.Disassembly]: 'warn',
      [ProjectPhase.Cleaning]: 'primary',
      [ProjectPhase.Repair]: 'warn',
      [ProjectPhase.Painting]: 'accent',
      [ProjectPhase.Reassembly]: 'primary',
      [ProjectPhase.Testing]: 'accent',
      [ProjectPhase.Completed]: 'primary'
    };
    return colors[phase] || 'primary';
  }

  getTotalPartsCost(parts: Part[]): number {
    return parts.reduce((sum, part) => sum + (part.cost || 0), 0);
  }

  getTotalHoursWorked(workLogs: WorkLog[]): number {
    return workLogs.reduce((sum, log) => sum + log.hoursWorked, 0);
  }
}
