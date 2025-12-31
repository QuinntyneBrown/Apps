import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SkillService } from '../services';

@Component({
  selector: 'app-skills-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTooltipModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="skills-list">
      <div class="skills-list__header">
        <div>
          <h1 class="skills-list__title">Skills</h1>
          <p class="skills-list__subtitle">Track your technical and professional skills</p>
        </div>
        <a mat-raised-button color="primary" routerLink="/skills/new" class="skills-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Skill
        </a>
      </div>

      <div class="skills-list__content">
        @if (loading$ | async) {
          <div class="skills-list__loading">
            <mat-spinner></mat-spinner>
          </div>
        } @else if ((skills$ | async)?.length === 0) {
          <div class="skills-list__empty">
            <mat-icon class="skills-list__empty-icon">psychology</mat-icon>
            <h2>No skills yet</h2>
            <p>Start building your skills inventory</p>
            <a mat-raised-button color="primary" routerLink="/skills/new">
              Add Your First Skill
            </a>
          </div>
        } @else {
          <table mat-table [dataSource]="skills$ | async" class="skills-list__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let skill">
                <div class="skills-list__cell-title">
                  {{ skill.name }}
                  @if (skill.isFeatured) {
                    <mat-icon class="skills-list__featured-icon" matTooltip="Featured">star</mat-icon>
                  }
                </div>
              </td>
            </ng-container>

            <ng-container matColumnDef="category">
              <th mat-header-cell *matHeaderCellDef>Category</th>
              <td mat-cell *matCellDef="let skill">
                <mat-chip class="skills-list__chip">{{ skill.category }}</mat-chip>
              </td>
            </ng-container>

            <ng-container matColumnDef="proficiencyLevel">
              <th mat-header-cell *matHeaderCellDef>Proficiency</th>
              <td mat-cell *matCellDef="let skill">
                {{ skill.proficiencyLevel }}
              </td>
            </ng-container>

            <ng-container matColumnDef="yearsOfExperience">
              <th mat-header-cell *matHeaderCellDef>Experience</th>
              <td mat-cell *matCellDef="let skill">
                {{ skill.yearsOfExperience ? skill.yearsOfExperience + ' years' : '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="lastUsedDate">
              <th mat-header-cell *matHeaderCellDef>Last Used</th>
              <td mat-cell *matCellDef="let skill">
                {{ skill.lastUsedDate ? (skill.lastUsedDate | date:'MMM y') : '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let skill">
                <div class="skills-list__actions">
                  <button
                    mat-icon-button
                    [matTooltip]="skill.isFeatured ? 'Unfeature' : 'Feature'"
                    (click)="toggleFeatured(skill.skillId)">
                    <mat-icon [class.skills-list__action-featured]="skill.isFeatured">
                      {{ skill.isFeatured ? 'star' : 'star_border' }}
                    </mat-icon>
                  </button>
                  <a
                    mat-icon-button
                    [routerLink]="['/skills', skill.skillId]"
                    matTooltip="Edit">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button
                    mat-icon-button
                    color="warn"
                    matTooltip="Delete"
                    (click)="deleteSkill(skill.skillId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        }
      </div>
    </div>
  `,
  styles: [`
    .skills-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .skills-list__header {
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
      margin-bottom: 2rem;
      gap: 1rem;
    }

    .skills-list__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .skills-list__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .skills-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .skills-list__content {
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .skills-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 4rem;
    }

    .skills-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 4rem 2rem;
      text-align: center;
    }

    .skills-list__empty-icon {
      width: 80px;
      height: 80px;
      font-size: 80px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 1rem;
    }

    .skills-list__table {
      width: 100%;
    }

    .skills-list__cell-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .skills-list__featured-icon {
      width: 20px;
      height: 20px;
      font-size: 20px;
      color: #ffd700;
    }

    .skills-list__chip {
      font-size: 0.75rem;
    }

    .skills-list__actions {
      display: flex;
      gap: 0.25rem;
    }

    .skills-list__action-featured {
      color: #ffd700;
    }

    @media (max-width: 768px) {
      .skills-list {
        padding: 1rem;
      }

      .skills-list__header {
        flex-direction: column;
      }

      .skills-list__add-btn {
        width: 100%;
      }
    }
  `]
})
export class SkillsList implements OnInit {
  private skillService = inject(SkillService);

  skills$ = this.skillService.skills$;
  loading$ = this.skillService.loading$;

  displayedColumns = ['name', 'category', 'proficiencyLevel', 'yearsOfExperience', 'lastUsedDate', 'actions'];

  ngOnInit(): void {
    this.skillService.getSkills().subscribe();
  }

  toggleFeatured(id: string): void {
    this.skillService.toggleFeatured(id).subscribe();
  }

  deleteSkill(id: string): void {
    if (confirm('Are you sure you want to delete this skill?')) {
      this.skillService.deleteSkill(id).subscribe();
    }
  }
}
