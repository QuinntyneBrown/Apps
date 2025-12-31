import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { RegistryItem, Priority } from '../../models';

export interface RegistryItemDialogData {
  item?: RegistryItem;
  registryId: string;
}

@Component({
  selector: 'app-registry-item-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './registry-item-dialog.html',
  styleUrl: './registry-item-dialog.scss'
})
export class RegistryItemDialog implements OnInit {
  form!: FormGroup;
  priorities = Object.values(Priority).filter(v => typeof v === 'number') as Priority[];
  Priority = Priority;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RegistryItemDialog>,
    @Inject(MAT_DIALOG_DATA) public data: RegistryItemDialogData
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.item;
    this.initForm();
  }

  initForm(): void {
    this.form = this.fb.group({
      name: [this.data.item?.name || '', Validators.required],
      description: [this.data.item?.description || ''],
      price: [this.data.item?.price || null, [Validators.min(0)]],
      url: [this.data.item?.url || ''],
      quantityDesired: [this.data.item?.quantityDesired || 1, [Validators.required, Validators.min(1)]],
      priority: [this.data.item?.priority ?? Priority.Medium, Validators.required],
      isFulfilled: [this.data.item?.isFulfilled ?? false]
    });
  }

  getPriorityLabel(priority: Priority): string {
    const labels: Record<Priority, string> = {
      [Priority.Low]: 'Low',
      [Priority.Medium]: 'Medium',
      [Priority.High]: 'High',
      [Priority.MustHave]: 'Must Have'
    };
    return labels[priority];
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        registryId: this.data.registryId,
        ...(this.isEditMode && { registryItemId: this.data.item!.registryItemId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
