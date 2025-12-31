import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CoursesService } from '../services';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="course-detail">
      <div class="course-detail__header">
        <button mat-button routerLink="/courses">
          <mat-icon>arrow_back</mat-icon>
          Back to Courses
        </button>
      </div>

      <mat-card *ngIf="(course$ | async) as course" class="course-detail__card">
        <mat-card-header>
          <mat-card-title>{{ course.name }}</mat-card-title>
          <mat-card-subtitle *ngIf="course.location">{{ course.location }}</mat-card-subtitle>
        </mat-card-header>

        <mat-card-content>
          <div class="course-detail__info">
            <div class="course-detail__info-item">
              <strong>Number of Holes:</strong> {{ course.numberOfHoles }}
            </div>
            <div class="course-detail__info-item">
              <strong>Total Par:</strong> {{ course.totalPar }}
            </div>
            <div class="course-detail__info-item" *ngIf="course.courseRating">
              <strong>Course Rating:</strong> {{ course.courseRating }}
            </div>
            <div class="course-detail__info-item" *ngIf="course.slopeRating">
              <strong>Slope Rating:</strong> {{ course.slopeRating }}
            </div>
            <div class="course-detail__info-item" *ngIf="course.notes">
              <strong>Notes:</strong> {{ course.notes }}
            </div>
            <div class="course-detail__info-item">
              <strong>Created:</strong> {{ course.createdAt | date:'medium' }}
            </div>
          </div>
        </mat-card-content>

        <mat-card-actions>
          <a mat-raised-button color="primary" [routerLink]="['/courses', course.courseId, 'edit']">
            <mat-icon>edit</mat-icon>
            Edit
          </a>
          <button mat-raised-button color="warn" (click)="deleteCourse(course.courseId)">
            <mat-icon>delete</mat-icon>
            Delete
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .course-detail {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .course-detail__header {
      margin-bottom: 1rem;
    }

    .course-detail__card {
      margin-top: 1rem;
    }

    .course-detail__info {
      display: grid;
      gap: 1rem;
      margin-top: 1rem;
    }

    .course-detail__info-item {
      padding: 0.5rem 0;
      border-bottom: 1px solid #eee;
    }

    .course-detail__info-item:last-child {
      border-bottom: none;
    }
  `]
})
export class CourseDetail implements OnInit {
  course$ = this.coursesService.currentCourse$;
  private courseId: string = '';

  constructor(
    private coursesService: CoursesService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.courseId = this.route.snapshot.paramMap.get('id')!;
    this.coursesService.getCourseById(this.courseId).subscribe();
  }

  deleteCourse(id: string): void {
    if (confirm('Are you sure you want to delete this course?')) {
      this.coursesService.deleteCourse(id).subscribe(() => {
        this.router.navigate(['/courses']);
      });
    }
  }
}
