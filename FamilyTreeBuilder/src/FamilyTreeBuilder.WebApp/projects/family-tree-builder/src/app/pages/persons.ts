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
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { PersonService } from '../services';
import { Person, Gender } from '../models';

@Component({
  selector: 'app-persons',
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
    MatNativeDateModule,
    MatDialogModule
  ],
  template: `
    <div class="persons">
      <mat-card class="persons__form-card">
        <mat-card-header>
          <mat-card-title>{{ isEditing ? 'Edit Person' : 'Add New Person' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="personForm" (ngSubmit)="savePerson()" class="persons__form">
            <mat-form-field class="persons__form-field">
              <mat-label>First Name</mat-label>
              <input matInput formControlName="firstName" required>
            </mat-form-field>

            <mat-form-field class="persons__form-field">
              <mat-label>Last Name</mat-label>
              <input matInput formControlName="lastName">
            </mat-form-field>

            <mat-form-field class="persons__form-field">
              <mat-label>Gender</mat-label>
              <mat-select formControlName="gender">
                <mat-option [value]="null">None</mat-option>
                <mat-option [value]="Gender.Male">Male</mat-option>
                <mat-option [value]="Gender.Female">Female</mat-option>
                <mat-option [value]="Gender.Other">Other</mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="persons__form-field">
              <mat-label>Date of Birth</mat-label>
              <input matInput [matDatepicker]="birthPicker" formControlName="dateOfBirth">
              <mat-datepicker-toggle matSuffix [for]="birthPicker"></mat-datepicker-toggle>
              <mat-datepicker #birthPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="persons__form-field">
              <mat-label>Date of Death</mat-label>
              <input matInput [matDatepicker]="deathPicker" formControlName="dateOfDeath">
              <mat-datepicker-toggle matSuffix [for]="deathPicker"></mat-datepicker-toggle>
              <mat-datepicker #deathPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="persons__form-field">
              <mat-label>Birth Place</mat-label>
              <input matInput formControlName="birthPlace">
            </mat-form-field>

            <div class="persons__form-actions">
              <button mat-raised-button color="primary" type="submit" [disabled]="!personForm.valid">
                {{ isEditing ? 'Update' : 'Create' }}
              </button>
              <button mat-button type="button" (click)="cancelEdit()" *ngIf="isEditing">
                Cancel
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="persons__table-card">
        <mat-card-header>
          <mat-card-title>Family Members</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="persons$ | async" class="persons__table">
            <ng-container matColumnDef="firstName">
              <th mat-header-cell *matHeaderCellDef>First Name</th>
              <td mat-cell *matCellDef="let person">{{ person.firstName }}</td>
            </ng-container>

            <ng-container matColumnDef="lastName">
              <th mat-header-cell *matHeaderCellDef>Last Name</th>
              <td mat-cell *matCellDef="let person">{{ person.lastName }}</td>
            </ng-container>

            <ng-container matColumnDef="gender">
              <th mat-header-cell *matHeaderCellDef>Gender</th>
              <td mat-cell *matCellDef="let person">{{ getGenderLabel(person.gender) }}</td>
            </ng-container>

            <ng-container matColumnDef="dateOfBirth">
              <th mat-header-cell *matHeaderCellDef>Date of Birth</th>
              <td mat-cell *matCellDef="let person">{{ person.dateOfBirth | date }}</td>
            </ng-container>

            <ng-container matColumnDef="birthPlace">
              <th mat-header-cell *matHeaderCellDef>Birth Place</th>
              <td mat-cell *matCellDef="let person">{{ person.birthPlace }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let person">
                <button mat-icon-button color="primary" (click)="editPerson(person)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deletePerson(person.personId)">
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
    .persons {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
    }

    .persons__form-card,
    .persons__table-card {
      margin-bottom: 20px;
    }

    .persons__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .persons__form-field {
      width: 100%;
    }

    .persons__form-actions {
      grid-column: 1 / -1;
      display: flex;
      gap: 12px;
    }

    .persons__table {
      width: 100%;
    }
  `]
})
export class Persons implements OnInit {
  personForm: FormGroup;
  persons$ = this.personService.persons$;
  displayedColumns = ['firstName', 'lastName', 'gender', 'dateOfBirth', 'birthPlace', 'actions'];
  isEditing = false;
  editingId?: string;
  Gender = Gender;

  constructor(
    private fb: FormBuilder,
    private personService: PersonService
  ) {
    this.personForm = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000'],
      firstName: ['', Validators.required],
      lastName: [''],
      gender: [null],
      dateOfBirth: [null],
      dateOfDeath: [null],
      birthPlace: ['']
    });
  }

  ngOnInit(): void {
    this.personService.getPersons().subscribe();
  }

  savePerson(): void {
    if (this.personForm.valid) {
      const formValue = this.personForm.value;
      const request = {
        ...formValue,
        dateOfBirth: formValue.dateOfBirth ? formValue.dateOfBirth.toISOString() : null,
        dateOfDeath: formValue.dateOfDeath ? formValue.dateOfDeath.toISOString() : null
      };

      if (this.isEditing && this.editingId) {
        this.personService.updatePerson(this.editingId, request).subscribe(() => {
          this.resetForm();
        });
      } else {
        this.personService.createPerson(request).subscribe(() => {
          this.resetForm();
        });
      }
    }
  }

  editPerson(person: Person): void {
    this.isEditing = true;
    this.editingId = person.personId;
    this.personForm.patchValue({
      ...person,
      dateOfBirth: person.dateOfBirth ? new Date(person.dateOfBirth) : null,
      dateOfDeath: person.dateOfDeath ? new Date(person.dateOfDeath) : null
    });
  }

  cancelEdit(): void {
    this.resetForm();
  }

  deletePerson(id: string): void {
    if (confirm('Are you sure you want to delete this person?')) {
      this.personService.deletePerson(id).subscribe();
    }
  }

  getGenderLabel(gender?: Gender): string {
    if (gender === undefined || gender === null) return '';
    return Gender[gender];
  }

  private resetForm(): void {
    this.isEditing = false;
    this.editingId = undefined;
    this.personForm.reset({
      userId: '00000000-0000-0000-0000-000000000000'
    });
  }
}
