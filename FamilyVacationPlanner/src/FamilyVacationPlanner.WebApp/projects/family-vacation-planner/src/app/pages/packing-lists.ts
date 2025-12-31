import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PackingListService, TripService } from '../services';
import { PackingList } from '../models';

@Component({
  selector: 'app-packing-lists',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatListModule,
    MatSnackBarModule
  ],
  template: `
    <div class="packing-lists">
      <div class="packing-lists__header">
        <h1 class="packing-lists__title">Packing Lists</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="packing-lists__add-btn">
          <mat-icon>add</mat-icon>
          Add Item
        </button>
      </div>

      <mat-card *ngIf="showForm" class="packing-lists__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingItem ? 'Edit Item' : 'New Item' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="packingListForm" (ngSubmit)="saveItem()" class="packing-lists__form">
            <mat-form-field appearance="outline" class="packing-lists__form-field">
              <mat-label>Trip</mat-label>
              <mat-select formControlName="tripId" required>
                <mat-option *ngFor="let trip of trips$ | async" [value]="trip.tripId">
                  {{ trip.name }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="packing-lists__form-field">
              <mat-label>Item Name</mat-label>
              <input matInput formControlName="itemName" required>
            </mat-form-field>

            <div class="packing-lists__checkbox-field">
              <mat-checkbox formControlName="isPacked">Packed</mat-checkbox>
            </div>

            <div class="packing-lists__form-actions">
              <button mat-button type="button" (click)="cancelEdit()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!packingListForm.valid">
                {{ editingItem ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="packing-lists__content">
        <mat-card class="packing-lists__list-card">
          <mat-card-header>
            <mat-card-title>Items</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <mat-list>
              <mat-list-item *ngFor="let item of packingLists$ | async" class="packing-lists__item">
                <mat-icon matListItemIcon [class.packing-lists__item-icon--packed]="item.isPacked">
                  {{ item.isPacked ? 'check_box' : 'check_box_outline_blank' }}
                </mat-icon>
                <div matListItemTitle class="packing-lists__item-name" [class.packing-lists__item-name--packed]="item.isPacked">
                  {{ item.itemName }}
                </div>
                <div matListItemLine class="packing-lists__item-created">
                  Created: {{ item.createdAt | date:'short' }}
                </div>
                <div matListItemMeta class="packing-lists__item-actions">
                  <button mat-icon-button color="primary" (click)="togglePacked(item)">
                    <mat-icon>{{ item.isPacked ? 'undo' : 'done' }}</mat-icon>
                  </button>
                  <button mat-icon-button color="primary" (click)="editItem(item)">
                    <mat-icon>edit</mat-icon>
                  </button>
                  <button mat-icon-button color="warn" (click)="deleteItem(item.packingListId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
              </mat-list-item>
            </mat-list>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .packing-lists {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1rem;
        margin-top: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__checkbox-field {
        display: flex;
        align-items: center;
      }

      &__form-actions {
        grid-column: 1 / -1;
        display: flex;
        gap: 1rem;
        justify-content: flex-end;
      }

      &__content {
        max-width: 800px;
      }

      &__list-card {
        mat-list {
          padding: 0;
        }
      }

      &__item {
        border-bottom: 1px solid rgba(0, 0, 0, 0.12);

        &:last-child {
          border-bottom: none;
        }
      }

      &__item-icon {
        &--packed {
          color: #4caf50;
        }
      }

      &__item-name {
        font-weight: 500;

        &--packed {
          text-decoration: line-through;
          opacity: 0.6;
        }
      }

      &__item-created {
        font-size: 0.875rem;
        color: rgba(0, 0, 0, 0.6);
      }

      &__item-actions {
        display: flex;
        gap: 0.25rem;
      }
    }
  `]
})
export class PackingLists implements OnInit {
  packingLists$ = this.packingListService.packingLists$;
  trips$ = this.tripService.trips$;
  packingListForm: FormGroup;
  showForm = false;
  editingItem: PackingList | null = null;

  constructor(
    private packingListService: PackingListService,
    private tripService: TripService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.packingListForm = this.fb.group({
      tripId: ['', Validators.required],
      itemName: ['', Validators.required],
      isPacked: [false]
    });
  }

  ngOnInit() {
    this.loadPackingLists();
    this.loadTrips();
  }

  loadPackingLists() {
    this.packingListService.getPackingLists().subscribe();
  }

  loadTrips() {
    const userId = '00000000-0000-0000-0000-000000000001';
    this.tripService.getTrips(userId).subscribe();
  }

  saveItem() {
    if (this.packingListForm.invalid) return;

    const command = this.packingListForm.value;

    if (this.editingItem) {
      this.packingListService.updatePackingList(this.editingItem.packingListId, {
        packingListId: this.editingItem.packingListId,
        ...command
      }).subscribe({
        next: () => {
          this.snackBar.open('Item updated successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to update item', 'Close', { duration: 3000 });
        }
      });
    } else {
      this.packingListService.createPackingList(command).subscribe({
        next: () => {
          this.snackBar.open('Item created successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to create item', 'Close', { duration: 3000 });
        }
      });
    }
  }

  togglePacked(item: PackingList) {
    this.packingListService.updatePackingList(item.packingListId, {
      ...item,
      isPacked: !item.isPacked
    }).subscribe({
      next: () => {
        this.snackBar.open(
          item.isPacked ? 'Item marked as unpacked' : 'Item marked as packed',
          'Close',
          { duration: 2000 }
        );
      },
      error: () => {
        this.snackBar.open('Failed to update item', 'Close', { duration: 3000 });
      }
    });
  }

  editItem(item: PackingList) {
    this.editingItem = item;
    this.showForm = true;
    this.packingListForm.patchValue(item);
  }

  deleteItem(packingListId: string) {
    if (confirm('Are you sure you want to delete this item?')) {
      this.packingListService.deletePackingList(packingListId).subscribe({
        next: () => {
          this.snackBar.open('Item deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete item', 'Close', { duration: 3000 });
        }
      });
    }
  }

  cancelEdit() {
    this.showForm = false;
    this.editingItem = null;
    this.packingListForm.reset({ isPacked: false });
  }
}
