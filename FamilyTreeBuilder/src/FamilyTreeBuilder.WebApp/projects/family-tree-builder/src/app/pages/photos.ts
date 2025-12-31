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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FamilyPhotoService, PersonService } from '../services';
import { FamilyPhoto, Person } from '../models';

@Component({
  selector: 'app-photos',
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
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="photos">
      <mat-card class="photos__form-card">
        <mat-card-header>
          <mat-card-title>{{ isEditing ? 'Edit Photo' : 'Add New Photo' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="photoForm" (ngSubmit)="savePhoto()" class="photos__form">
            <mat-form-field class="photos__form-field">
              <mat-label>Person</mat-label>
              <mat-select formControlName="personId" required>
                <mat-option *ngFor="let person of persons$ | async" [value]="person.personId">
                  {{ person.firstName }} {{ person.lastName }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="photos__form-field">
              <mat-label>Photo URL</mat-label>
              <input matInput formControlName="photoUrl">
            </mat-form-field>

            <mat-form-field class="photos__form-field">
              <mat-label>Caption</mat-label>
              <input matInput formControlName="caption">
            </mat-form-field>

            <mat-form-field class="photos__form-field">
              <mat-label>Date Taken</mat-label>
              <input matInput [matDatepicker]="dateTakenPicker" formControlName="dateTaken">
              <mat-datepicker-toggle matSuffix [for]="dateTakenPicker"></mat-datepicker-toggle>
              <mat-datepicker #dateTakenPicker></mat-datepicker>
            </mat-form-field>

            <div class="photos__form-actions">
              <button mat-raised-button color="primary" type="submit" [disabled]="!photoForm.valid">
                {{ isEditing ? 'Update' : 'Create' }}
              </button>
              <button mat-button type="button" (click)="cancelEdit()" *ngIf="isEditing">
                Cancel
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="photos__table-card">
        <mat-card-header>
          <mat-card-title>Family Photos</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="photos$ | async" class="photos__table">
            <ng-container matColumnDef="person">
              <th mat-header-cell *matHeaderCellDef>Person</th>
              <td mat-cell *matCellDef="let photo">{{ getPersonName(photo.personId) }}</td>
            </ng-container>

            <ng-container matColumnDef="photoUrl">
              <th mat-header-cell *matHeaderCellDef>Photo</th>
              <td mat-cell *matCellDef="let photo">
                <img *ngIf="photo.photoUrl" [src]="photo.photoUrl" alt="Family photo" class="photos__thumbnail">
                <span *ngIf="!photo.photoUrl">No photo</span>
              </td>
            </ng-container>

            <ng-container matColumnDef="caption">
              <th mat-header-cell *matHeaderCellDef>Caption</th>
              <td mat-cell *matCellDef="let photo">{{ photo.caption }}</td>
            </ng-container>

            <ng-container matColumnDef="dateTaken">
              <th mat-header-cell *matHeaderCellDef>Date Taken</th>
              <td mat-cell *matCellDef="let photo">{{ photo.dateTaken | date }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created</th>
              <td mat-cell *matCellDef="let photo">{{ photo.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let photo">
                <button mat-icon-button color="primary" (click)="editPhoto(photo)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deletePhoto(photo.familyPhotoId)">
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
    .photos {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
    }

    .photos__form-card,
    .photos__table-card {
      margin-bottom: 20px;
    }

    .photos__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .photos__form-field {
      width: 100%;
    }

    .photos__form-actions {
      grid-column: 1 / -1;
      display: flex;
      gap: 12px;
    }

    .photos__table {
      width: 100%;
    }

    .photos__thumbnail {
      width: 50px;
      height: 50px;
      object-fit: cover;
      border-radius: 4px;
    }
  `]
})
export class Photos implements OnInit {
  photoForm: FormGroup;
  photos$ = this.photoService.photos$;
  persons$ = this.personService.persons$;
  displayedColumns = ['person', 'photoUrl', 'caption', 'dateTaken', 'createdAt', 'actions'];
  isEditing = false;
  editingId?: string;
  private personsMap = new Map<string, Person>();

  constructor(
    private fb: FormBuilder,
    private photoService: FamilyPhotoService,
    private personService: PersonService
  ) {
    this.photoForm = this.fb.group({
      personId: ['', Validators.required],
      photoUrl: [''],
      caption: [''],
      dateTaken: [null]
    });
  }

  ngOnInit(): void {
    this.personService.getPersons().subscribe(persons => {
      persons.forEach(person => {
        this.personsMap.set(person.personId, person);
      });
    });
    this.photoService.getFamilyPhotos().subscribe();
  }

  savePhoto(): void {
    if (this.photoForm.valid) {
      const formValue = this.photoForm.value;
      const request = {
        ...formValue,
        dateTaken: formValue.dateTaken ? formValue.dateTaken.toISOString() : null
      };

      if (this.isEditing && this.editingId) {
        this.photoService.updateFamilyPhoto(this.editingId, request).subscribe(() => {
          this.resetForm();
        });
      } else {
        this.photoService.createFamilyPhoto(request).subscribe(() => {
          this.resetForm();
        });
      }
    }
  }

  editPhoto(photo: FamilyPhoto): void {
    this.isEditing = true;
    this.editingId = photo.familyPhotoId;
    this.photoForm.patchValue({
      ...photo,
      dateTaken: photo.dateTaken ? new Date(photo.dateTaken) : null
    });
  }

  cancelEdit(): void {
    this.resetForm();
  }

  deletePhoto(id: string): void {
    if (confirm('Are you sure you want to delete this photo?')) {
      this.photoService.deleteFamilyPhoto(id).subscribe();
    }
  }

  getPersonName(personId: string): string {
    const person = this.personsMap.get(personId);
    return person ? `${person.firstName} ${person.lastName || ''}`.trim() : 'Unknown';
  }

  private resetForm(): void {
    this.isEditing = false;
    this.editingId = undefined;
    this.photoForm.reset();
  }
}
