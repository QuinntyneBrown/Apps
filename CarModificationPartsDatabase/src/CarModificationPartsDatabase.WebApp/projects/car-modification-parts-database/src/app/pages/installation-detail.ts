import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { InstallationsService } from '../services';
import { Installation } from '../models';

@Component({
  selector: 'app-installation-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatChipsModule,
    MatIconModule
  ],
  template: `
    <div class="installation-detail">
      <div class="installation-detail__header">
        <h1>{{ isEditMode ? (isNewInstallation ? 'New Installation' : 'Edit Installation') : 'Installation Details' }}</h1>
        <div>
          <a mat-button routerLink="/installations">Back to List</a>
          <button *ngIf="!isEditMode && !isNewInstallation" mat-raised-button color="primary" (click)="toggleEditMode()">Edit</button>
        </div>
      </div>

      <mat-card *ngIf="!isEditMode && installation" class="installation-detail__view">
        <mat-card-content>
          <div class="installation-detail__field">
            <label>Vehicle:</label>
            <span>{{ installation.vehicleInfo }}</span>
          </div>
          <div class="installation-detail__field">
            <label>Installation Date:</label>
            <span>{{ installation.installationDate | date }}</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.installedBy">
            <label>Installed By:</label>
            <span>{{ installation.installedBy }}</span>
          </div>
          <div class="installation-detail__field">
            <label>Status:</label>
            <span>{{ installation.isCompleted ? 'Completed' : 'Pending' }}</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.installationCost">
            <label>Installation Cost:</label>
            <span>{{ installation.installationCost | currency }}</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.partsCost">
            <label>Parts Cost:</label>
            <span>{{ installation.partsCost | currency }}</span>
          </div>
          <div class="installation-detail__field">
            <label>Total Cost:</label>
            <span><strong>{{ installation.totalCost | currency }}</strong></span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.laborHours">
            <label>Labor Hours:</label>
            <span>{{ installation.laborHours }}</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.difficultyRating">
            <label>Difficulty Rating:</label>
            <span>{{ installation.difficultyRating }}/5</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.satisfactionRating">
            <label>Satisfaction Rating:</label>
            <span>{{ installation.satisfactionRating }}/5</span>
          </div>
          <div class="installation-detail__field" *ngIf="installation.partsUsed.length">
            <label>Parts Used:</label>
            <div class="installation-detail__chips">
              <mat-chip *ngFor="let part of installation.partsUsed">{{ part }}</mat-chip>
            </div>
          </div>
          <div class="installation-detail__field" *ngIf="installation.notes">
            <label>Notes:</label>
            <span>{{ installation.notes }}</span>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card *ngIf="isEditMode" class="installation-detail__form">
        <mat-card-content>
          <form [formGroup]="installationForm" (ngSubmit)="onSubmit()">
            <mat-form-field appearance="outline">
              <mat-label>Modification ID</mat-label>
              <input matInput formControlName="modificationId" required>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Vehicle Info</mat-label>
              <input matInput formControlName="vehicleInfo" required>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Installation Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="installationDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Installed By</mat-label>
              <input matInput formControlName="installedBy">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Installation Cost</mat-label>
              <input matInput type="number" formControlName="installationCost">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Parts Cost</mat-label>
              <input matInput type="number" formControlName="partsCost">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Labor Hours</mat-label>
              <input matInput type="number" formControlName="laborHours">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Difficulty Rating (1-5)</mat-label>
              <input matInput type="number" formControlName="difficultyRating" min="1" max="5">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Satisfaction Rating (1-5)</mat-label>
              <input matInput type="number" formControlName="satisfactionRating" min="1" max="5">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>

            <mat-checkbox formControlName="isCompleted">Completed</mat-checkbox>

            <div class="installation-detail__actions">
              <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!installationForm.valid">Save</button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .installation-detail {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;

        div {
          display: flex;
          gap: 1rem;
        }
      }

      &__view {
        max-width: 800px;
      }

      &__field {
        margin-bottom: 1rem;

        label {
          font-weight: 500;
          display: block;
          margin-bottom: 0.25rem;
        }
      }

      &__chips {
        display: flex;
        flex-wrap: wrap;
        gap: 0.5rem;
        margin-top: 0.5rem;
      }

      &__form {
        max-width: 800px;

        form {
          display: flex;
          flex-direction: column;
          gap: 1rem;
        }
      }

      &__actions {
        display: flex;
        gap: 1rem;
        justify-content: flex-end;
        margin-top: 1rem;
      }
    }
  `]
})
export class InstallationDetail implements OnInit {
  installation: Installation | null = null;
  installationForm: FormGroup;
  isEditMode = false;
  isNewInstallation = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private installationsService: InstallationsService,
    private fb: FormBuilder
  ) {
    this.installationForm = this.fb.group({
      modificationId: ['', Validators.required],
      vehicleInfo: ['', Validators.required],
      installationDate: [new Date(), Validators.required],
      installedBy: [''],
      installationCost: [null],
      partsCost: [null],
      laborHours: [null],
      difficultyRating: [null, [Validators.min(1), Validators.max(5)]],
      satisfactionRating: [null, [Validators.min(1), Validators.max(5)]],
      notes: [''],
      isCompleted: [false]
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    const isEdit = this.route.snapshot.url.some(segment => segment.path === 'edit');

    if (id === 'new') {
      this.isNewInstallation = true;
      this.isEditMode = true;
    } else if (id) {
      this.installationsService.getById(id).subscribe(installation => {
        this.installation = installation;
        if (isEdit) {
          this.isEditMode = true;
          this.installationForm.patchValue({
            ...installation,
            installationDate: new Date(installation.installationDate)
          });
        }
      });
    }
  }

  toggleEditMode() {
    this.isEditMode = !this.isEditMode;
    if (this.isEditMode && this.installation) {
      this.installationForm.patchValue({
        ...this.installation,
        installationDate: new Date(this.installation.installationDate)
      });
    }
  }

  cancel() {
    if (this.isNewInstallation) {
      this.router.navigate(['/installations']);
    } else {
      this.isEditMode = false;
    }
  }

  onSubmit() {
    if (this.installationForm.valid) {
      const formValue = this.installationForm.value;

      if (this.isNewInstallation) {
        this.installationsService.create(formValue).subscribe(() => {
          this.router.navigate(['/installations']);
        });
      } else if (this.installation) {
        this.installationsService.update(this.installation.installationId, {
          ...formValue,
          installationId: this.installation.installationId
        }).subscribe(updatedInstallation => {
          this.installation = updatedInstallation;
          this.isEditMode = false;
        });
      }
    }
  }
}
