import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Tag } from '../../models';

export interface TagDialogData {
  tag?: Tag;
  userId: string;
}

@Component({
  selector: 'app-tag-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './tag-dialog.html',
  styleUrl: './tag-dialog.scss'
})
export class TagDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<TagDialog>);

  form!: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: TagDialogData) {}

  ngOnInit(): void {
    const tag = this.data.tag;
    this.form = this.fb.group({
      name: [tag?.name || '', Validators.required],
      color: [tag?.color || '#3f51b5']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        ...this.form.value,
        userId: this.data.userId,
        ...(this.data.tag && { tagId: this.data.tag.tagId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
