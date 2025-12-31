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
import { RecipeService } from '../services';
import { Recipe, CreateRecipe, UpdateRecipe, MeatType, CookingMethod, MeatTypeLabels, CookingMethodLabels } from '../models';

@Component({
  selector: 'app-recipe-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Recipe' : 'New Recipe' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="recipe-dialog__form">
        <mat-form-field class="recipe-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Meat Type</mat-label>
          <mat-select formControlName="meatType" required>
            @for (type of meatTypes; track type.value) {
              <mat-option [value]="type.value">{{ type.label }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Cooking Method</mat-label>
          <mat-select formControlName="cookingMethod" required>
            @for (method of cookingMethods; track method.value) {
              <mat-option [value]="method.value">{{ method.label }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Prep Time (minutes)</mat-label>
          <input matInput type="number" formControlName="prepTimeMinutes" required>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Cook Time (minutes)</mat-label>
          <input matInput type="number" formControlName="cookTimeMinutes" required>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Ingredients</mat-label>
          <textarea matInput formControlName="ingredients" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Instructions</mat-label>
          <textarea matInput formControlName="instructions" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Target Temperature (F)</mat-label>
          <input matInput type="number" formControlName="targetTemperature">
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Servings</mat-label>
          <input matInput type="number" formControlName="servings" required>
        </mat-form-field>

        <mat-form-field class="recipe-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="2"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .recipe-dialog__form {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .recipe-dialog__field {
      width: 100%;
    }
  `]
})
export class RecipeDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: Recipe | null = null;
  form: FormGroup;

  meatTypes = Object.keys(MeatTypeLabels).map(key => ({
    value: Number(key),
    label: MeatTypeLabels[Number(key) as MeatType]
  }));

  cookingMethods = Object.keys(CookingMethodLabels).map(key => ({
    value: Number(key),
    label: CookingMethodLabels[Number(key) as CookingMethod]
  }));

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      meatType: [MeatType.Beef, Validators.required],
      cookingMethod: [CookingMethod.DirectGrilling, Validators.required],
      prepTimeMinutes: [0, [Validators.required, Validators.min(0)]],
      cookTimeMinutes: [0, [Validators.required, Validators.min(0)]],
      ingredients: ['', Validators.required],
      instructions: ['', Validators.required],
      targetTemperature: [null],
      servings: [4, [Validators.required, Validators.min(1)]],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './recipes.html',
  styleUrl: './recipes.scss'
})
export class Recipes implements OnInit {
  private recipeService = inject(RecipeService);
  private dialog = inject(MatDialog);
  private fb = inject(FormBuilder);

  recipes$ = this.recipeService.recipes$;
  displayedColumns = ['name', 'meatType', 'cookingMethod', 'prepTime', 'cookTime', 'servings', 'favorite', 'actions'];

  meatTypeLabels = MeatTypeLabels;
  cookingMethodLabels = CookingMethodLabels;

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe();
  }

  openDialog(recipe?: Recipe): void {
    const dialogRef = this.dialog.open(RecipeDialog, {
      width: '600px',
      data: recipe || null
    });

    const dialogComponent = dialogRef.componentInstance;
    dialogComponent.data = recipe || null;

    if (recipe) {
      dialogComponent.form.patchValue(recipe);
    }

    dialogRef.afterClosed().subscribe(result => {
      if (dialogComponent.form.valid) {
        const formValue = dialogComponent.form.value;

        if (recipe) {
          const updateData: UpdateRecipe = {
            recipeId: recipe.recipeId,
            ...formValue
          };
          this.recipeService.updateRecipe(updateData).subscribe();
        } else {
          const createData: CreateRecipe = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...formValue
          };
          this.recipeService.createRecipe(createData).subscribe();
        }
      }
    });
  }

  deleteRecipe(id: string): void {
    if (confirm('Are you sure you want to delete this recipe?')) {
      this.recipeService.deleteRecipe(id).subscribe();
    }
  }
}
