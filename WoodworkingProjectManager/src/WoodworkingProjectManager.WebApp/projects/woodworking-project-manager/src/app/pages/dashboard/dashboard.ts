import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { ProjectService, MaterialService, ToolService } from '../../services';
import { map } from 'rxjs/operators';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private _projectService = inject(ProjectService);
  private _materialService = inject(MaterialService);
  private _toolService = inject(ToolService);

  stats$ = combineLatest([
    this._projectService.projects$,
    this._materialService.materials$,
    this._toolService.tools$
  ]).pipe(
    map(([projects, materials, tools]) => ({
      totalProjects: projects.length,
      activeProjects: projects.filter(p => p.status === 'InProgress').length,
      completedProjects: projects.filter(p => p.status === 'Completed').length,
      totalMaterials: materials.length,
      totalTools: tools.length
    }))
  );

  ngOnInit(): void {
    this._projectService.getProjects().subscribe();
    this._materialService.getMaterials().subscribe();
    this._toolService.getTools().subscribe();
  }
}
