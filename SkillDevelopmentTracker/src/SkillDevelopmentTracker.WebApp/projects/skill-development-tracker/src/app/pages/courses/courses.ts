import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { CourseService } from '../../services';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatProgressBarModule, MatChipsModule],
  templateUrl: './courses.html',
  styleUrl: './courses.scss'
})
export class Courses implements OnInit {
  private _courseService = inject(CourseService);

  courses$ = this._courseService.courses$;
  displayedColumns: string[] = ['title', 'provider', 'progress', 'status', 'actualHours', 'actions'];

  ngOnInit(): void {
    this._courseService.getCourses().subscribe();
  }

  deleteCourse(id: string): void {
    if (confirm('Are you sure you want to delete this course?')) {
      this._courseService.deleteCourse(id).subscribe();
    }
  }
}
