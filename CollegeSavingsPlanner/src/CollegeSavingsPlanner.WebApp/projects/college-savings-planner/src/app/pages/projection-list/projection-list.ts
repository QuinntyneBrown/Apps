import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Observable } from 'rxjs';
import { Projection } from '../../models';
import { ProjectionService } from '../../services';

@Component({
  selector: 'app-projection-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './projection-list.html',
  styleUrl: './projection-list.scss'
})
export class ProjectionList implements OnInit {
  projections$: Observable<Projection[]>;

  constructor(private projectionService: ProjectionService) {
    this.projections$ = this.projectionService.projections$;
  }

  ngOnInit(): void {
    this.projectionService.getProjections().subscribe();
  }

  deleteProjection(id: string): void {
    if (confirm('Are you sure you want to delete this projection?')) {
      this.projectionService.deleteProjection(id).subscribe();
    }
  }

  getProgressPercentage(projection: Projection): number {
    if (projection.targetGoal === 0) return 0;
    return Math.min((projection.projectedBalance / projection.targetGoal) * 100, 100);
  }
}
