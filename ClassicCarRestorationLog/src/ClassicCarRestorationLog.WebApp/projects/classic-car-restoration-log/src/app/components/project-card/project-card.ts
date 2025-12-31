import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Project, ProjectPhase } from '../../models';

@Component({
  selector: 'app-project-card',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './project-card.html',
  styleUrl: './project-card.scss'
})
export class ProjectCard {
  @Input() project!: Project;
  @Output() edit = new EventEmitter<Project>();
  @Output() delete = new EventEmitter<Project>();

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

  onEdit(event: Event): void {
    event.stopPropagation();
    this.edit.emit(this.project);
  }

  onDelete(event: Event): void {
    event.stopPropagation();
    this.delete.emit(this.project);
  }
}
