import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Category } from '../../models';

@Component({
  selector: 'app-create-category-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './create-category-dialog.html',
  styleUrl: './create-category-dialog.scss'
})
export class CreateCategoryDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<CreateCategoryDialog>);

  form!: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { category?: Category }) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      name: [this.data.category?.name || '', Validators.required],
      color: [this.data.category?.color || '#000000', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
