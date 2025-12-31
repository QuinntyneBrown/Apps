import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { CoursesService } from '../services';

@Component({
  selector: 'app-courses-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  template: `
    <div class="courses-list">
      <div class="courses-list__header">
        <h1>Courses</h1>
        <a mat-raised-button color="primary" routerLink="/courses/new">
          <mat-icon>add</mat-icon>
          Add Course
        </a>
      </div>

      <mat-card class="courses-list__card">
        <mat-card-content>
          <div *ngIf="(courses$ | async) as courses">
            <p *ngIf="courses.length === 0" class="courses-list__empty">No courses found. Add your first course!</p>

            <table mat-table [dataSource]="courses" *ngIf="courses.length > 0" class="courses-list__table">
              <ng-container matColumnDef="name">
                <th mat-header-cell *matHeaderCellDef>Name</th>
                <td mat-cell *matCellDef="let course">{{ course.name }}</td>
              </ng-container>

              <ng-container matColumnDef="location">
                <th mat-header-cell *matHeaderCellDef>Location</th>
                <td mat-cell *matCellDef="let course">{{ course.location || 'N/A' }}</td>
              </ng-container>

              <ng-container matColumnDef="holes">
                <th mat-header-cell *matHeaderCellDef>Holes</th>
                <td mat-cell *matCellDef="let course">{{ course.numberOfHoles }}</td>
              </ng-container>

              <ng-container matColumnDef="par">
                <th mat-header-cell *matHeaderCellDef>Par</th>
                <td mat-cell *matCellDef="let course">{{ course.totalPar }}</td>
              </ng-container>

              <ng-container matColumnDef="rating">
                <th mat-header-cell *matHeaderCellDef>Rating / Slope</th>
                <td mat-cell *matCellDef="let course">
                  {{ course.courseRating || 'N/A' }} / {{ course.slopeRating || 'N/A' }}
                </td>
              </ng-container>

              <ng-container matColumnDef="actions">
                <th mat-header-cell *matHeaderCellDef>Actions</th>
                <td mat-cell *matCellDef="let course">
                  <a mat-button color="primary" [routerLink]="['/courses', course.courseId]">View</a>
                  <a mat-button color="accent" [routerLink]="['/courses', course.courseId, 'edit']">Edit</a>
                  <button mat-button color="warn" (click)="deleteCourse(course.courseId)">Delete</button>
                </td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
            </table>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .courses-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .courses-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .courses-list__header h1 {
      margin: 0;
    }

    .courses-list__card {
      margin-top: 1rem;
    }

    .courses-list__empty {
      text-align: center;
      padding: 2rem;
      color: #666;
    }

    .courses-list__table {
      width: 100%;
    }
  `]
})
export class CoursesList implements OnInit {
  courses$ = this.coursesService.courses$;
  displayedColumns: string[] = ['name', 'location', 'holes', 'par', 'rating', 'actions'];

  constructor(private coursesService: CoursesService) {}

  ngOnInit(): void {
    this.coursesService.getCourses().subscribe();
  }

  deleteCourse(id: string): void {
    if (confirm('Are you sure you want to delete this course?')) {
      this.coursesService.deleteCourse(id).subscribe();
    }
  }
}
