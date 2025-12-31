import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CookSessionService, RecipeService } from '../services';
import { CookSession, CreateCookSession, UpdateCookSession } from '../models';

@Component({
  selector: 'app-cook-session-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Cook Session' : 'New Cook Session' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="cook-session-dialog__form">
        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Recipe</mat-label>
          <mat-select formControlName="recipeId" required>
            @for (recipe of recipes$ | async; track recipe.recipeId) {
              <mat-option [value]="recipe.recipeId">{{ recipe.name }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Cook Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="cookDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Actual Cook Time (minutes)</mat-label>
          <input matInput type="number" formControlName="actualCookTimeMinutes" required>
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Temperature Used (F)</mat-label>
          <input matInput type="number" formControlName="temperatureUsed">
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Rating (1-5)</mat-label>
          <input matInput type="number" formControlName="rating" min="1" max="5">
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="cook-session-dialog__field">
          <mat-label>Modifications</mat-label>
          <textarea matInput formControlName="modifications" rows="3"></textarea>
        </mat-form-field>

        <div class="cook-session-dialog__checkbox">
          <mat-checkbox formControlName="wasSuccessful">Was Successful</mat-checkbox>
        </div>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .cook-session-dialog__form {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .cook-session-dialog__field {
      width: 100%;
    }

    .cook-session-dialog__checkbox {
      margin: 1rem 0;
    }
  `]
})
export class CookSessionDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private recipeService = inject(RecipeService);

  data: CookSession | null = null;
  form: FormGroup;
  recipes$ = this.recipeService.recipes$;

  constructor() {
    this.form = this.fb.group({
      recipeId: ['', Validators.required],
      cookDate: [new Date(), Validators.required],
      actualCookTimeMinutes: [0, [Validators.required, Validators.min(0)]],
      temperatureUsed: [null],
      rating: [null, [Validators.min(1), Validators.max(5)]],
      notes: [''],
      modifications: [''],
      wasSuccessful: [true]
    });

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        cookDate: new Date(this.data.cookDate)
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-cook-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './cook-sessions.html',
  styleUrl: './cook-sessions.scss'
})
export class CookSessions implements OnInit {
  private cookSessionService = inject(CookSessionService);
  private recipeService = inject(RecipeService);
  private dialog = inject(MatDialog);

  cookSessions$ = this.cookSessionService.cookSessions$;
  recipes$ = this.recipeService.recipes$;
  displayedColumns = ['cookDate', 'recipe', 'actualCookTime', 'temperature', 'rating', 'wasSuccessful', 'actions'];

  ngOnInit(): void {
    this.cookSessionService.getCookSessions().subscribe();
    this.recipeService.getRecipes().subscribe();
  }

  getRecipeName(recipeId: string): string {
    const recipes = this.recipeService.recipes$.value;
    const recipe = recipes.find(r => r.recipeId === recipeId);
    return recipe ? recipe.name : 'Unknown';
  }

  openDialog(session?: CookSession): void {
    const dialogRef = this.dialog.open(CookSessionDialog, {
      width: '600px',
      data: session || null
    });

    const dialogComponent = dialogRef.componentInstance;
    dialogComponent.data = session || null;

    if (session) {
      dialogComponent.form.patchValue({
        ...session,
        cookDate: new Date(session.cookDate)
      });
    }

    dialogRef.afterClosed().subscribe(result => {
      if (dialogComponent.form.valid) {
        const formValue = dialogComponent.form.value;
        const cookDate = new Date(formValue.cookDate);

        if (session) {
          const updateData: UpdateCookSession = {
            cookSessionId: session.cookSessionId,
            ...formValue,
            cookDate: cookDate.toISOString()
          };
          this.cookSessionService.updateCookSession(updateData).subscribe();
        } else {
          const createData: CreateCookSession = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...formValue,
            cookDate: cookDate.toISOString()
          };
          this.cookSessionService.createCookSession(createData).subscribe();
        }
      }
    });
  }

  deleteCookSession(id: string): void {
    if (confirm('Are you sure you want to delete this cook session?')) {
      this.cookSessionService.deleteCookSession(id).subscribe();
    }
  }
}
