import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Observable } from 'rxjs';
import { Projection } from '../../models';
import { ProjectionService } from '../../services';

@Component({
  selector: 'app-projection-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './projection-detail.html',
  styleUrl: './projection-detail.scss'
})
export class ProjectionDetail implements OnInit {
  projection$: Observable<Projection | null>;
  projectionId: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectionService: ProjectionService
  ) {
    this.projection$ = this.projectionService.selectedProjection$;
  }

  ngOnInit(): void {
    this.projectionId = this.route.snapshot.paramMap.get('id') || '';
    if (this.projectionId) {
      this.projectionService.getProjectionById(this.projectionId).subscribe();
    }
  }

  deleteProjection(): void {
    if (confirm('Are you sure you want to delete this projection?')) {
      this.projectionService.deleteProjection(this.projectionId).subscribe(() => {
        this.router.navigate(['/projections']);
      });
    }
  }

  getProgressPercentage(projection: Projection): number {
    if (projection.targetGoal === 0) return 0;
    return Math.min((projection.projectedBalance / projection.targetGoal) * 100, 100);
  }
}
