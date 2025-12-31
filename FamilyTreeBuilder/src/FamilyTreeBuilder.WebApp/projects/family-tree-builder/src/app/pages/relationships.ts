import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { RelationshipService, PersonService } from '../services';
import { Relationship, RelationshipType, Person } from '../models';

@Component({
  selector: 'app-relationships',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  template: `
    <div class="relationships">
      <mat-card class="relationships__form-card">
        <mat-card-header>
          <mat-card-title>Add New Relationship</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="relationshipForm" (ngSubmit)="createRelationship()" class="relationships__form">
            <mat-form-field class="relationships__form-field">
              <mat-label>Person</mat-label>
              <mat-select formControlName="personId" required>
                <mat-option *ngFor="let person of persons$ | async" [value]="person.personId">
                  {{ person.firstName }} {{ person.lastName }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="relationships__form-field">
              <mat-label>Related Person</mat-label>
              <mat-select formControlName="relatedPersonId" required>
                <mat-option *ngFor="let person of persons$ | async" [value]="person.personId">
                  {{ person.firstName }} {{ person.lastName }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="relationships__form-field">
              <mat-label>Relationship Type</mat-label>
              <mat-select formControlName="relationshipType" required>
                <mat-option [value]="RelationshipType.Parent">Parent</mat-option>
                <mat-option [value]="RelationshipType.Child">Child</mat-option>
                <mat-option [value]="RelationshipType.Spouse">Spouse</mat-option>
                <mat-option [value]="RelationshipType.Sibling">Sibling</mat-option>
                <mat-option [value]="RelationshipType.Other">Other</mat-option>
              </mat-select>
            </mat-form-field>

            <div class="relationships__form-actions">
              <button mat-raised-button color="primary" type="submit" [disabled]="!relationshipForm.valid">
                Create
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="relationships__table-card">
        <mat-card-header>
          <mat-card-title>Family Relationships</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="relationships$ | async" class="relationships__table">
            <ng-container matColumnDef="person">
              <th mat-header-cell *matHeaderCellDef>Person</th>
              <td mat-cell *matCellDef="let relationship">{{ getPersonName(relationship.personId) }}</td>
            </ng-container>

            <ng-container matColumnDef="relationshipType">
              <th mat-header-cell *matHeaderCellDef>Relationship</th>
              <td mat-cell *matCellDef="let relationship">{{ getRelationshipTypeLabel(relationship.relationshipType) }}</td>
            </ng-container>

            <ng-container matColumnDef="relatedPerson">
              <th mat-header-cell *matHeaderCellDef>Related Person</th>
              <td mat-cell *matCellDef="let relationship">{{ getPersonName(relationship.relatedPersonId) }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created</th>
              <td mat-cell *matCellDef="let relationship">{{ relationship.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let relationship">
                <button mat-icon-button color="warn" (click)="deleteRelationship(relationship.relationshipId)">
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
    .relationships {
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
    }

    .relationships__form-card,
    .relationships__table-card {
      margin-bottom: 20px;
    }

    .relationships__form {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .relationships__form-field {
      width: 100%;
    }

    .relationships__form-actions {
      grid-column: 1 / -1;
      display: flex;
      gap: 12px;
    }

    .relationships__table {
      width: 100%;
    }
  `]
})
export class Relationships implements OnInit {
  relationshipForm: FormGroup;
  relationships$ = this.relationshipService.relationships$;
  persons$ = this.personService.persons$;
  displayedColumns = ['person', 'relationshipType', 'relatedPerson', 'createdAt', 'actions'];
  RelationshipType = RelationshipType;
  private personsMap = new Map<string, Person>();

  constructor(
    private fb: FormBuilder,
    private relationshipService: RelationshipService,
    private personService: PersonService
  ) {
    this.relationshipForm = this.fb.group({
      personId: ['', Validators.required],
      relatedPersonId: ['', Validators.required],
      relationshipType: [RelationshipType.Parent, Validators.required]
    });
  }

  ngOnInit(): void {
    this.personService.getPersons().subscribe(persons => {
      persons.forEach(person => {
        this.personsMap.set(person.personId, person);
      });
    });
    this.relationshipService.getRelationships().subscribe();
  }

  createRelationship(): void {
    if (this.relationshipForm.valid) {
      this.relationshipService.createRelationship(this.relationshipForm.value).subscribe(() => {
        this.relationshipForm.reset({
          relationshipType: RelationshipType.Parent
        });
      });
    }
  }

  deleteRelationship(id: string): void {
    if (confirm('Are you sure you want to delete this relationship?')) {
      this.relationshipService.deleteRelationship(id).subscribe();
    }
  }

  getRelationshipTypeLabel(type: RelationshipType): string {
    return RelationshipType[type];
  }

  getPersonName(personId: string): string {
    const person = this.personsMap.get(personId);
    return person ? `${person.firstName} ${person.lastName || ''}`.trim() : 'Unknown';
  }
}
