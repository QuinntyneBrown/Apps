import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { StoryService, PersonService } from '../services';
import { Story, Person } from '../models';

@Component({
  selector: 'app-stories',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],
  template: `
    <div class="stories">
      <mat-card class="stories__form-card">
        <mat-card-header>
          <mat-card-title>{{ isEditing ? 'Edit Story' : 'Add New Story' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="storyForm" (ngSubmit)="saveStory()" class="stories__form">
            <mat-form-field class="stories__form-field">
              <mat-label>Person</mat-label>
              <mat-select formControlName="personId" required>
                <mat-option *ngFor="let person of persons$ | async" [value]="person.personId">
                  {{ person.firstName }} {{ person.lastName }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="stories__form-field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" required>
            </mat-form-field>

            <mat-form-field class="stories__form-field stories__form-field--full">
              <mat-label>Content</mat-label>
              <textarea matInput formControlName="content" rows="6"></textarea>
            </mat-form-field>

            <div class="stories__form-actions">
              <button mat-raised-button color="primary" type="submit" [disabled]="!storyForm.valid">
                {{ isEditing ? 'Update' : 'Create' }}
              </button>
              <button mat-button type="button" (click)="cancelEdit()" *ngIf="isEditing">
                Cancel
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="stories__table-card">
        <mat-card-header>
          <mat-card-title>Family Stories</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="stories$ | async" class="stories__table">
            <ng-container matColumnDef="person">
              <th mat-header-cell *matHeaderCellDef>Person</th>
              <td mat-cell *matCellDef="let story">{{ getPersonName(story.personId) }}</td>
            </ng-container>

            <ng-container matColumnDef="title">
              <th mat-header-cell *matHeaderCellDef>Title</th>
              <td mat-cell *matCellDef="let story">{{ story.title }}</td>
            </ng-container>

            <ng-container matColumnDef="content">
              <th mat-header-cell *matHeaderCellDef>Content</th>
              <td mat-cell *matCellDef="let story">{{ truncate(story.content, 100) }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created</th>
              <td mat-cell *matCellDef="let story">{{ story.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let story">
                <button mat-icon-button color="primary" (click)="editStory(story)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteStory(story.storyId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .stories {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
    }

    .stories__form-card,
    .stories__table-card {
      margin-bottom: 20px;
    }

    .stories__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .stories__form-field {
      width: 100%;
    }

    .stories__form-field--full {
      grid-column: 1 / -1;
    }

    .stories__form-actions {
      grid-column: 1 / -1;
      display: flex;
      gap: 12px;
    }

    .stories__table {
      width: 100%;
    }
  `]
})
export class Stories implements OnInit {
  storyForm: FormGroup;
  stories$ = this.storyService.stories$;
  persons$ = this.personService.persons$;
  displayedColumns = ['person', 'title', 'content', 'createdAt', 'actions'];
  isEditing = false;
  editingId?: string;
  private personsMap = new Map<string, Person>();

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private personService: PersonService
  ) {
    this.storyForm = this.fb.group({
      personId: ['', Validators.required],
      title: ['', Validators.required],
      content: ['']
    });
  }

  ngOnInit(): void {
    this.personService.getPersons().subscribe(persons => {
      persons.forEach(person => {
        this.personsMap.set(person.personId, person);
      });
    });
    this.storyService.getStories().subscribe();
  }

  saveStory(): void {
    if (this.storyForm.valid) {
      if (this.isEditing && this.editingId) {
        this.storyService.updateStory(this.editingId, this.storyForm.value).subscribe(() => {
          this.resetForm();
        });
      } else {
        this.storyService.createStory(this.storyForm.value).subscribe(() => {
          this.resetForm();
        });
      }
    }
  }

  editStory(story: Story): void {
    this.isEditing = true;
    this.editingId = story.storyId;
    this.storyForm.patchValue(story);
  }

  cancelEdit(): void {
    this.resetForm();
  }

  deleteStory(id: string): void {
    if (confirm('Are you sure you want to delete this story?')) {
      this.storyService.deleteStory(id).subscribe();
    }
  }

  getPersonName(personId: string): string {
    const person = this.personsMap.get(personId);
    return person ? `${person.firstName} ${person.lastName || ''}`.trim() : 'Unknown';
  }

  truncate(text?: string, length: number = 100): string {
    if (!text) return '';
    return text.length > length ? text.substring(0, length) + '...' : text;
  }

  private resetForm(): void {
    this.isEditing = false;
    this.editingId = undefined;
    this.storyForm.reset();
  }
}
