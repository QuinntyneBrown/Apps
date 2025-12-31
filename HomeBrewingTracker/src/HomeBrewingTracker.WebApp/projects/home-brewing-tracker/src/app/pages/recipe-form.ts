import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RecipeService } from '../services';
import { BeerStyle, BeerStyleLabels } from '../models';

@Component({
  selector: 'app-recipe-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="recipe-form">
      <div class="recipe-form__header">
        <button mat-icon-button (click)="goBack()">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="recipe-form__title">{{ isEdit ? 'Edit Recipe' : 'New Recipe' }}</h1>
      </div>

      <mat-card class="recipe-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()">
          <div class="recipe-form__row">
            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>Recipe Name</mat-label>
              <input matInput formControlName="name" required>
              <mat-error *ngIf="form.get('name')?.hasError('required')">Name is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>Beer Style</mat-label>
              <mat-select formControlName="beerStyle" required>
                <mat-option *ngFor="let style of beerStyles" [value]="style.value">
                  {{ style.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('beerStyle')?.hasError('required')">Beer style is required</mat-error>
            </mat-form-field>
          </div>

          <mat-form-field appearance="outline" class="recipe-form__field recipe-form__field--full">
            <mat-label>Description</mat-label>
            <textarea matInput formControlName="description" rows="3" required></textarea>
            <mat-error *ngIf="form.get('description')?.hasError('required')">Description is required</mat-error>
          </mat-form-field>

          <div class="recipe-form__row">
            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>Original Gravity</mat-label>
              <input matInput type="number" step="0.001" formControlName="originalGravity">
            </mat-form-field>

            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>Final Gravity</mat-label>
              <input matInput type="number" step="0.001" formControlName="finalGravity">
            </mat-form-field>
          </div>

          <div class="recipe-form__row">
            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>ABV (%)</mat-label>
              <input matInput type="number" step="0.1" formControlName="abv">
            </mat-form-field>

            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>IBU</mat-label>
              <input matInput type="number" formControlName="ibu">
            </mat-form-field>

            <mat-form-field appearance="outline" class="recipe-form__field">
              <mat-label>Batch Size (L)</mat-label>
              <input matInput type="number" step="0.1" formControlName="batchSize" required>
              <mat-error *ngIf="form.get('batchSize')?.hasError('required')">Batch size is required</mat-error>
            </mat-form-field>
          </div>

          <mat-form-field appearance="outline" class="recipe-form__field recipe-form__field--full">
            <mat-label>Ingredients</mat-label>
            <textarea matInput formControlName="ingredients" rows="4" required></textarea>
            <mat-error *ngIf="form.get('ingredients')?.hasError('required')">Ingredients are required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="recipe-form__field recipe-form__field--full">
            <mat-label>Instructions</mat-label>
            <textarea matInput formControlName="instructions" rows="6" required></textarea>
            <mat-error *ngIf="form.get('instructions')?.hasError('required')">Instructions are required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="recipe-form__field recipe-form__field--full">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="3"></textarea>
          </mat-form-field>

          <div class="recipe-form__actions">
            <button mat-button type="button" (click)="goBack()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              <mat-icon>save</mat-icon>
              {{ isEdit ? 'Update' : 'Create' }} Recipe
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .recipe-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;

      &__header {
        display: flex;
        align-items: center;
        margin-bottom: 2rem;
        gap: 1rem;
      }

      &__title {
        font-size: 2rem;
        margin: 0;
      }

      &__card {
        padding: 2rem;
      }

      &__row {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
        gap: 1rem;
        margin-bottom: 1rem;
      }

      &__field {
        width: 100%;

        &--full {
          margin-bottom: 1rem;
        }
      }

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 2rem;
      }
    }
  `]
})
export class RecipeForm implements OnInit {
  form: FormGroup;
  isEdit = false;
  recipeId?: string;
  beerStyles = Object.keys(BeerStyle)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: BeerStyleLabels[Number(key)]
    }));

  constructor(
    private fb: FormBuilder,
    private recipeService: RecipeService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      beerStyle: [BeerStyle.PaleAle, Validators.required],
      description: ['', Validators.required],
      originalGravity: [null],
      finalGravity: [null],
      abv: [null],
      ibu: [null],
      batchSize: [5.0, Validators.required],
      ingredients: ['', Validators.required],
      instructions: ['', Validators.required],
      notes: ['']
    });
  }

  ngOnInit() {
    this.recipeId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEdit = !!this.recipeId && this.route.snapshot.url[this.route.snapshot.url.length - 1].path === 'edit';

    if (this.recipeId && this.isEdit) {
      this.loadRecipe();
    }
  }

  loadRecipe() {
    if (!this.recipeId) return;

    this.recipeService.getRecipe(this.recipeId).subscribe(recipe => {
      this.form.patchValue({
        name: recipe.name,
        beerStyle: recipe.beerStyle,
        description: recipe.description,
        originalGravity: recipe.originalGravity,
        finalGravity: recipe.finalGravity,
        abv: recipe.abv,
        ibu: recipe.ibu,
        batchSize: recipe.batchSize,
        ingredients: recipe.ingredients,
        instructions: recipe.instructions,
        notes: recipe.notes
      });
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const formValue = this.form.value;

    if (this.isEdit && this.recipeId) {
      this.recipeService.updateRecipe(this.recipeId, {
        recipeId: this.recipeId,
        ...formValue
      }).subscribe(() => {
        this.router.navigate(['/recipes']);
      });
    } else {
      this.recipeService.createRecipe({
        userId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        ...formValue
      }).subscribe(() => {
        this.router.navigate(['/recipes']);
      });
    }
  }

  goBack() {
    this.router.navigate(['/recipes']);
  }
}
