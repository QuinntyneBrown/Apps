import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Item, Category, Room, CategoryLabels, RoomLabels, CreateItemCommand, UpdateItemCommand } from '../../models';

export interface ItemDialogData {
  item?: Item;
  userId: string;
}

@Component({
  selector: 'app-item-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './item-dialog.html',
  styleUrls: ['./item-dialog.scss']
})
export class ItemDialog implements OnInit {
  form!: FormGroup;
  isEditMode = false;

  categories = Object.keys(Category)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ value: Number(key), label: CategoryLabels[Number(key) as Category] }));

  rooms = Object.keys(Room)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ value: Number(key), label: RoomLabels[Number(key) as Room] }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ItemDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ItemDialogData
  ) {
    this.isEditMode = !!data.item;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.data.item?.name || '', Validators.required],
      description: [this.data.item?.description || ''],
      category: [this.data.item?.category ?? Category.Other, Validators.required],
      room: [this.data.item?.room ?? Room.Other, Validators.required],
      brand: [this.data.item?.brand || ''],
      modelNumber: [this.data.item?.modelNumber || ''],
      serialNumber: [this.data.item?.serialNumber || ''],
      purchaseDate: [this.data.item?.purchaseDate ? new Date(this.data.item.purchaseDate) : null],
      purchasePrice: [this.data.item?.purchasePrice ?? null],
      currentValue: [this.data.item?.currentValue ?? null],
      quantity: [this.data.item?.quantity ?? 1, [Validators.required, Validators.min(1)]],
      photoUrl: [this.data.item?.photoUrl || ''],
      receiptUrl: [this.data.item?.receiptUrl || ''],
      notes: [this.data.item?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.data.item) {
        const command: UpdateItemCommand = {
          itemId: this.data.item.itemId,
          name: formValue.name,
          description: formValue.description || null,
          category: formValue.category,
          room: formValue.room,
          brand: formValue.brand || null,
          modelNumber: formValue.modelNumber || null,
          serialNumber: formValue.serialNumber || null,
          purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null,
          purchasePrice: formValue.purchasePrice ?? null,
          currentValue: formValue.currentValue ?? null,
          quantity: formValue.quantity,
          photoUrl: formValue.photoUrl || null,
          receiptUrl: formValue.receiptUrl || null,
          notes: formValue.notes || null
        };
        this.dialogRef.close(command);
      } else {
        const command: CreateItemCommand = {
          userId: this.data.userId,
          name: formValue.name,
          description: formValue.description || null,
          category: formValue.category,
          room: formValue.room,
          brand: formValue.brand || null,
          modelNumber: formValue.modelNumber || null,
          serialNumber: formValue.serialNumber || null,
          purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null,
          purchasePrice: formValue.purchasePrice ?? null,
          currentValue: formValue.currentValue ?? null,
          quantity: formValue.quantity,
          photoUrl: formValue.photoUrl || null,
          receiptUrl: formValue.receiptUrl || null,
          notes: formValue.notes || null
        };
        this.dialogRef.close(command);
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
