import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { LearningPathService } from '../../services';

@Component({
  selector: 'app-learning-paths',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatProgressBarModule, MatChipsModule],
  templateUrl: './learning-paths.html',
  styleUrl: './learning-paths.scss'
})
export class LearningPaths implements OnInit {
  private _learningPathService = inject(LearningPathService);

  learningPaths$ = this._learningPathService.learningPaths$;
  displayedColumns: string[] = ['title', 'description', 'progress', 'status', 'targetDate', 'actions'];

  ngOnInit(): void {
    this._learningPathService.getLearningPaths().subscribe();
  }

  deleteLearningPath(id: string): void {
    if (confirm('Are you sure you want to delete this learning path?')) {
      this._learningPathService.deleteLearningPath(id).subscribe();
    }
  }

  formatDate(date?: string): string {
    if (!date) return '-';
    return new Date(date).toLocaleDateString();
  }
}
