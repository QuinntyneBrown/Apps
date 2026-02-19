import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

export interface PromptDialogData {
  userId?: string;
}

@Component({
  selector: 'app-prompt-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './prompt-dialog.html',
  styleUrl: './prompt-dialog.scss'
})
export class PromptDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<PromptDialog>);

  form!: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: PromptDialogData) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      text: ['', Validators.required],
      category: ['']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        ...this.form.value,
        ...(this.data.userId && { createdByUserId: this.data.userId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
