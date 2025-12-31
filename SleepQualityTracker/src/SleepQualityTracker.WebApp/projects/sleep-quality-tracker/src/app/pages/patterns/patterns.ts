import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Observable } from 'rxjs';
import { PatternService } from '../../services';
import { Pattern } from '../../models';
import { PatternCard } from '../../components';

@Component({
  selector: 'app-patterns',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    PatternCard
  ],
  templateUrl: './patterns.html',
  styleUrl: './patterns.scss'
})
export class Patterns implements OnInit {
  private patternService = inject(PatternService);

  patterns$!: Observable<Pattern[]>;

  ngOnInit(): void {
    this.loadPatterns();
  }

  loadPatterns(): void {
    this.patterns$ = this.patternService.getPatterns();
  }

  onEdit(pattern: Pattern): void {
    console.log('Edit pattern:', pattern);
    // TODO: Implement edit dialog
  }

  onDelete(patternId: string): void {
    if (confirm('Are you sure you want to delete this pattern?')) {
      this.patternService.deletePattern(patternId).subscribe({
        next: () => console.log('Pattern deleted successfully'),
        error: (error) => console.error('Error deleting pattern:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new pattern');
    // TODO: Implement create dialog
  }
}
