import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Recipe, Cuisine, DifficultyLevel, CreateRecipeRequest, UpdateRecipeRequest } from '../../models';

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
    MatButtonModule,
    MatCheckboxModule,
  ],
  templateUrl: './recipe-dialog.html',
  styleUrls: ['./recipe-dialog.scss'],
})
export class RecipeDialog implements OnInit {
  form!: FormGroup;
  cuisines = Object.keys(Cuisine).filter((k) => !isNaN(Number(k))).map((k) => ({
    value: Number(k),
    label: Cuisine[Number(k)],
  }));
  difficultyLevels = Object.keys(DifficultyLevel).filter((k) => !isNaN(Number(k))).map((k) => ({
    value: Number(k),
    label: DifficultyLevel[Number(k)],
  }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RecipeDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { recipe?: Recipe; userId: string }
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.data.recipe?.name || '', Validators.required],
      description: [this.data.recipe?.description || ''],
      cuisine: [this.data.recipe?.cuisine ?? 0, Validators.required],
      difficultyLevel: [this.data.recipe?.difficultyLevel ?? 0, Validators.required],
      prepTimeMinutes: [this.data.recipe?.prepTimeMinutes || 0, [Validators.required, Validators.min(0)]],
      cookTimeMinutes: [this.data.recipe?.cookTimeMinutes || 0, [Validators.required, Validators.min(0)]],
      servings: [this.data.recipe?.servings || 1, [Validators.required, Validators.min(1)]],
      instructions: [this.data.recipe?.instructions || '', Validators.required],
      photoUrl: [this.data.recipe?.photoUrl || ''],
      source: [this.data.recipe?.source || ''],
      rating: [this.data.recipe?.rating || null],
      notes: [this.data.recipe?.notes || ''],
      isFavorite: [this.data.recipe?.isFavorite || false],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.data.recipe) {
        const request: UpdateRecipeRequest = {
          recipeId: this.data.recipe.recipeId,
          ...formValue,
        };
        this.dialogRef.close(request);
      } else {
        const request: CreateRecipeRequest = {
          userId: this.data.userId,
          ...formValue,
        };
        this.dialogRef.close(request);
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
